using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMovementDirection : MonoBehaviour
{
    private Vector2 prevPos;

    private void Start()
    {
        prevPos = transform.position;
    }
    private void FixedUpdate()
    {
        Vector2 vectorToTarget = (Vector2)transform.position - prevPos;
        prevPos = transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = q;
    }
}
