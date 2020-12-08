using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelEndScreen : LevelScreen
{
    [SerializeField] private string _nextLevelSceneName;
    [SerializeField] private string _menuSceneName;
    [SerializeField] private LevelGoalsSystem _goalsSystem;
    [SerializeField] private Button _nextLevelButton;
    [SerializeField] private LevelResetter _levelResetter;

    public override void Open()
    {
        Init(_goalsSystem);

        if (_goalsSystem.GoalsFailed)
        {
            SoundsPerformer.PlaySound(GameSounds.SoundsList.Fail);
            _nextLevelButton.interactable = false;
        }
        else if (_goalsSystem.GoalsReached)
        {
            SoundsPerformer.PlaySound(GameSounds.SoundsList.Win);
        }

        if (_nextLevelSceneName == "")
        {
            _nextLevelButton.interactable = false;
        }

        gameObject.SetActive(true);
    }

    public void RetryLevel()
    {
        SoundsPerformer.PlaySound(GameSounds.SoundsList.ButtonClick);

        _levelResetter.ResetLevel();
    }

    public void OpenMainMenu()
    {
        SoundsPerformer.PlaySound(GameSounds.SoundsList.ButtonClick);

        SceneManager.LoadScene(_menuSceneName);
    }

    public void OpenNextLevel()
    {
        SoundsPerformer.PlaySound(GameSounds.SoundsList.ButtonClick);

        if (_nextLevelSceneName != "")
        {
            SceneManager.LoadScene(_nextLevelSceneName);
        }
    }

    public override void Reset()
    {
        base.Reset();
        _nextLevelButton.interactable = true;
        Close();
    }
}
