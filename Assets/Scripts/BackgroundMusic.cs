using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] private GameSettings _settings;
    
    private static GameObject _instance;
    private AudioSource _source;
    private bool _isPaused;

    private void Start()
    {
        if (_instance == null)
        {
            _instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }

        _source = GetComponent<AudioSource>();

        if (_settings.IsMusicEnabled)
        {
            _source.Play();
        }

        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (_settings.IsMusicEnabled)
        {
            if (_isPaused)
            {
                _source.UnPause();
            }
            else
            {
                if (_source.isPlaying == false)
                {
                    _source.Play();
                }
            }
            _isPaused = false;
        }
        else
        {
            if (_source.isPlaying)
            {
                _source.Pause();
                _isPaused = true;
            }
        }
    }
}
