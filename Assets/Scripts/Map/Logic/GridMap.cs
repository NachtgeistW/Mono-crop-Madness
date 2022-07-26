using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class GridMap : MonoBehaviour
{
    public MapData_SO mapData;
    public GridType gridType;
    private Tilemap curTilemap;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        if (!Application.IsPlaying(this))
        {
            curTilemap = GetComponent<Tilemap>();
            if (mapData != null)
                mapData.tileProperties.Clear();
        }
    }

    private void OnDisable()
    {
        if (!Application.IsPlaying(this))
        {
            curTilemap = GetComponent<Tilemap>();
            UpdateTileProperties();
#if UNITY_EDITOR
            if (mapData != null)
            {
                //You can use SetDirty when you want to modify an object without creating an undo entry,
                //but still ensure the change is registered and not lost.
                //If the object is part of a Scene, the Scene is marked dirty.
                EditorUtility.SetDirty(mapData);
            }
#endif
        }
    }

    private void UpdateTileProperties()
    {
        curTilemap.CompressBounds();

        if (!Application.IsPlaying(this))
        {
            if (mapData != null)
            {
                //Left bottom coordinate of the drawn tilemap
                var startPos = curTilemap.cellBounds.min;
                //Right top coordinate of the drawn tilemap
                var endPos = curTilemap.cellBounds.max;

                for (int x = startPos.x; x < endPos.x; x++)
                {
                    for (int y = startPos.y; y < endPos.y; y++)
                    {
                        TileBase tile = curTilemap.GetTile(new Vector3Int(x, y, 0));
                        if (tile != null)
                        {
                            var newTile = new TileProperty
                            {
                                tileCoordinate = new Vector2Int(x, y),
                                gridType = gridType,
                                boolTypeValue = true,
                            };
                            mapData.tileProperties.Add(newTile);
                        }
                    }
                }
            }
        }
    }
}
