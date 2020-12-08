using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private Matcher _matcher;
    [SerializeField] private ElementHighlighter _highlighter;
    [SerializeField] private TilesElementsSwapper _swapper;
    [SerializeField] private SoundsPerformer _soundsPerformer;

    private bool _isTurnStarted;
    private PlayerTurnState _playerTurnState;
    private Tile _highlightedTile;
    private Tile _chosenTile;
    private bool _isWrongSwap;
    private bool _isNoPossibleTurns;

    public bool IsNoPossibleTurns => _isNoPossibleTurns;
    public PlayerTurnState State => _playerTurnState;

    private void Start()
    {
        _playerTurnState = PlayerTurnState.WaitForStartOfTurn;
        _isTurnStarted = false;
    }

    private void Update()
    {
        if (_playerTurnState == PlayerTurnState.WaitForStartOfTurn)
        {
            if (_isTurnStarted)
            {
                _playerTurnState = PlayerTurnState.ChooseElements;
            }
        }
        else if (_playerTurnState == PlayerTurnState.ChooseElements)
        {
            if (_highlightedTile != null && _highlightedTile != _chosenTile)
            {
                _highlighter.RemoveHighlight(_highlightedTile.Element);
            }

            Tile tileUnderMouse = GetTileUnderMouse();
            if (tileUnderMouse != null)
            {
                _highlightedTile = tileUnderMouse;
                if (_highlightedTile != _chosenTile)
                {
                    _highlighter.AddHighlight(_highlightedTile.Element);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    if (_chosenTile != null)
                    {
                        if (_chosenTile != _highlightedTile)
                        {
                            _highlighter.RemoveHighlight(_chosenTile.Element);
                        }

                        if (_highlightedTile.IsCloseTo(_chosenTile))
                        {
                            _highlighter.RemoveHighlight(_highlightedTile.Element);

                            _swapper.SwapElements(_highlightedTile, _chosenTile);
                            _highlightedTile.MoveElementIntoTile();
                            _chosenTile.MoveElementIntoTile();

                            _playerTurnState = PlayerTurnState.WaitForEndOfSwap;
                        }
                        else
                        {
                            _chosenTile = _highlightedTile;
                        }
                    }
                    else
                    {
                        _chosenTile = _highlightedTile;
                    }

                    _soundsPerformer.PlaySound(GameSounds.SoundsList.PickElement);
                }
            }
        }
        else if (_playerTurnState == PlayerTurnState.WaitForEndOfSwap)
        {
            if (_board.TryFindMovingElements() == false)
            {
                if (_isWrongSwap == false)
                {
                    _playerTurnState = PlayerTurnState.CheckMatches;
                }
                else
                {
                    _isWrongSwap = false;
                    _playerTurnState = PlayerTurnState.ChooseElements;
                }
            }
        }
        else if (_playerTurnState == PlayerTurnState.CheckMatches)
        {
            if (_matcher.TryFindMatches())
            {
                _playerTurnState = PlayerTurnState.EndOfTurn;
            }
            else
            {
                _isWrongSwap = true;
                _swapper.SwapElements(_highlightedTile, _chosenTile);
                _highlightedTile.MoveElementIntoTile();
                _chosenTile.MoveElementIntoTile();

                _playerTurnState = PlayerTurnState.WaitForEndOfSwap;
            }

            _highlightedTile = null;
            _chosenTile = null;
        }
        else if (_playerTurnState == PlayerTurnState.EndOfTurn)
        {
            if (_isTurnStarted == false)
            {
                _playerTurnState = PlayerTurnState.WaitForStartOfTurn;
            }
        }
    }

    public void StartTurn()
    {
        _isTurnStarted = true;

        if (TryFindPossibleTurn())
        {
            _isNoPossibleTurns = false;
        }
        else
        {
            _isNoPossibleTurns = true;
            _playerTurnState = PlayerTurnState.EndOfTurn;
        }
    }

    public void EndTurn()
    {
        _isTurnStarted = false;
    }

    private Tile GetTileUnderMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100);
        if (hit)
        {
            if (hit.collider.gameObject.TryGetComponent<Tile>(out Tile tile))
            {
                return tile;
            }
        }

        return null;
    }

    private bool TryFindPossibleTurn()
    {
        bool isTurnFound = false;
        for (int i = 0; i < _board.Tiles.GetLength(0); i++)
        {
            for (int j = 0; j < _board.Tiles.GetLength(1); j++)
            {
                if (isTurnFound == false && j < _board.Tiles.GetLength(1) - 1)
                {
                    _swapper.SwapElements(_board.Tiles[i, j], _board.Tiles[i, j + 1]);
                    isTurnFound = _matcher.TryFindMatches(isSearchOnly: true);
                    _swapper.SwapElements(_board.Tiles[i, j], _board.Tiles[i, j + 1]);
                }
                if (isTurnFound == false && i < _board.Tiles.GetLength(0) - 1)
                {
                    _swapper.SwapElements(_board.Tiles[i, j], _board.Tiles[i + 1, j]);
                    isTurnFound = _matcher.TryFindMatches(isSearchOnly: true);
                    _swapper.SwapElements(_board.Tiles[i, j], _board.Tiles[i + 1, j]);
                }
            }
        }

        return isTurnFound;
    }
}
