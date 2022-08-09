using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Map;

public class CursorManager : MonoBehaviour
{
    public Sprite normal, plant;

    private Sprite curSprite; //Current cursor image
    private Image cursorImage;
    private RectTransform cursorCanvas;

    //Check
    private Camera mainCamera;
    private Grid curGrid;
    private Vector3 mouseWorldPos;
    private Vector3Int mouseGridPos;
    private bool isCursorEnable;
    private bool isCursorPositionValid;

    private ItemDetails curItem;
    private Transform playerTransform => GameObject.FindGameObjectWithTag("Player").transform;

    private void OnEnable()
    {
        EventHandler.ItemSelectEvent += OnItemSelectEvent;
        EventHandler.BeforeSceneUnloadedEvent += OnBeforeSceneLoadedEvent;
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
    }

    private void OnDisable()
    {
        EventHandler.ItemSelectEvent -= OnItemSelectEvent;
        EventHandler.BeforeSceneUnloadedEvent -= OnBeforeSceneLoadedEvent;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
    }

    private void Start()
    {
        cursorCanvas = GameObject.FindGameObjectWithTag("CursorCanvas").GetComponent<RectTransform>();
        cursorImage = cursorCanvas.GetChild(0).GetComponent<Image>();
        curSprite = normal;
        SetCursorImage(normal);

        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (cursorCanvas == null) return;
        cursorImage.transform.position = Input.mousePosition;

        if (isCursorEnable && !IsCursorInteractWithUI())
        {
            SetCursorImage(curSprite);
            CheckCursorValid();
            CheckPlayerInput();
        }
        else
            SetCursorImage(normal);
    }
    private void OnBeforeSceneLoadedEvent()
    {
        isCursorEnable = false;
    }

    private void OnAfterSceneLoadedEvent()
    {
        curGrid = FindObjectOfType<Grid>();
    }

    private void OnItemSelectEvent(ItemDetails itemDetail, bool isSelected)
    {
        if (!isSelected)
        {
            curItem = null;
            isCursorEnable = false;
            curSprite = normal;
        }
        else
        {
            curItem = itemDetail;
            curSprite = plant;
            isCursorEnable = true;
        }
    }

    private void CheckPlayerInput()
    {
        //On pressing mouse left key down
        if (Input.GetMouseButtonDown(0) && isCursorPositionValid)
        {
            EventHandler.CallMouseClickEvent(mouseWorldPos, curItem);
        }
    }

    #region Set the style of cursor

    /// <summary>
    /// Set the image of cursor
    /// </summary>
    /// <param name="sprite">the image to be set</param>
    private void SetCursorImage(Sprite sprite)
    {
        cursorImage.sprite = sprite;
        cursorImage.color = Color.white;
    }

    /// <summary>
    /// Set the image of cursor as valid
    /// </summary>
    private void SetCursorValid()
    {
        isCursorPositionValid = true;
        cursorImage.color = Color.white;
    }

    /// <summary>
    /// Set the image of cursor as invalid
    /// </summary>
    private void SetCursorInvalid()
    {
        isCursorPositionValid = false;
        cursorImage.color = new Color(1, 0, 0, 0.4f);
    }

    #endregion

    private void CheckCursorValid()
    {
        mouseWorldPos = mainCamera.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, -mainCamera.transform.position.z)
        );
        mouseGridPos = curGrid.WorldToCell(mouseWorldPos);

        var playerGridPos = curGrid.WorldToCell(playerTransform.position);

        //Judge usage
        var itemUseRadius = curItem.itemUseRadius;
        if (Mathf.Abs(mouseGridPos.x - playerGridPos.x) > itemUseRadius
            || Mathf.Abs(mouseGridPos.y - playerGridPos.y) > itemUseRadius)
        {
            SetCursorInvalid();
            return;
        }

        //Judge plantable
        TileDetails curTile = GridMapManager.Instance.GetTileDetailsOnMousePosition(mouseGridPos);
        
        if (curTile != null)
        {
            switch (curItem.itemType)
            {
                //TODO: 所有物品类型的判断
                case ItemType.Grass:
                case ItemType.Bush:
                case ItemType.Tree:
                    if(curTile.canPlant)    SetCursorValid();
                    break;
                default:
                    SetCursorInvalid();
                    break;
            }
        }
        else
        {
            SetCursorInvalid();
        }
    }

    private bool IsCursorInteractWithUI()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return true;
        else
            return false;
    }
}
