using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsMenuScreen : Screen
{
    [SerializeField] private MainMenuScreen _mainMenuScreen;
    [SerializeField] private TipScreen _tipScreen;
    [SerializeField] private SoundsPerformer _soundsPerformer;


    private void Start()
    {
        LevelButton[] levelButtons;
        levelButtons = GetComponentsInChildren<LevelButton>();
        foreach(LevelButton levelButton in levelButtons)
        {
            if (levelButton.TryGetComponent<Button>(out Button button))
            {
                button.onClick.AddListener(() => TryOpenLevel(levelButton));
            }
        }
    }

    public void OpenMainMenuScreen()
    {
        _soundsPerformer.PlaySound(GameSounds.SoundsList.ButtonClick);
        _mainMenuScreen.Open();
        Close();
    }

    public void TryOpenLevel(LevelButton levelButton)
    {
        _soundsPerformer.PlaySound(GameSounds.SoundsList.ButtonClick);
        if (levelButton.LevelSceneName != "")
        {
            SceneManager.LoadScene(levelButton.LevelSceneName);
        }
        else
        {
            _tipScreen.Open("Coming soon");
        }
    }
}
