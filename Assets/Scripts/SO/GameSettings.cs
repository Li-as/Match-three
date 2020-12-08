using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGameSettings", menuName = "Game Settings", order = 51)]
public class GameSettings : ScriptableObject
{
    public bool IsMusicEnabled;
    public bool IsSoundEnabled;
}
