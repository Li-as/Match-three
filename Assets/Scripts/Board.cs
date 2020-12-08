using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private ElementCreator _elementsCreator;
    [SerializeField] private int _cellsInYAxis;
    [SerializeField] private int _cellsInXAxis;

    private List<Tile> _tilesFromScene = new List<Tile>();
    private Tile[,] _tiles;

    public Tile[,] Tiles => _tiles;

    private void Awake()
    {
        _tiles = new Tile[_cellsInYAxis, _cellsInXAxis];
        _tilesFromScene.AddRange(gameObject.GetComponentsInChildren<Tile>());
        Fill();

        //ShowInfo();
    }

    private void Fill()
    {
        for (int i = 0; i < _tiles.GetLength(0); i++)
        {
            for (int j = 0; j < _tiles.GetLength(1); j++)
            {
                _tiles[i, j] = _tilesFromScene[j + i * _cellsInXAxis];
                _tiles[i, j].Init(_elementsCreator.GetNewElement(_tiles[i, j]), i, j);
            }
        }
    }

    private void Clear()
    {
        for (int i = 0; i < _tiles.GetLength(0); i++)
        {
            for (int j = 0; j < _tiles.GetLength(1); j++)
            {
                Destroy(_tiles[i, j].Element.gameObject);
            }
        }
    }

    //private void ShowInfo()
    //{
    //    foreach (var tile in _tiles)
    //    {
    //        tile.ShowInfo();
    //    }
    //}

    public bool TryFindDestroyingElements()
    {
        bool isElementFound = false;

        for (int i = 0; i < Tiles.GetLength(0); i++)
        {
            for (int j = 0; j < Tiles.GetLength(1); j++)
            {
                if (Tiles[i, j].IsElementDestroying)
                {
                    isElementFound = true;
                }
            }
        }

        return isElementFound;
    }

    public bool TryFindMovingElements()
    {
        bool isElementFound = false;

        for (int i = 0; i < Tiles.GetLength(0); i++)
        {
            for (int j = 0; j < Tiles.GetLength(1); j++)
            {
                if (Tiles[i, j].IsElementInWrongPosition)
                {
                    isElementFound = true;
                }
            }
        }

        return isElementFound;
    }

    public void Refresh()
    {
        for (int i = 0; i < Tiles.GetLength(0); i++)
        {
            for (int j = 0; j < Tiles.GetLength(1); j++)
            {
                Tiles[i, j].StartElementDestroying();
            }
        }
    }

    public void Reset()
    {
        Clear();
        Fill();
    }
}
