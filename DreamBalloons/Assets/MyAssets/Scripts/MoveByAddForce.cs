using UnityEngine;

[RequireComponent(typeof (Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class MoveByAddForce : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D myRig;
    [SerializeField]
    private float force = 1f;
    [SerializeField]
    private float maxSpeed = 2f;
    [SerializeField]
    private float amplitude = 0.5f;
    [SerializeField]
    private float frequency = 1f;
    private Vector2 direction = new Vector2(0, 1);
    private bool useSin;

    private void Awake() 
    {
        if (Random.value < 0.5f)
        {
            useSin = true;
        }
        else
        {
            useSin = false;
        }
    }

    private void FixedUpdate()
    {
        if (myRig.velocity.magnitude < maxSpeed)
        {
            direction = new Vector2(GetOffset(transform.position.y, useSin), direction.y).normalized;
            myRig.AddForce(direction * force);
        }        
    }

    private float GetOffset(float y, bool useSin)
    {
        if (useSin)
            return Mathf.Sin(frequency * y) * amplitude;
        else
            return Mathf.Cos(frequency * y) * amplitude;
    }
}
