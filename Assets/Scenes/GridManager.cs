using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject player;
    public int width;
    public int height;
    public int tileSpacing;
    public GameObject tilePrefab; // The prefab for your tiles

    private GameObject[,] gridArray;
    private Transform gridParent;

    void Start()
    {
        gridParent = new GameObject("GridParent").transform; // Create a new parent object for the grid tiles
        CreateGrid();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Use GetKeyDown to create a new grid only once per space press
        {
            DestroyGrid();
            CreateGrid(); 
        }
    }
    public float xspriteoffset;
    public float yspriteoffset;
    void CreateGrid()
    {
        gridArray = new GameObject[width, height];

        Vector3 playerCenterPosition = player.transform.position + new Vector3(player.GetComponent<SpriteRenderer>().bounds.size.x / 2f, player.GetComponent<SpriteRenderer>().bounds.size.y / 2f, 0f);
        Vector3 gridStartPosition = playerCenterPosition - new Vector3((width - 1) * tileSpacing / 2f, (height - 1) * tileSpacing / 2f, 0f);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 tilePosition = gridStartPosition + new Vector3(x * tileSpacing, y * tileSpacing, 0);
                GameObject tile = Instantiate(tilePrefab, tilePosition, Quaternion.identity);
                gridArray[x, y] = tile;
                tile.transform.parent = gridParent; // Set the gridParent as the parent of the tiles
            }
        }
    }
    void DestroyGrid()
    {
        if (gridArray != null)
        {
            foreach (GameObject tile in gridArray)
            {
                Destroy(tile);
            }
        }
    }

}


