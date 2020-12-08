using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newElementsGoal", menuName = "Goal/Elements Goal", order = 51)]
public class ElementsGoalInfo : GoalInfo
{
    [SerializeField] private Sprite _elementSprite;

    public Sprite ElementSprite => _elementSprite;
}
