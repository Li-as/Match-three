using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelResetter : MonoBehaviour
{
    [SerializeField] private GameStatesChanger _gameStatesChanger;
    [SerializeField] private Board _board;
    [SerializeField] private LevelGoalsSystem _goalsSystem;
    [SerializeField] private LevelEndScreen _endScreen;

    public void ResetLevel()
    {
        _endScreen.Reset();
        _board.Reset();
        _goalsSystem.Reset();
        _gameStatesChanger.Reset();
    }
}
