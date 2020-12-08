using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MovesGoal : Goal
{
    [SerializeField] private MovesGoalInfo _info;
    [SerializeField] private MovesLeftDisplay _movesDisplay;

    public MovesGoalInfo Info => _info;

    public void Init()
    {
        CurrentAmount = _info.GoalValue;
        _movesDisplay.ChangeText(CurrentAmount);
    }

    public override bool IsReached()
    {
        bool isReached;
        if (CurrentAmount >= 0)
        {
            isReached = true;
        }
        else
        {
            isReached = false;
        }

        return isReached;
    }

    public bool IsNoMovesLeft()
    {
        if (CurrentAmount == 0)
        {
            return true;
        }

        return false;
    }

    public void ChangeAmount()
    {
        CurrentAmount--;
        _movesDisplay.ChangeText(CurrentAmount);
    }

    public override void Reset()
    {
        Init();
    }
}
