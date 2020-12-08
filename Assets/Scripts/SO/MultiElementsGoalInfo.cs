using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMultiElementsGoal", menuName = "Goal/MultiElements Goal", order = 51)]
public class MultiElementsGoalInfo : ScriptableObject
{
    [SerializeField] private int goalsInfoMaxSize = 4;
    [SerializeField] private ElementsGoalInfo[] _goalsInfo;

    public ElementsGoalInfo[] GoalsInfo => _goalsInfo;


    private void OnValidate()
    {
        if (_goalsInfo.Length > goalsInfoMaxSize)
        {
            _goalsInfo = new ElementsGoalInfo[goalsInfoMaxSize];
        }
    }
}
