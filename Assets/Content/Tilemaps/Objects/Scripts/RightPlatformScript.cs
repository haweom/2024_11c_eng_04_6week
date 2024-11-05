using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "RightPlatformTile", menuName = "Tiles/RightPlatformTile")]
public class RightPlatformTile : Tile
{
    public GameObject RightPlatform;
    
    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        if (Application.isPlaying && RightPlatform != null)
        {
            Tilemap tilemapComponent = tilemap.GetComponent<Tilemap>();
            Vector3 worldPos = tilemapComponent.CellToWorld(position) + new Vector3(0.5f, 0.5f, 0);

            if (!ObjectExistsAtPosition(worldPos))
            {
                GameObject goldInstance = Instantiate(RightPlatform, worldPos, Quaternion.identity);
                goldInstance.transform.parent = tilemapComponent.transform;
            }

            tilemapComponent.SetTile(position, null);
        }
        return base.StartUp(position, tilemap, go);
    }

    private bool ObjectExistsAtPosition(Vector3 position)
    {
        Collider2D collider = Physics2D.OverlapPoint(position);
        return collider != null && collider.gameObject.CompareTag("CoinGold");
    }
}