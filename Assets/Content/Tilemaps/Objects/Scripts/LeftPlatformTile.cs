using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "LeftPlatformTile", menuName = "Tiles/LeftPlatformTile")]
public class LeftPlatformTile : Tile
{
    public GameObject LeftPlatform;
    
    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        if (Application.isPlaying && LeftPlatform != null)
        {
            Tilemap tilemapComponent = tilemap.GetComponent<Tilemap>();
            Vector3 worldPos = tilemapComponent.CellToWorld(position) + new Vector3(0.5f, 0.5f, 0);

            if (!ObjectExistsAtPosition(worldPos))
            {
                GameObject goldInstance = Instantiate(LeftPlatform, worldPos, Quaternion.identity);
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