using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsScreen : Screen
{
    [SerializeField] private GameSettings _settings;
    [SerializeField] private SoundsPerformer _soundsPerformer;
    [SerializeField] private Sprite _switchButtonOffSprite;
    [SerializeField] private Sprite _switchButtonOnSprite;
    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private Toggle _soundToggle;


    private void Awake()
    {
        _musicToggle.isOn = _settings.IsMusicEnabled;
        _soundToggle.isOn = _settings.IsSoundEnabled;
    }

    public override void Close()
    {
        _soundsPerformer.PlaySound(GameSounds.SoundsList.ButtonClick);

        gameObject.SetActive(false);
    }

    public void SwitchMusic()
    {
        _soundsPerformer.PlaySound(GameSounds.SoundsList.ToggleClick);

        _settings.IsMusicEnabled = _musicToggle.isOn;
    }

    public void SwitchSound()
    {
        _soundsPerformer.PlaySound(GameSounds.SoundsList.ToggleClick);

        _settings.IsSoundEnabled = _soundToggle.isOn;
    }
}
