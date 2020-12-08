using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiElementsGoal : MonoBehaviour
{
    [SerializeField] private MultiElementsGoalInfo _info;
    [SerializeField] private ElementsGoalDisplay _goalDisplayTemplate;
    [SerializeField] private Transform _goalDisplayContainer;
    [SerializeField] private Image _elementImageTemplate;
    [SerializeField] private ElementsGoal _elementsGoalTemplate;

    private List<ElementsGoal> _elementsGoals;

    public MultiElementsGoalInfo Info => _info;
    public List<ElementsGoal> ElementsGoals => _elementsGoals;


    private void Awake()
    {
        _elementsGoals = new List<ElementsGoal>();
        foreach (var goalInfo in _info.GoalsInfo)
        {
            ElementsGoal elementsGoal = Instantiate(_elementsGoalTemplate, transform);
            elementsGoal.Init(goalInfo, _goalDisplayTemplate, _goalDisplayContainer, _elementImageTemplate);
            _elementsGoals.Add(elementsGoal);
        }
    }

    public bool IsHalfReached()
    {
        int half = _elementsGoals.Count / 2;

        foreach (var goal in _elementsGoals)
        {
            if (goal.IsReached())
            {
                half--;
            }
        }

        if (half <= 0)
        {
            return true;
        }

        return false;
    }

    public bool IsReached()
    {
        foreach (var goal in _elementsGoals)
        {
            if (goal.IsReached() == false)
            {
                return false;
            }
        }

        return true;
    }

    public void TryAddElements(Sprite elementSprite, int value)
    {
        foreach (var goal in _elementsGoals)
        {
            if (goal.Info.ElementSprite == elementSprite)
            {
                goal.ChangeAmount(value);
            }
        }
    }

    public void Reset()
    {
        foreach (var goal in _elementsGoals)
        {
            goal.Reset();
        }
    }
}
