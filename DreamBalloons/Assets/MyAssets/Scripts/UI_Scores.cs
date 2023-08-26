using TMPro;
using UnityEngine;
using LiderboardSystem;

public class UI_Scores : MonoBehaviour
{
    public ColorEventChannelSO scoreChannel;
    public Liderboard liderboard;
    private TextMeshProUGUI textUI;
    public int curScore = 0;
    private Color lastColor;
    private int multiplier = 1;
    public TextMeshProUGUI textBonus;
    private Vector2 offsetTextBonus;

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
        textBonus.text = (Balloon.WORTH * multiplier).ToString();
        float randZ = Random.Range(45, 135);
        offsetTextBonus = new Vector2(Random.Range(-5, 5), 5);
        Instantiate(textBonus.gameObject, (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + offsetTextBonus, Quaternion.Euler(0, 0, randZ));
    }
}
