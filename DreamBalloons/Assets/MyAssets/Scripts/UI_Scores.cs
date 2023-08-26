using TMPro;
using UnityEngine;
using LiderboardSystem;

public class UI_Scores : MonoBehaviour
{
    [SerializeField]
    private ColorEventChannelSO scoreChannel;
    [SerializeField]
    private Liderboard liderboard;
    [SerializeField]
    private TextMeshProUGUI textUI;
    [SerializeField]
    private TextMeshProUGUI textBonus;
    [SerializeField]
    private Canvas canvas;

    private int curScore = 0;
    private Color lastColor;
    private int multiplier = 1;    

    private void Awake()
    {
        textUI = GetComponent<TextMeshProUGUI>();
        scoreChannel.OnEventRaised += RefreshUI;
    }

    private void OnDisable()
    {
        scoreChannel.OnEventRaised -= RefreshUI;
    }

    private void RefreshUI(Color color)
    {
        CalculateMultiplier(color);

        curScore += Balloon.WORTH * multiplier;
        textUI.text = curScore.ToString();

        CreateAddedScoreText();
    }

    private void CalculateMultiplier(Color color)
    {
        if (lastColor == color)
            multiplier++;
        else
            multiplier = 1;
        lastColor = color;
    }

    private void CreateAddedScoreText()
    {
        textBonus.text = "+" + (Balloon.WORTH * multiplier);
        textBonus.color = lastColor;

        float randAngle = 0;
        Vector2 offsetTextBonus = new Vector2(0, 100);

        if (Random.value < 0.5f)
        {
            randAngle = 25;
            offsetTextBonus = new Vector2(-100, 100);
        }
        else
        {
            randAngle = -25;
            offsetTextBonus = new Vector2(100, 100);
        }

        Instantiate<TextMeshProUGUI>(textBonus, (Vector2)(Input.mousePosition) + offsetTextBonus, Quaternion.Euler(0, 0, randAngle), canvas.transform);
    }
}
