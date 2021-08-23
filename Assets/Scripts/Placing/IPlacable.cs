using UnityEngine;

public interface IPlacable
{
    event System.Action<Vector3> onRemovedFromPosition;
    GameObject GetGameObject();
    void Discard();
    void PlaceOnMap();
    void SetCanPlaceVisuals(bool canPlace);
}