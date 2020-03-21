using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{

    public GameObject tile;
    public bool walkable;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public Node parent;

    public Node(GameObject tile)
    {
        this.tile = tile;
        this.walkable = tile.GetComponent<Tile>().objectOnTile == null;
        this.gridX = (int) Math.Round(tile.transform.position.x / 0.5f);
        this.gridY = (int) Math.Round(tile.transform.position.y / 0.5f);
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }

}
