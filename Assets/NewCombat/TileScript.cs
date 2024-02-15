using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    GameObject player;

    public enum TileState
    {
        PlayerTile,
        MeleeTile,
        LungeTile,
        FarTile
    }
    public TileState tileState = TileState.FarTile;
    public bool isPlayerTile = false;

    public static Color GreenColor = new Color(0f, 1f, 0f, 0.8f);
    public static Color OrangeColor = new Color(1f, 0.5f, 0f, 0.8f);
    public static Color RedColor = new Color(1f, 0f, 0f, 0.8f);
    public static Color WhiteColor = new Color(1f, 1f, 1f, 0.8f);

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");
        
    }


    private void Update()
    {
        SetTileState(player.transform.position);
        TileMath();
    }

    // Function to determine the distance from the player and change the tile color accordingly
    public void SetTileState(Vector3 playerPosition)
    {
        float distance = Vector3.Distance(playerPosition, transform.position);
        // Calculate the number of tiles away from the player based on the tile spacing
        int tilesAway = Mathf.RoundToInt(distance / GridManager.tileSpacing);

        // Determine the tile state based on the distance from the player
        if (tilesAway == 0)
        {
            tileState = TileState.PlayerTile;
        }
        else if (tilesAway == 1)
        {
            tileState = TileState.MeleeTile;
        }
        else if (tilesAway == 2)
        {
            tileState = TileState.LungeTile;
        }
        else
        {
            tileState = TileState.FarTile;
        }
    }
    public bool isDodgingTile = false;
    public void SetDodgeableTile(float playerCenterPosition)
    {
        float distanceX = (Mathf.Abs(transform.position.x - playerCenterPosition));

        // Check if the current tile is the player's tile, or one of the tiles to the left or right
        if (Mathf.Approximately(distanceX, 0.5f) || Mathf.Approximately(distanceX, 2.5f) || Mathf.Approximately(distanceX, 1.5f))
        {
            isDodgingTile = true; // Set the flag indicating it's a dodging tile
        }

    }

    public void TileMath()
    {
        switch (tileState)
        {
            case TileState.PlayerTile:
                isPlayerTile = true;
                spriteRenderer.color = GreenColor;
                // Add any actions specific to the player tile
                break;
            case TileState.MeleeTile:
                isPlayerTile = false;
                spriteRenderer.color = RedColor;
                // Add any actions specific to the melee tile
                break;
            case TileState.LungeTile:
                isPlayerTile = false;
                spriteRenderer.color = OrangeColor;
                // Add any actions specific to the lunge tile
                break;
            case TileState.FarTile:
                isPlayerTile = false;
                spriteRenderer.color = WhiteColor;
                // Add any actions specific to the far tile
                break;
            default:
                break;
        }
    }
}


