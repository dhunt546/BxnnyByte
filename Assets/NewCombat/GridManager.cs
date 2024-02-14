using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject player;
    public int width;
    public int height;
    public static int tileSpacing = 2;
    public GameObject tilePrefab; // The prefab for your tiles
    public float tileSize = 1f;
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

    void CreateGrid()
    {
        gridArray = new GameObject[width, height];

        Vector3 playerCenterPosition = player.transform.position + new Vector3(player.GetComponent<SpriteRenderer>().bounds.size.x / 2f, player.GetComponent<SpriteRenderer>().bounds.size.y / 2f, 0f);
        Vector3 gridStartPosition = playerCenterPosition - new Vector3((width - 0.5f) * tileSpacing / 2f, (height - 0.5f) * tileSpacing / 2f, 0f);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 tilePosition = gridStartPosition + new Vector3(x * tileSpacing, y * tileSpacing, 0);
                GameObject tile = Instantiate(tilePrefab, tilePosition, Quaternion.identity);
                gridArray[x, y] = tile;
                tile.transform.parent = gridParent; // Set the gridParent as the parent of the tiles
                tile.GetComponent<TileScript>().SetDodgeableTile(playerCenterPosition.x);
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
    // Function to convert world coordinates to grid coordinates
    public Vector3Int WorldToGridCoordinates(Vector3 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / tileSize);
        int y = Mathf.FloorToInt(worldPosition.y / tileSize);
        return new Vector3Int(x, y, 0);
    }

    // Function to convert grid coordinates to world coordinates
    public Vector3 GridToWorldCoordinates(Vector3Int gridPosition)
    {
        float x = (gridPosition.x + 0.5f) * tileSize;
        float y = (gridPosition.y + 0.5f) * tileSize;
        return new Vector3(x, y, 0);
    }

    // Function to check if a tile is walkable
  //public bool IsTileWalkable(Vector3Int gridPosition)
  //{
  //    // Check if the grid position is within the grid boundaries
  //    if (gridPosition.x >= 0 && gridPosition.x < width && gridPosition.y >= 0 && gridPosition.y < height)
  //    {
  //        // Check if there is an obstacle at the grid position using raycasting
  //        Vector3 worldPosition = GridToWorldCoordinates(gridPosition);
  //        RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero, 0f, obstacleMask);
  //        return hit.collider == null; // Return true if there is no obstacle
  //    }
  //    else
  //    {
  //        // Grid position is outside the grid boundaries
  //        return false;
  //    }
  //}

}


