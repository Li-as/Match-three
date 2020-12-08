using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalInfo : ScriptableObject
{
    [SerializeField] protected int GoalAmount;
    [SerializeField] protected string Description;

    public int GoalValue => GoalAmount;
    public string GoalDescription => Description;
}
