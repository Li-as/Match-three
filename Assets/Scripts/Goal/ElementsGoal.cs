using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ElementsGoal : Goal
{
    private ElementsGoalInfo _info;
    private ElementsGoalDisplay _goalDisplay;

    public ElementsGoalInfo Info => _info;

    public void Init(ElementsGoalInfo info, ElementsGoalDisplay elementsDisplayTemplate, Transform elementsDisplayContainer, Image elementImageTemplate)
    {
        _info = info;
        CurrentAmount = _info.GoalValue;
        _goalDisplay = Instantiate(elementsDisplayTemplate, elementsDisplayContainer);
        _goalDisplay.Init(CurrentAmount, elementImageTemplate, _info.ElementSprite);
    }

    public override bool IsReached()
    {
        bool isReached;
        if (CurrentAmount == 0)
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
        if (CurrentAmount - value > 0)
        {
            CurrentAmount -= value;
        }
        else
        {
            CurrentAmount = 0;
        }

        _goalDisplay.ChangeAmount(CurrentAmount);
    }

    public override void Reset()
    {
        CurrentAmount = _info.GoalValue;
        _goalDisplay.Reset(CurrentAmount);
    }
}
