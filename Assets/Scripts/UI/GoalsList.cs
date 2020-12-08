using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalsList : MonoBehaviour
{
    [SerializeField] private GoalDescriptionDisplay _goalDescriptionTemplate;
    [SerializeField] private Transform _goalContainer;

    private List<GoalDescriptionDisplay> _goalsDescriptions;

    public void Show(LevelGoalsSystem goalsSystem)
    {
        _goalsDescriptions = new List<GoalDescriptionDisplay>();
        var descriptionDisplay = Instantiate(_goalDescriptionTemplate, _goalContainer);
        descriptionDisplay.ChangeDescription(goalsSystem.ScoreGoal.Info.GoalDescription);
        descriptionDisplay.ChangeState(goalsSystem.ScoreGoal.IsReached());
        _goalsDescriptions.Add(descriptionDisplay);

        foreach (var elementsGoal in goalsSystem.MultiElementsGoal.ElementsGoals)
        {
            descriptionDisplay = Instantiate(_goalDescriptionTemplate, _goalContainer);
            descriptionDisplay.ChangeDescription(elementsGoal.Info.GoalDescription);
            descriptionDisplay.ChangeState(elementsGoal.IsReached());
            _goalsDescriptions.Add(descriptionDisplay);
        }
    }

    public void Reset()
    {
        foreach (var description in _goalsDescriptions)
        {
            Destroy(description.gameObject);
        }
    }
}
