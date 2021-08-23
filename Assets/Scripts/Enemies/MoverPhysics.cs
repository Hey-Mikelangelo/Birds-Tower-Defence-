using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoverPhysics : Mover
{
    private Rigidbody2D rigidbody2d;

    private WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    protected void Start()
    {
        StartCoroutine(WaitForFixedUpdate(LateFixedUpdate));
    }

    protected override void MoveTowards(Vector2 movementVector)
    {
        rigidbody2d.velocity += movementVector;
    }

    protected void LateFixedUpdate()
    {
        rigidbody2d.velocity = Vector2.zero;
    }

    private IEnumerator WaitForFixedUpdate(System.Action action)
    {
        while (true)
        {
            action.Invoke();
            yield return waitForFixedUpdate;
        }
    }
    
}
