using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newGameSounds", menuName = "Game Sounds", order = 51)]
public class GameSounds : ScriptableObject
{
    [SerializeField] private AudioClip _pickElementSound;
    [SerializeField] private AudioClip _matchSound;
    [SerializeField] private AudioClip _buttonClickSound;
    [SerializeField] private AudioClip _toggleClickSound;
    [SerializeField] private AudioClip _winSound;
    [SerializeField] private AudioClip _failSound;

    private Dictionary<SoundsList, AudioClip> _dictionary;

    public enum SoundsList
    {
        PickElement,
        Match,
        ButtonClick,
        ToggleClick,
        Win,
        Fail
    }

    //public void Init()
    //{
    //    _dictionary = new Dictionary<SoundsList, AudioClip>();
    //    _dictionary.Add(SoundsList.PickElement, _pickElementSound);
    //    _dictionary.Add(SoundsList.Match, _matchSound);
    //    _dictionary.Add(SoundsList.ButtonClick, _buttonClickSound);
    //    _dictionary.Add(SoundsList.ToggleClick, _toggleClickSound);
    //    _dictionary.Add(SoundsList.Win, _winSound);
    //    _dictionary.Add(SoundsList.Fail, _failSound);
    //}

    public AudioClip GetSound(SoundsList sound)
    {
        //return _dictionary[sound];

        AudioClip clip;

        switch (sound)
        {
            case SoundsList.PickElement:
                clip = _pickElementSound;
                break;
            case SoundsList.Match:
                clip = _matchSound;
                break;
            case SoundsList.ButtonClick:
                clip = _buttonClickSound;
                break;
            case SoundsList.ToggleClick:
                clip = _toggleClickSound;
                break;
            case SoundsList.Win:
                clip = _winSound;
                break;
            case SoundsList.Fail:
                clip = _failSound;
                break;
            default:
                clip = null;
                break;
        }

        return clip;
    }
}
