using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreGoal : Goal
{
    [SerializeField] private ScoreGoalInfo _info;
    [SerializeField] private ScoreBar _scoreBar;

    public ScoreGoalInfo Info => _info;


    public void Init()
    {
        CurrentAmount = 0;
    }

    public override bool IsReached()
    {
        bool isReached;
        if (CurrentAmount >= _info.GoalValue)
        {
            isReached = true;
        }
        else
        {
            isReached = false;
        }    

        return isReached;
    }

    public void ChangeAmount(int value)
    {
        CurrentAmount += value;
        _scoreBar.ChangeScore(value, _info.GoalValue);
    }

    public override void Reset()
    {
        CurrentAmount = 0;
        _scoreBar.Reset();
    }
}
