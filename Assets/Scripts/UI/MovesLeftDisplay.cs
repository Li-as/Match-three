using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MovesLeftDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    public void ChangeText(int amount)
    {
        _text.text = "Moves left: " + amount;
    }
}
