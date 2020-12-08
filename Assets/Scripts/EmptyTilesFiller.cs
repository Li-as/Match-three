using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTilesFiller : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private TilesElementsSwapper _swapper;
    [SerializeField] private ElementCreator _creator;

    public void FillEmptyTiles()
    {
        int amountOfMoves = 0;

        for (int j = 0; j < _board.Tiles.GetLength(1); j++)
        {
            for (int i = 0; i < _board.Tiles.GetLength(0); i++)
            {
                if (_board.Tiles[i, j].IsEmpty)
                {
                    MoveColumnDown(i, j, amountOfMoves);
                    amountOfMoves++;
                }
            }
        }

        for (int i = 0; i < _board.Tiles.GetLength(0); i++)
        {
            for (int j = 0; j < _board.Tiles.GetLength(1); j++)
            {
                if (_board.Tiles[i, j].Element.transform.position != _board.Tiles[i, j].transform.position)
                {
                    _board.Tiles[i, j].MoveElementIntoTile();
                }
            }
        }
    }

    private void MoveColumnDown(int toI, int toJ, int amountOfMoves)
    {
        for (int i = toI; i >= 0; i--)
        {
            if (i == 0)
            {
                if (_board.Tiles[i, toJ].IsEmpty)
                {
                    Vector2 tileSize = _board.Tiles[i, toJ].GetComponent<Renderer>().bounds.size;
                    Vector3 newElementPosition = _board.Tiles[i, toJ].transform.position;
                    newElementPosition.y += tileSize.y * (amountOfMoves + 1) / 2;
                    Element newElement = _creator.GetNewElement(newElementPosition);
                    _board.Tiles[i, toJ].ChangeElement(newElement);
                }
            }
            else if (_board.Tiles[i - 1, toJ].IsEmpty == false)
            {
                _swapper.SwapElements(_board.Tiles[i, toJ], _board.Tiles[i - 1, toJ]);
            }
        }
    }
}
