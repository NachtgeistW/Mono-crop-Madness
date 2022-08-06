using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        private void OnEnable()
        {
            EventHandler.ExecuteActionAfterAnimation += OnExecuteActionAfterAnimation;
            EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        }

        private void OnDisable()
        {
            EventHandler.ExecuteActionAfterAnimation -= OnExecuteActionAfterAnimation;
            EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
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
                //TODO
                switch (itemDetails.itemType)
                {
                    case ItemType.Grass:
                    case ItemType.Bush:
                    case ItemType.Tree:
                        EventHandler.CallDropItemEvent(itemDetails.itemID, mouseWorldPos);
                        break;
                    default:
                        break;
                }
            }
        }

        
    }
}
