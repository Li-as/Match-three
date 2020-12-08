using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElementsGoalDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private int _elementAmount;

    public void Init(int goalAmount, Image elementImageTemplate, Sprite elementSprite)
    {
        var elementImage = Instantiate(elementImageTemplate, transform);
        elementImage.sprite = elementSprite;
        _elementAmount = goalAmount;
        _text.text = _elementAmount.ToString();
    }

    public void ChangeAmount(int nextValue)
    {
        StartCoroutine(ChangeText(_elementAmount, nextValue));
    }

    private IEnumerator ChangeText(int startValue, int nextValue)
    {
        while (startValue != nextValue)
        {
            startValue--;
            _text.text = startValue.ToString();
            yield return new WaitForSeconds(0.1f);
        }
        _text.text = nextValue.ToString();
        _elementAmount = nextValue;

        yield break;
    }

    public void Reset(int goalAmount)
    {
        _elementAmount = goalAmount;
        _text.text = _elementAmount.ToString();
    }
}
