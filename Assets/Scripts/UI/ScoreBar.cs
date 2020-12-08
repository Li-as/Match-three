using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ScoreBar : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private GameObject _barMask;
    [SerializeField] private Sprite _emptyBarSprite;
    [SerializeField] private Sprite _filledBarSprite;
    [SerializeField] private float _lerpDuration;

    private Image _image;
    private int _currentScore;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _slider.value = 0;
        _currentScore = 0;
    }

    public void ChangeScore(int value, int maxValue)
    {
        _currentScore += value;
        if (_slider.value < 1)
        {
            float nextValue = (float)_currentScore / maxValue;
            StartCoroutine(Fill(_slider.value, nextValue, _lerpDuration));
        }
    }

    private IEnumerator Fill(float currentValue, float nextValue, float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            _slider.value = Mathf.Lerp(currentValue, nextValue, elapsedTime / duration);

            yield return null;
        }
        _slider.value = nextValue;

        if (_slider.value >= 1)
        {
            _barMask.SetActive(false);
            _image.sprite = _filledBarSprite;
        }
    }

    public void Reset()
    {
        _barMask.SetActive(true);
        _image.sprite = _emptyBarSprite;
        _slider.value = 0;
        _currentScore = 0;
    }
}
