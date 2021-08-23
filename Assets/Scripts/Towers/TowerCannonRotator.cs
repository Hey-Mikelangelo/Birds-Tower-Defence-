using UnityEngine;


public class TowerCannonRotator : MonoBehaviour
{
    [SerializeField] private Transform rotatableHead;
    [SerializeField] private float rotationSpeed;
    private Vector2? targetPosition;

    public void SetTarget(Vector2? targetPosition)
    {
        this.targetPosition = targetPosition;
    }

    private void Update()
    {
        if(targetPosition == null)
        {
            return;
        }
        Vector2 towerPosition = transform.position;
        Vector2 vectorToTarget = targetPosition.Value - towerPosition;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        rotatableHead.rotation = Quaternion.Lerp(rotatableHead.rotation, q, Time.deltaTime * rotationSpeed);
    }
}
