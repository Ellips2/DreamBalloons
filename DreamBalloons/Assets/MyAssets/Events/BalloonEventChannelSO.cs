using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Balloon Event Channel")]
public class BalloonEventChannelSO : ScriptableObject
{
    public UnityAction<Balloon> OnEventRaised;

    public void RaiseEvent(Balloon balloon)
    {
        OnEventRaised?.Invoke(balloon);
    }
}
