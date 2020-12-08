using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TipScreen : Screen
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private SoundsPerformer _soundsPerformer;

    public void Open(string tip)
    {
        _text.text = tip;
        gameObject.SetActive(true);
    }

    public override void Close()
    {
        _soundsPerformer.PlaySound(GameSounds.SoundsList.ButtonClick);
        gameObject.SetActive(false);
    }
}
