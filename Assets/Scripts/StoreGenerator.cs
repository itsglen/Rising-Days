using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreGenerator : MonoBehaviour
{

    public GameObject tile;
    public GameObject wall;
    public GameObject border;

    private string[] tileSprites;
    private Sprite selectedTileSprite;
    private List<GameObject> tiles;
    private List<GameObject> walls;
    private List<GameObject> borders;
    private Color defaultColor;
    private int gridSize = 7;
    private int wallHeight = 3;

    void Start()
    {
        tiles = new List<GameObject>();
        walls = new List<GameObject>();
        borders = new List<GameObject>();

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

        // Generate walls at the top of the tilemap
        for (int i = 1; i < gridSize + 1; i++)
        {
            GameObject wallToInstantiate = wall;
            Vector3 positionToSpawnAt = tiles[(i * gridSize) - 1].GetComponent<Transform>().position;
            positionToSpawnAt.y += 1f;
            wallToInstantiate.GetComponent<Transform>().position = positionToSpawnAt;
            GameObject instantiatedObject = Instantiate(wallToInstantiate);
            instantiatedObject.GetComponent<Transform>().parent = GetComponent<Transform>();
            walls.Add(instantiatedObject);
        }

        // Generate borders of tilemap
        // Start with corners
        GameObject borderToInstantiate = border;
        Vector3 positionToSpawnBorder = tiles[0].GetComponent<Transform>().position;
        borderToInstantiate.GetComponent<SpriteRenderer>().sprite = border.GetComponent<Border>().topRight;
        positionToSpawnBorder.x -= 0.5f;
        positionToSpawnBorder.y -= 0.5f;
        borderToInstantiate.GetComponent<Transform>().position = positionToSpawnBorder;
        GameObject instantiatedBorder = Instantiate(borderToInstantiate);
        instantiatedBorder.GetComponent<Transform>().parent = GetComponent<Transform>();
        borders.Add(instantiatedBorder);

    }

}
