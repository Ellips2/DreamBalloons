using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Color Event Channel")]
public class ColorEventChannelSO : ScriptableObject
{
    public UnityAction<Color> OnEventRaised;

    public void RaiseEvent(Color color)
    {
        OnEventRaised?.Invoke(color);
    }
}
