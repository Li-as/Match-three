using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesElementsSwapper : MonoBehaviour
{
    public void SwapElements(Tile tile1, Tile tile2)
    {
        tile1.RemoveElementListeners();
        tile2.RemoveElementListeners();

        Element tempElement = tile1.Element;
        tile1.ChangeElement(tile2.Element);
        tile2.ChangeElement(tempElement);
    }
}
