using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementCreator : MonoBehaviour
{
    [SerializeField] private Element _elementTemplate;
    [SerializeField] private List<Sprite> _candiesSprites;

    public Element GetNewElement(Tile parent)
    {
        Element newElement = Instantiate(_elementTemplate, parent.transform);
        newElement.Renderer.sprite = _candiesSprites[Random.Range(0, _candiesSprites.Count)];
        return newElement;
    }

    public Element GetNewElement(Vector3 elementPosition)
    {
        Element newElement = Instantiate(_elementTemplate, elementPosition, Quaternion.identity);
        newElement.Renderer.sprite = _candiesSprites[Random.Range(0, _candiesSprites.Count)];
        return newElement;
    }
}
