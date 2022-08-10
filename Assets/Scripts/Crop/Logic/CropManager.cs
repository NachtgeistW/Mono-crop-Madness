using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CropPlant
{
    public class CropManager : MonoBehaviour
    {
        public CropDataList_SO cropData;
        private Transform cropParent;
        private Grid curGrid;

        private void OnEnable()
        {
            EventHandler.PlantSeedEvent += OnPlantSeedEvent;
            EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        }

        private void OnDisable()
        {
            EventHandler.PlantSeedEvent -= OnPlantSeedEvent;
            EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        }

        private void OnAfterSceneLoadedEvent()
        {
            curGrid = FindObjectOfType<Grid>();
            cropParent = GameObject.FindWithTag("CropParent").transform;
        }

        private void OnPlantSeedEvent(int seedID, TileDetails tileDetails)
        {
            CropDetails curCrop = GetCropDetails(seedID);
            //if find the seed and there is no seed on the tile
            //for first plant
            if (curCrop != null && tileDetails.seedID == -1)
            {
                tileDetails.seedID = seedID;
                tileDetails.growthDays = 0;
                //显示农作物
                DisplayCropPlant(tileDetails, curCrop);
            }
            //refresh map
            else if (tileDetails.seedID != -1)
            {
                //显示农作物
                DisplayCropPlant(tileDetails, curCrop);
            }
        }

        /// <summary>
        /// Display crop plant on the scene
        /// </summary>
        /// <param name="tileDetails">tile details info</param>
        /// <param name="cropDetails">seed info</param>
        private void DisplayCropPlant(TileDetails tileDetails, CropDetails cropDetails)
        {
            //On crop growing
            int growthStages = cropDetails.growthDays.Length;
            int curStage = 0;
            int dayCounter = cropDetails.TotalGrowthDays;

            //倒序计算当前的成长阶段
            for (int i = growthStages -1; i >= 0; i--)
            {
                //last stage
                if (tileDetails.growthDays >= dayCounter)
                {
                    curStage = i;
                    //TODO: fix the bug
                    EventHandler.CallCropFullyGrowthEvent(cropDetails.seedItemID, tileDetails);
                    break;
                }
                dayCounter -= cropDetails.growthDays[i];
            }

            //Get prefab on current stage
            GameObject cropPrefab = cropDetails.growthPrefabs[curStage];
            Sprite cropSprite = cropDetails.growthSprites[curStage];

            //Coord
            Vector3 pos = new Vector3(tileDetails.coord.x + 0.5f, tileDetails.coord.y + 0.5f, 0);

            GameObject cropInstance = Instantiate(cropPrefab, pos, Quaternion.identity, cropParent);
            cropInstance.GetComponentInChildren<SpriteRenderer>().sprite = cropSprite;
        }

        /// <summary>
        /// Find crop info by item ID
        /// </summary>
        /// <param name="ID">item ID</param>
        /// <returns></returns>
        private CropDetails GetCropDetails(int ID) 
            => cropData.cropDetailsList.Find(c => c.seedItemID == ID);
    }

}
