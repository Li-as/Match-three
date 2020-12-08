using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerTurnState
{
    WaitForStartOfTurn,
    ChooseElements,
    WaitForEndOfSwap,
    CheckMatches,
    EndOfTurn
}