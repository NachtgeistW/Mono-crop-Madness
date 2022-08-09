using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

namespace Map
{
    public class GridMapManager : Singleton<GridMapManager>
    {
        [Header("Map Info")]
        public List<MapData_SO> mapDataList;

        //Scene name and coordinate
        private Dictionary<string, TileDetails> tileDetailsDict = new Dictionary<string, TileDetails>();

        private Grid curGrid;

        [Header("Plant Tilemaps")]
        public RuleTile plantTile;
        private Tilemap plantTilemap;

        private void OnEnable()
        {
            EventHandler.ExecuteActionAfterAnimation += OnExecuteActionAfterAnimation;
            EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
            EventHandler.GameDayEvent += OnGameDayEvent;
        }

        private void OnDisable()
        {
            EventHandler.ExecuteActionAfterAnimation -= OnExecuteActionAfterAnimation;
            EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
            EventHandler.GameDayEvent -= OnGameDayEvent;
        }

        private void Start()
        {
            foreach (var mapData in mapDataList)
            {
                InitTileDetailsDict(mapData);
            }
        }

        private void InitTileDetailsDict(MapData_SO mapData)
        {
            foreach (TileProperty tileProperty in mapData.tileProperties)
            {
                TileDetails tileDetails = new TileDetails
                {
                    coord = new Vector2Int(tileProperty.tileCoordinate.x, tileProperty.tileCoordinate.y)
                };
                
                //Set the tile details data
                string key = tileDetails.coord.ToString() + mapData.sceneName;
                if (GetTileDetails(key) != null)
                {
                    tileDetails = GetTileDetails(key);
                }
                switch (tileProperty.gridType)
                {
                    case GridType.Plantable:
                        tileDetails.canPlant = true;
                        break;
                    case GridType.NPCObstacle:
                        tileDetails.isNPCObstacle = true;
                        break;
                    default:
                        break;
                }

                //Update detail or Add to dict
                if (GetTileDetails(key) != null)
                    tileDetailsDict[key] = tileDetails;
                else
                    tileDetailsDict.Add(key, tileDetails);
            }
        }

        private TileDetails GetTileDetails(string key)
        {
            if (tileDetailsDict.ContainsKey(key))
            {
                return tileDetailsDict[key];
            }
            else return null;
        }

        /// <summary>
        /// Return TileDetails by mouse position on grid
        /// </summary>
        /// <param name="mouseGridPos">mouse position on grid</param>
        /// <returns></returns>
        public TileDetails GetTileDetailsOnMousePosition(Vector3Int mouseGridPos)
        {
            string key = new Vector2Int(mouseGridPos.x, mouseGridPos.y).ToString() + SceneManager.GetActiveScene().name;
            return GetTileDetails(key);
        }

        private void OnAfterSceneLoadedEvent()
        {
            curGrid = FindObjectOfType<Grid>();
            plantTilemap = GameObject.FindWithTag("Dig").GetComponent<Tilemap>();

            RefreshMap();
        }

        /// <summary>
        /// Execute everyday
        /// </summary>
        /// <param name="day"></param>
        private void OnGameDayEvent(int day)
        {
            foreach (var tile in tileDetailsDict)
            {
                if (tile.Value.daySincePlanted > -1)
                {
                    tile.Value.daySincePlanted++;
                }
                if (tile.Value.seedID != -1)
                {
                    tile.Value.growthDays++;
                }
            }
            RefreshMap();
        }

        /// <summary>
        /// Execute the actual item function
        /// </summary>
        /// <param name="mouseWorldPos">mouse position</param>
        /// <param name="itemDetails">item info</param>
        private void OnExecuteActionAfterAnimation(Vector3 mouseWorldPos, ItemDetails itemDetails)
        {
            var mouseGridPos = curGrid.WorldToCell(mouseWorldPos);
            var curTile = GetTileDetailsOnMousePosition(mouseGridPos);

            if (curTile != null)
            {
                switch (itemDetails.itemType)
                {
                    //TODO: workflow about using item
                    case ItemType.Grass:
                    case ItemType.Bush:
                    case ItemType.Tree:
                        EventHandler.CallDropItemEvent(itemDetails.itemID, mouseWorldPos, itemDetails.itemType);
                        EventHandler.CallPlantSeedEvent(itemDetails.itemID, curTile);
                        SetPlantableGround(curTile);
                        curTile.daySincePlanted = 0;
                        curTile.canPlant = false;
                        break;
                    default:
                        break;
                }
                //update map
                UpdateTileDetails(curTile);
            }
        }

        /// <summary>
        /// Show dug tilemap
        /// </summary>
        /// <param name="tile"></param>
        private void SetPlantableGround(TileDetails tile)
        {
            Vector3Int pos = new Vector3Int(tile.coord.x, tile.coord.y, 0);
            if (plantTilemap != null)
            {
                plantTilemap.SetTile(pos, plantTile);
            }
        }
        
        private void UpdateTileDetails(TileDetails tileDetails)
        {
            string key = tileDetails.coord.ToString() + SceneManager.GetActiveScene().name;
            if(tileDetailsDict.ContainsKey(key))
            {
                tileDetailsDict[key] = tileDetails;
            }
        }

        /// <summary>
        /// Refresh tile on map
        /// </summary>
        private void RefreshMap()
        {
            if (plantTilemap != null)
                plantTilemap.ClearAllTiles();

            foreach (var crop in FindObjectsOfType<Crop>())
            {
                Destroy(crop.gameObject);
            }

            DisplayMapInfo(SceneManager.GetActiveScene().name);
        }

        /// <summary>
        /// Show tile on map
        /// </summary>
        /// <param name="sceneName">scene map</param>
        private void DisplayMapInfo(string sceneName)
        {
            foreach(var tile in tileDetailsDict)
            {
                var key = tile.Key;
                var tileDetails = tile.Value;

                if (key.Contains(sceneName))
                {
                    if (tileDetails.daySincePlanted > -1)
                        SetPlantableGround(tileDetails);
                    if (tileDetails.seedID > -1)
                        EventHandler.CallPlantSeedEvent(tileDetails.seedID, tileDetails);
                }
            }
        }
    }
}
