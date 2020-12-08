using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    WaitForEndOfStart,
    CheckMatches,
    WaitForEndOfMatch,
    FillEmptyTiles,
    WaitForEndOfFill,
    PlayerTurn,
    EndOfLevel
}