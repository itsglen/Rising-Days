using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreGenerator : MonoBehaviour
{

    public GameObject tile;

    private string[] tileSprites;
    private Sprite selectedTileSprite;
    private List<GameObject> tiles;
    private Color defaultColor;
    private int gridSize = 6;

    void Start()
    {
        tiles = new List<GameObject>();
        tileSprites = new string[] { "Tile01", "Tile02" };

        // Create a gridSizexgridSize grid of tiles using public prefab
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                GameObject tileToInstantiate = tile;
                tileToInstantiate.GetComponent<Transform>().position = new Vector3(i * 0.5f, j * 0.5f, 2);
                GameObject instantiatedObject = Instantiate(tileToInstantiate);
                instantiatedObject.GetComponent<Transform>().parent = GetComponent<Transform>();
                tiles.Add(instantiatedObject);
            }
        }
    }

}
