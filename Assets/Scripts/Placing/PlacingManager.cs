using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlacingManager : MonoBehaviour
{
    [SerializeField] private int initialCoinCount;
    [SerializeField] private UnsignedIntValueSO coinsCount;
    [SerializeField] private List<PlacableObjectSO> placableObjects = new List<PlacableObjectSO>();
    [SerializeField] private Grid placingPositionsGrid;
    [SerializeField] private List<Tilemap> placingPositionTilemaps = new List<Tilemap>();
    [SerializeField] private new Camera camera;

    private IPlacable selectedPlacable;
    private PlacableObjectSO selectedPlacebleObjectSO;
    private PlacingPositionTile currentMousePointingPositionTile;
    private Vector3Int currentMousePointingCell;
    private Vector3Int prevMousePoitingCell;
    private readonly List<Vector3Int> occupiedCells = new List<Vector3Int>();

    private void Start()
    {
        coinsCount.SetValue(initialCoinCount);
    }
    private void OnEnable()
    {
        foreach (var placableObj in placableObjects)
        {
            placableObj.onSpawnButtonPressed += PlacableObj_onSpawnButtonPressed;
        }
    }

    private void OnDisable()
    {
        foreach (var placableObj in placableObjects)
        {
            placableObj.onSpawnButtonPressed -= PlacableObj_onSpawnButtonPressed;
        }
    }

    private void Update()
    {
        currentMousePointingCell = GetMousePointingGridCell();

        if (prevMousePoitingCell != currentMousePointingCell)
        {
            prevMousePoitingCell = currentMousePointingCell;

            currentMousePointingPositionTile = GetPlacingPositionTile(currentMousePointingCell);
            Vector3 currentMousePointingCellCenter = placingPositionsGrid.GetCellCenterWorld(currentMousePointingCell);
            if (selectedPlacable != null)
            {
                bool canPlace = CanPlace();
                selectedPlacable.SetCanPlaceVisuals(canPlace);
                selectedPlacable.GetGameObject().transform.position = currentMousePointingCellCenter;
            }
        }

        //Accept
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (CanPlace())
            {
                PlaceCurrentPlacable();
            }
        }
        //Discard
        else if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            DiscardCurrentPlacable();
        }
    }

    private void PlacableObj_onSpawnButtonPressed(PlacableObjectSO placableObjectSO)
    {
        if (placableObjectSO.Cost > coinsCount)
        {
            return;
        }
        if (selectedPlacable != null)
        {
            DiscardCurrentPlacable();
        }
        selectedPlacable = placableObjectSO.NewPlacableObject;
        selectedPlacable.GetGameObject().transform.parent = this.transform;
        selectedPlacebleObjectSO = placableObjectSO;
        prevMousePoitingCell = new Vector3Int(-99999, -999999, -1);
    }

    private void PlaceCurrentPlacable()
    {
        coinsCount.RemoveValue(selectedPlacebleObjectSO.Cost);
        occupiedCells.Add(currentMousePointingCell);
        selectedPlacable.PlaceOnMap();
        selectedPlacable.onRemovedFromPosition += SelectedPlacable_onRemovedFromPosition;
        selectedPlacable = null;
    }

    private void DiscardCurrentPlacable()
    {
        if (selectedPlacable != null)
        {
            selectedPlacable.Discard();
            selectedPlacable = null;
        }
    }

    private Vector3Int GetMousePointingGridCell()
    {
        Vector3 mouseWorldPoint = GetMouseWorldPoint();
        Vector3Int mousePointingCell = WorldToCell(mouseWorldPoint);
        return mousePointingCell;
    }

    private Vector3Int WorldToCell(Vector3 worldPosition)
    {
        return placingPositionsGrid.WorldToCell(worldPosition);
    }

    private PlacingPositionTile GetPlacingPositionTile(Vector3Int cell)
    {
        foreach (Tilemap tilemap in placingPositionTilemaps)
        {
            TileBase tile = tilemap.GetTile(cell);
            if (tile != null)
            {
                return tile as PlacingPositionTile;
            }
        }
        return null;
    }

    private Vector3 GetMouseWorldPoint()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(mousePosition);
    }

  
    private void SelectedPlacable_onRemovedFromPosition(Vector3 removedPlacableWorldPos)
    {
        Vector3Int gridPos = WorldToCell(removedPlacableWorldPos);
        occupiedCells.Remove(gridPos);
    }
   
    private bool CanPlace()
    {
        if (currentMousePointingPositionTile == null || selectedPlacebleObjectSO == null)
        {
            return false;
        }
        return
            occupiedCells.Contains(currentMousePointingCell) == false &&
            currentMousePointingPositionTile.PlacableType == selectedPlacebleObjectSO.PlacableType;

    }

}
