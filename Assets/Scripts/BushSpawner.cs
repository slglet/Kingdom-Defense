using UnityEngine;
using UnityEngine.Tilemaps;

public class BushSpawner : MonoBehaviour
{
    public Tilemap bushTilemap;
    public TileBase bushTile;

    public int width = 8;
    public int height = 8;
    [Range(0f, 1f)]
    public float bushChance = 0.5f; // 50% of tiles get bushes

    void Start()
    {
        GenerateBushes();
    }

    void GenerateBushes()
    {
        bushTilemap.ClearAllTiles(); // optional: clears old bushes

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (Random.value < bushChance)
                {
                    Vector3Int position = new Vector3Int(x, y, 0);
                    bushTilemap.SetTile(position, bushTile);
                }
            }
        }
    }
}