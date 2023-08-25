using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.TryGetComponent(out Balloon balloon)){
            balloon.Death(false);
        }
    }
}
