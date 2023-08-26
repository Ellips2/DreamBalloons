using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Explosion Event Channel")]
public class ExplosionEventChannelSO : ScriptableObject
{
    public UnityAction<Explosion> OnEventRaised;

    public void RaiseEvent(Explosion explosion)
    {
        OnEventRaised?.Invoke(explosion);
    }
}
