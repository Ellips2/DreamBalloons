using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private ExplosionEventChannelSO explosionChannel;

    public void EndOfExplosion()
    {
        if (explosionChannel != null)
            explosionChannel.RaiseEvent(this);
    }
}
