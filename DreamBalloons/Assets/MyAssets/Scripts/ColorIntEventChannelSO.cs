using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Color & Int Event Channel")]
public class ColorIntEventChannelSO : ScriptableObject
{
    public UnityAction<Color, int> OnEventRaised;

    public void RaiseEvent(Color valueColor, int valueInt)
    {
        OnEventRaised?.Invoke(valueColor, valueInt);
    }
}
