using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatesChanger : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private float _startDelay;
    [SerializeField] private Matcher _matcher;
    [SerializeField] private EmptyTilesFiller _tilesFiller;
    [SerializeField] private PlayerTurn _playerTurn;
    [SerializeField] private LevelGoalsSystem _goalsSystem;
    [SerializeField] private LevelEndScreen _endScreen;

    private bool _isInStartDelay;
    private GameState _gameState;

    private void Start()
    {
        _gameState = GameState.WaitForEndOfStart;
    }

    private void Update()
    {
        if (_gameState == GameState.WaitForEndOfStart)
        {
            if (_isInStartDelay == false)
            {
                StartCoroutine(WaitForEndOfStartDelay());
            }
        }
        else if (_gameState == GameState.CheckMatches)
        {
            if(_matcher.TryFindMatches())
            {
                _gameState = GameState.WaitForEndOfMatch;
            }
            else
            {
                _gameState = GameState.PlayerTurn;
            }
        }
        else if (_gameState == GameState.WaitForEndOfMatch)
        {
            if (_board.TryFindDestroyingElements() == false)
            {
                _gameState = GameState.FillEmptyTiles;
            }
        }
        else if (_gameState == GameState.FillEmptyTiles)
        {
            _tilesFiller.FillEmptyTiles();
            _gameState = GameState.WaitForEndOfFill;
        }
        else if (_gameState == GameState.WaitForEndOfFill)
        {
            if (_board.TryFindMovingElements() == false)
            {
                _gameState = GameState.CheckMatches;
            }
        }
        else if (_gameState == GameState.PlayerTurn)
        {
            if (_playerTurn.State == PlayerTurnState.WaitForStartOfTurn)
            {
                if (_goalsSystem.IsEndOfLevel())
                {
                    _endScreen.Open();
                    _gameState = GameState.EndOfLevel;
                }
                else
                {
                    _playerTurn.StartTurn();
                }
            }
            else if (_playerTurn.State == PlayerTurnState.EndOfTurn)
            {
                if (_playerTurn.IsNoPossibleTurns)
                {
                    _board.Refresh();
                }
                else
                {
                    _goalsSystem.ReduceMovesAmount();
                }

                _playerTurn.EndTurn();
                _gameState = GameState.WaitForEndOfMatch;
            }
        }
    }

    private IEnumerator WaitForEndOfStartDelay()
    {
        float passedTime = 0;

        _isInStartDelay = true;
        while (passedTime < _startDelay)
        {
            passedTime += Time.deltaTime;
            yield return null;
        }
        _isInStartDelay = false;

        _gameState = GameState.CheckMatches;

        yield break;
    }

    public void Reset()
    {
        _gameState = GameState.WaitForEndOfStart;
    }
}