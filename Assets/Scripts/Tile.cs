using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Element _element;
    private bool _isEmpty;
    private int _boardPositionI;
    private int _boardPositionJ;
    private bool _isElementInWrongPosition;
    private bool _isElementDestroying;

    public Element Element => _element;
    public bool IsEmpty => _isEmpty;
    public int BoardPositionI => _boardPositionI;
    public int BoardPositionJ => _boardPositionJ;
    public bool IsElementInWrongPosition => _isElementInWrongPosition;
    public bool IsElementDestroying => _isElementDestroying;

    public void Init(Element element, int positionI, int positionJ)
    {
        _element = element;
        _isEmpty = false;
        _boardPositionI = positionI;
        _boardPositionJ = positionJ;

        _isElementInWrongPosition = false;
        _isElementDestroying = false;
        _element.ElementStoppedMoving += OnElementStoppedMoving;
        _element.ElementDestroyed += OnElementDestroyed;
    }

    public void RemoveElementListeners()
    {
        if (_isEmpty)
        {
            return;
        }

        _element.ElementStoppedMoving -= OnElementStoppedMoving;
        _element.ElementDestroyed -= OnElementDestroyed;
    }

    public void ChangeElement(Element element)
    {
        _element = element;
        if (_element == null)
        {
            _isEmpty = true;
        }
        else
        {
            _isEmpty = false;
            _element.gameObject.transform.SetParent(transform);
            _element.ElementStoppedMoving += OnElementStoppedMoving;
            _element.ElementDestroyed += OnElementDestroyed;
        }
    }

    public void MoveElementIntoTile()
    {
        _isElementInWrongPosition = true;
        _element.StartMovingTo(transform.position);
    }

    public void OnElementStoppedMoving()
    {
        //Debug.Log("In OnElementStoppedMoving method");

        _isElementInWrongPosition = false;
    }

    public void StartElementDestroying()
    {
        _isElementDestroying = true;
        _element.StartDestroying();
    }

    public void OnElementDestroyed()
    {
        //Debug.Log("In OnElementDestroyed method");

        RemoveElementListeners();
        Destroy(_element.gameObject);
        _isElementDestroying = false;
        _element = null;
        _isEmpty = true;
    }

    public void ShowInfo()
    {
        Debug.Log("Tile [" + _boardPositionI + ", " + _boardPositionJ + "]; Contains: " + _element +
            "; IsEmpty: " + _isEmpty);
    }

    public bool CompareTo(Tile tile)
    {
        if (_isEmpty || tile.IsEmpty)
        {
            if (_isEmpty == tile.IsEmpty)
            {
                return true;
            }
        }
        else if (_element.Renderer.sprite == tile.Element.Renderer.sprite)
        {
            return true;
        }

        return false;
    }

    public bool IsCloseTo(Tile tile)
    {
        if ((Mathf.Abs(_boardPositionI - tile.BoardPositionI) == 1 && _boardPositionJ == tile.BoardPositionJ) ||
            (Mathf.Abs(_boardPositionJ - tile.BoardPositionJ) == 1 && _boardPositionI == tile.BoardPositionI))
        {
            return true;
        }

        return false;
    }
}
