using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

    private void OnEnable()
    {
        EventHandler.ItemSelectEvent += OnItemSelectEvent;
        EventHandler.BeforeSceneLoadedEvent += OnBeforeSceneLoadedEvent;
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
    }

    private void OnDisable()
    {
        EventHandler.ItemSelectEvent -= OnItemSelectEvent;
        EventHandler.BeforeSceneLoadedEvent -= OnBeforeSceneLoadedEvent;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
    }

    private void Start()
    {
        curSprite = normal;
        cursorCanvas = GameObject.FindGameObjectWithTag("CursorCanvas").GetComponent<RectTransform>();
        cursorImage = cursorCanvas.GetChild(0).GetComponent<Image>();
        SetCursorImage(normal);

        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (cursorCanvas == null) return;
        cursorImage.transform.position = Input.mousePosition;

        if (!IsCursorInteractWithUI())
        {
            SetCursorImage(curSprite);
            CheckCursorValid();
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
        isCursorEnable= true;
        curGrid = FindObjectOfType<Grid>();
    }

    private void SetCursorImage(Sprite sprite)
    {
        cursorImage.sprite = sprite;
        cursorImage.color = new Color(1, 1, 1, 1);
    }

    private void OnItemSelectEvent(ItemDetails itemDetail, bool isSelected)
    {
        if (!isSelected)
        {
            curSprite = normal;
        }
        else
        {
            curSprite = plant;
        }
    }

    private void CheckCursorValid()
    {
        curGrid = FindObjectOfType<Grid>();
        mouseWorldPos = mainCamera.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, -mainCamera.transform.position.z));
        mouseGridPos = curGrid.WorldToCell(mouseWorldPos);

        Debug.Log("WorldPos: " + mouseWorldPos + " GridPos: " + mouseGridPos);
    }

    private bool IsCursorInteractWithUI()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
           return true;
        else
            return false;
    }
}
