using TMPro;
using UnityEngine;

public class UI_Scores : MonoBehaviour
{
    public VoidEventChannelSO deathChannel;
    private TextMeshProUGUI textUI;
    private int scores = 0;

    private void Awake()
    {
        textUI = GetComponent<TextMeshProUGUI>();
        deathChannel.OnEventRaised += RefreshUI;
    }

    private void OnDisable()
    {
        deathChannel.OnEventRaised -= RefreshUI;
    }

    private void RefreshUI()
    {
        scores++;
        textUI.text = ""+scores;
    }
}
