using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundsPerformer : MonoBehaviour
{
    [SerializeField] private GameSounds _gameSounds;
    [SerializeField] private GameSettings _settings;

    private AudioSource _source;

    private void Start()
    {
        _source = GetComponent<AudioSource>();
    }

    public void PlaySound(GameSounds.SoundsList sound)
    {
        if (_settings.IsSoundEnabled)
        {
            _source.clip = _gameSounds.GetSound(sound);
            if (_source.clip != null)
            {
                _source.Play();
            }
        }
    }
}
