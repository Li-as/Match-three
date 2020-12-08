using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matcher : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private LevelGoalsSystem _goalsSystem;
    [SerializeField] private SoundsPerformer _soundsPerformer;
    [SerializeField] private int _match5Score;
    [SerializeField] private int _match4Score;
    [SerializeField] private int _match3Score;

    public bool TryFindMatches(bool isSearchOnly = false)
    {
        bool isMatchFound = false;

        for (int i = 0; i < _board.Tiles.GetLength(0); i++)
        {
            for (int j = 0; j < _board.Tiles.GetLength(1); j++)
            {
                bool isMatchFoundInThisCycle = false;
                if (_board.Tiles[i, j].IsEmpty == false && _board.Tiles[i, j].IsElementDestroying == false)
                {
                    // Search for horizontal matches
                    if (j < _board.Tiles.GetLength(1) - 2)
                    {
                        //Debug.Log("Result of comparing [" + i + ", " + j + "] and [" + i + ", " + (j + 1) + "] is: " + board.Tiles[i, j].CompareTo(board.Tiles[i, j + 1]));
                        //Debug.Log("Result of comparing [" + i + ", " + j + "] and [" + i + ", " + (j + 2) + "] is: " + board.Tiles[i, j].CompareTo(board.Tiles[i, j + 2]));
                        if (_board.Tiles[i, j].CompareTo(_board.Tiles[i, j + 1]) && _board.Tiles[i, j].CompareTo(_board.Tiles[i, j + 2]))
                        {
                            if (j < _board.Tiles.GetLength(1) - 4)
                            {
                                if (_board.Tiles[i, j].CompareTo(_board.Tiles[i, j + 3]) && _board.Tiles[i, j].CompareTo(_board.Tiles[i, j + 4]))
                                {
                                    if (isSearchOnly == false)
                                    { 
                                        Match(i, j, i, j + 5);
                                    }
                                    isMatchFoundInThisCycle = true;
                                }
                            }
                            if (j < _board.Tiles.GetLength(1) - 3 && isMatchFoundInThisCycle == false)
                            {
                                if (_board.Tiles[i, j].CompareTo(_board.Tiles[i, j + 3]))
                                {
                                    if (isSearchOnly == false)
                                    {
                                        Match(i, j, i, j + 4);
                                    }
                                    isMatchFoundInThisCycle = true;
                                }
                            }
                            if (isMatchFoundInThisCycle == false)
                            {
                                if (isSearchOnly == false)
                                {
                                    Match(i, j, i, j + 3);
                                }
                                isMatchFoundInThisCycle = true;
                            }

                            isMatchFound = true;
                        }
                    }

                    // Search for vertical matches
                    if (i < _board.Tiles.GetLength(0) - 2)
                    {
                        if (isMatchFoundInThisCycle == false)
                        {
                            //Debug.Log("Result of comparing [" + i + ", " + j + "] and [" + (i + 1) + ", " + j + "] is: " + board.Tiles[i, j].CompareTo(board.Tiles[i + 1, j]));
                            //Debug.Log("Result of comparing [" + i + ", " + j + "] and [" + (i + 2) + ", " + j + "] is: " + board.Tiles[i, j].CompareTo(board.Tiles[i + 2, j]));
                            if (_board.Tiles[i, j].CompareTo(_board.Tiles[i + 1, j]) && _board.Tiles[i, j].CompareTo(_board.Tiles[i + 2, j]))
                            {
                                if ((i < _board.Tiles.GetLength(0) - 4))
                                {
                                    if (_board.Tiles[i, j].CompareTo(_board.Tiles[i + 3, j]) && _board.Tiles[i, j].CompareTo(_board.Tiles[i + 4, j]))
                                    {
                                        if (isSearchOnly == false)
                                        {
                                            Match(i, j, i + 5, j);
                                        }
                                        isMatchFoundInThisCycle = true;
                                    }
                                }

                                if (i < _board.Tiles.GetLength(0) - 3 && isMatchFoundInThisCycle == false)
                                {
                                    if (_board.Tiles[i, j].CompareTo(_board.Tiles[i + 3, j]))
                                    {
                                        if (isSearchOnly == false)
                                        {
                                            Match(i, j, i + 4, j);
                                        }
                                        isMatchFoundInThisCycle = true;
                                    }
                                }

                                if (isMatchFoundInThisCycle == false)
                                {
                                    if (isSearchOnly == false)
                                    {
                                        Match(i, j, i + 3, j);
                                    }
                                    isMatchFoundInThisCycle = true;
                                }

                                isMatchFound = true;
                            }
                        }
                    }
                }
            }
        }

        return isMatchFound;
    }

    private void Match(int fromI, int fromJ, int toI, int toJ)
    {
        int matchN = 0;
        if (fromI == toI)
        {
            matchN = toJ - fromJ;
            for (int j = fromJ; j < toJ; j++)
            {
                _board.Tiles[fromI, j].StartElementDestroying();
            }
        }
        else
        {
            matchN = toI - fromI;
            for (int i = fromI; i < toI; i++)
            {
                _board.Tiles[i, fromJ].StartElementDestroying();
            }
        }

        switch (matchN)
        {
            case 5:
                _goalsSystem.AddScore(_match5Score);
                break;
            case 4:
                _goalsSystem.AddScore(_match4Score);
                break;
            case 3:
                _goalsSystem.AddScore(_match3Score);
                break;
        }

        Sprite matchElementSprite = _board.Tiles[fromI, fromJ].Element.Renderer.sprite;
        _goalsSystem.TryAddElements(matchElementSprite, matchN);

        _soundsPerformer.PlaySound(GameSounds.SoundsList.Match);
    }
}
