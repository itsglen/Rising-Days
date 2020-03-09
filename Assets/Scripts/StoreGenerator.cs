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
    private List<GameObject> corners;
    private List<GameObject> borders;
    private Color defaultColor;

    // Size in grid units of one length of grid
    private int gridSize = 10;
    // The height of the wall sprite, value refers to grid units (will affect corner placement)
    private int wallHeight = 3;

    void Start()
    {
        tiles = new List<GameObject>();
        walls = new List<GameObject>();
        corners = new List<GameObject>();
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

        // Grab corners
        corners.Add(tiles[0]); // Bottom left
        corners.Add(tiles[gridSize - 1]); // Top left
        corners.Add(tiles[tiles.Count - 1]); // Top right
        corners.Add(tiles[tiles.Count - gridSize]); // Bottom Right

        // Iterate through corners and instantiate each corner
        foreach (GameObject corner in corners)
        {
            GameObject borderToInstantiate = border;
            Vector3 positionToSpawnAt = corner.GetComponent<Transform>().position;

            // Right side of map
            if (positionToSpawnAt.x > 0)
            {
                // Top of map
                if (positionToSpawnAt.y > 0)
                {
                    borderToInstantiate.GetComponent<SpriteRenderer>().sprite = border.GetComponent<Border>().bottomLeft;
                    positionToSpawnAt.x += 0.5f;
                    positionToSpawnAt.y += (0.5f + (0.5f * wallHeight));
                }
                // Bottom of map
                else
                {
                    borderToInstantiate.GetComponent<SpriteRenderer>().sprite = border.GetComponent<Border>().topLeft;
                    positionToSpawnAt.x += 0.5f;
                    positionToSpawnAt.y -= 0.5f;
                }
            }
            // Left side of map
            else
            {
                // Top of map
                if (positionToSpawnAt.y > 0)
                {
                    borderToInstantiate.GetComponent<SpriteRenderer>().sprite = border.GetComponent<Border>().bottomRight;
                    positionToSpawnAt.x -= 0.5f;
                    positionToSpawnAt.y += (0.5f + (0.5f * wallHeight));
                }
                // Bottom of map
                else
                {
                    borderToInstantiate.GetComponent<SpriteRenderer>().sprite = border.GetComponent<Border>().topRight;
                    positionToSpawnAt.x -= 0.5f;
                    positionToSpawnAt.y -= 0.5f;
                }
            }

            borderToInstantiate.GetComponent<Transform>().position = positionToSpawnAt;
            GameObject instantiatedObject = Instantiate(borderToInstantiate);
            instantiatedObject.GetComponent<Transform>().parent = GetComponent<Transform>();
            borders.Add(instantiatedObject);
        }

        // Fill in the walls from corner to corner
        for (int i = 0; i < corners.Count; i++)
        {
            Vector3 currentCornerPosition = borders[i].GetComponent<Transform>().position;
            Vector3 targetCornerPosition;

            if (i < corners.Count - 1)
            {
                // Grab the corner one position ahead of current corner
                targetCornerPosition = borders[i + 1].GetComponent<Transform>().position;
            }
            else
            {
                // Build border to first corner
                targetCornerPosition = borders[0].GetComponent<Transform>().position;
            }

            // Build border from current corner to target corner
            for (int j = 0; j < gridSize; j++)
            {
                GameObject borderToInstantiate = border;
                Vector3 positionToSpawnAt = currentCornerPosition;

                // Horizontal border
                if (currentCornerPosition.y == targetCornerPosition.y)
                {
                    // Left to right
                    if (currentCornerPosition.x < targetCornerPosition.x)
                    {
                        // Top of map
                        if (currentCornerPosition.y > 0)
                        {
                            borderToInstantiate.GetComponent<SpriteRenderer>().sprite = border.GetComponent<Border>().bottom;
                            positionToSpawnAt.x += (0.5f + (0.5f * j));
                            borderToInstantiate.GetComponent<Transform>().position = positionToSpawnAt;
                            GameObject instantiatedObject = Instantiate(borderToInstantiate);
                            instantiatedObject.GetComponent<Transform>().parent = GetComponent<Transform>();
                            borders.Add(instantiatedObject);
                        }
                        // Bottom of map
                        else
                        {
                            borderToInstantiate.GetComponent<SpriteRenderer>().sprite = border.GetComponent<Border>().top;
                            positionToSpawnAt.x += (0.5f + (0.5f * j));
                            borderToInstantiate.GetComponent<Transform>().position = positionToSpawnAt;
                            GameObject instantiatedObject = Instantiate(borderToInstantiate);
                            instantiatedObject.GetComponent<Transform>().parent = GetComponent<Transform>();
                            borders.Add(instantiatedObject);
                        }
                    }
                    // Right to left
                    else
                    {
                        // Top of map
                        if (currentCornerPosition.y > 0)
                        {
                            borderToInstantiate.GetComponent<SpriteRenderer>().sprite = border.GetComponent<Border>().bottom;
                            positionToSpawnAt.x -= (0.5f + (0.5f * j));
                            borderToInstantiate.GetComponent<Transform>().position = positionToSpawnAt;
                            GameObject instantiatedObject = Instantiate(borderToInstantiate);
                            instantiatedObject.GetComponent<Transform>().parent = GetComponent<Transform>();
                            borders.Add(instantiatedObject);
                        }
                        // Bottom of map
                        else
                        {
                            borderToInstantiate.GetComponent<SpriteRenderer>().sprite = border.GetComponent<Border>().top;
                            positionToSpawnAt.x -= (0.5f + (0.5f * j));
                            borderToInstantiate.GetComponent<Transform>().position = positionToSpawnAt;
                            GameObject instantiatedObject = Instantiate(borderToInstantiate);
                            instantiatedObject.GetComponent<Transform>().parent = GetComponent<Transform>();
                            borders.Add(instantiatedObject);
                        }
                    }
                }
            }

            // Need additional units for wall height
            for (int j = 0; j < gridSize + wallHeight; j++)
            {
                GameObject borderToInstantiate = border;
                Vector3 positionToSpawnAt = currentCornerPosition;

                // Vertical border
                if (currentCornerPosition.y != targetCornerPosition.y)
                {
                    // Bottom to top
                    if (currentCornerPosition.y < targetCornerPosition.y)
                    {
                        // Right of map
                        if (currentCornerPosition.x > 0)
                        {
                            borderToInstantiate.GetComponent<SpriteRenderer>().sprite = border.GetComponent<Border>().left;
                            positionToSpawnAt.y += (0.5f + (0.5f * j));
                            borderToInstantiate.GetComponent<Transform>().position = positionToSpawnAt;
                            GameObject instantiatedObject = Instantiate(borderToInstantiate);
                            instantiatedObject.GetComponent<Transform>().parent = GetComponent<Transform>();
                            borders.Add(instantiatedObject);
                        }
                        // Left of map
                        else
                        {
                            borderToInstantiate.GetComponent<SpriteRenderer>().sprite = border.GetComponent<Border>().right;
                            positionToSpawnAt.y += (0.5f + (0.5f * j));
                            borderToInstantiate.GetComponent<Transform>().position = positionToSpawnAt;
                            GameObject instantiatedObject = Instantiate(borderToInstantiate);
                            instantiatedObject.GetComponent<Transform>().parent = GetComponent<Transform>();
                            borders.Add(instantiatedObject);
                        }
                    }
                    // Top to bottom
                    else
                    {
                        // Right of map
                        if (currentCornerPosition.x > 0)
                        {
                            borderToInstantiate.GetComponent<SpriteRenderer>().sprite = border.GetComponent<Border>().left;
                            positionToSpawnAt.y -= (0.5f + (0.5f * j));
                            borderToInstantiate.GetComponent<Transform>().position = positionToSpawnAt;
                            GameObject instantiatedObject = Instantiate(borderToInstantiate);
                            instantiatedObject.GetComponent<Transform>().parent = GetComponent<Transform>();
                            borders.Add(instantiatedObject);
                        }
                        // Left of map
                        else
                        {
                            borderToInstantiate.GetComponent<SpriteRenderer>().sprite = border.GetComponent<Border>().right;
                            positionToSpawnAt.y -= (0.5f + (0.5f * j));
                            borderToInstantiate.GetComponent<Transform>().position = positionToSpawnAt;
                            GameObject instantiatedObject = Instantiate(borderToInstantiate);
                            instantiatedObject.GetComponent<Transform>().parent = GetComponent<Transform>();
                            borders.Add(instantiatedObject);
                        }
                    }
                }   
            }

        }

    }

    

}
