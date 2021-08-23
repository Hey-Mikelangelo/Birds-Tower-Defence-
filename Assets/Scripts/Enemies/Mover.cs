using UnityEngine;

public abstract class Mover : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    public float Speed => speed * speedMult;

    private float speedMult = 1;
    public void Move(Vector2 moveDirection, float maxDistanceThisFrame = float.MaxValue)
    {
        float movementSpeed = Mathf.Clamp(Speed, 0, maxDistanceThisFrame / Time.deltaTime);
        MoveTowards(moveDirection.normalized * movementSpeed);
    }

    public void SetSpeedMultiplier(float mult)
    {
        mult = Mathf.Clamp01(mult);
        speedMult = mult;
    }
    public void ResetNormalSpeedMultiplier()
    {
        speedMult = 1;
    }
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    protected abstract void MoveTowards(Vector2 movementVector);
}
