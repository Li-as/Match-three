using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScreen : Screen
{
    [SerializeField] private string _startLevelSceneName;
    [SerializeField] private LevelsMenuScreen _levelsMenuScreen;
    [SerializeField] private SettingsScreen _settingsScreen;
    [SerializeField] private SoundsPerformer _soundsPerformer;


    public void OpenLevelsMenuScreen()
    {
        _soundsPerformer.PlaySound(GameSounds.SoundsList.ButtonClick);
        _levelsMenuScreen.Open();
        Close();
    }

    public void OpenSettingsScreen()
    {
        _soundsPerformer.PlaySound(GameSounds.SoundsList.ButtonClick);
        _settingsScreen.Open();
    }

    public void Exit()
    {
        _soundsPerformer.PlaySound(GameSounds.SoundsList.ButtonClick);
        Application.Quit();
    }
}
