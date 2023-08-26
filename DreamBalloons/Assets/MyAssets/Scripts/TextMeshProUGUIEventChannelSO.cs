using TMPro;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/TextMeshProUGUI Event Channel")]
public class TextMeshProUGUIEventChannelSO : ScriptableObject
{
    public UnityAction<TextMeshProUGUI> OnEventRaised;

    public void RaiseEvent(TextMeshProUGUI text)
    {
        OnEventRaised?.Invoke(text);
    }
}
