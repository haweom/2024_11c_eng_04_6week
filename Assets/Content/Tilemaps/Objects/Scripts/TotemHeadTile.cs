using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TotemHeadTile", menuName = "Tiles/Totem Head Tile")]
public class TotemHeadTile : Tile
{
    public GameObject totemHead;
    
    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        if (Application.isPlaying && totemHead != null)
        {
            Tilemap tilemapComponent = tilemap.GetComponent<Tilemap>();
            Vector3 worldPos = tilemapComponent.CellToWorld(position) + new Vector3(0.5f, 0.5f, 0);

            if (!ObjectExistsAtPosition(worldPos))
            {
                GameObject spikesInstance = Instantiate(totemHead, worldPos, Quaternion.identity);
                spikesInstance.transform.parent = tilemapComponent.transform;
            }

            tilemapComponent.SetTile(position, null);
        }

        return base.StartUp(position, tilemap, go);
    }

    private bool ObjectExistsAtPosition(Vector3 position)
    {
        Collider2D collider = Physics2D.OverlapPoint(position);
        return collider != null && collider.gameObject.name == "TotemHead";
    }
}