using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Goal : MonoBehaviour
{
    protected int CurrentAmount;

    public int CurrentValue => CurrentAmount;

    public abstract bool IsReached();
    public abstract void Reset();
}
