using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class GridMapManager : MonoBehaviour
    {
        [Header("Map Info")]
        public List<MapData_SO> mapDataList;

        //Scene name and coordinate
        private Dictionary<string, TileDetails> tileDetailsDict = new Dictionary<string, TileDetails>();

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
                string key = tileDetails.coord.ToString() + mapData.name;
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
    }
}
