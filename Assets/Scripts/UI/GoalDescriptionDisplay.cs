using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoalDescriptionDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _descriptionDisplay;
    [SerializeField] private Image _goalStateImage;
    [SerializeField] private Sprite _goalFailedSprite;
    [SerializeField] private Sprite _goalReachedSprite;

    public void ChangeDescription(string newDescription)
    {
        _descriptionDisplay.text = newDescription;
    }

    public void ChangeState(bool isReached)
    {
        if (isReached)
        {
            _goalStateImage.sprite = _goalReachedSprite;
        }
        else
        {
            _goalStateImage.sprite = _goalFailedSprite;
        }
    }
}
