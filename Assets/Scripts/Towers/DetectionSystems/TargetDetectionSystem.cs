using UnityEngine;
public abstract class TargetDetectionSystem : MonoBehaviour
{
    public abstract Vector2? GetDetectedTargetPos();
    public virtual void InitSystem() { }
    protected virtual Vector2 GetOrigin()
    {
        return transform.position;
    }
}