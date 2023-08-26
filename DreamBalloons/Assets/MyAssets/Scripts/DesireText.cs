using TMPro;
using UnityEngine;

public class DesireText : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUIEventChannelSO desireTextChannel;
    [SerializeField]
    private TextMeshProUGUI myText;

    public void DesiretextIsOff()
    {
        if (desireTextChannel != null)
            desireTextChannel.RaiseEvent(myText);
    }
}
