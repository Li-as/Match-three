using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementHighlighter : MonoBehaviour
{
    [SerializeField] private float _highlightForce;
    [SerializeField] private Element _elementTemplate;

    public void AddHighlight(Element element)
    {
        Vector3 elementScale = element.transform.localScale;
        elementScale.x += _highlightForce;
        elementScale.y += _highlightForce;
        element.transform.localScale = elementScale;
    }

    public void RemoveHighlight(Element element)
    {
        element.transform.localScale = _elementTemplate.transform.localScale;
    }
}
