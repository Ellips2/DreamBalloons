using UnityEngine;
using LiderboardSystem;
using UnityEngine.UI;

public class GameRules : MonoBehaviour
{
    [SerializeField]
    private Liderboard liderboard;
    [SerializeField]
    private Menu menu;
    [SerializeField]
    private UI_Scores scores;
    [SerializeField]
    private BalloonEventChannelSO deathBalloonChannel;
    [SerializeField]
    private const int MAX_MISSED_BALLOON = 10;
    private int missedBalloon = 0;
    [SerializeField]
    private GameObject textDefeat;
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private Spawner spawner;
    private bool playerIsDefeat;

    private void OnEnable() 
    {
        deathBalloonChannel.OnEventRaised += CheckDefeateCondition;
    }

    private void OnDisable() 
    {
        deathBalloonChannel.OnEventRaised -= CheckDefeateCondition;
    }

    private void CheckDefeateCondition(Balloon deadBalloon)
    {
        if(deadBalloon.Health > 0 && !playerIsDefeat)
        {
            missedBalloon++;            
            healthBar.fillAmount = 1 - (1.0f / MAX_MISSED_BALLOON) * missedBalloon;
            if (missedBalloon >= MAX_MISSED_BALLOON)
            {
                playerIsDefeat = true;
                Defeate();
            }
        }        
    }

    public void Defeate()
    {
        textDefeat.SetActive(true);
        if (liderboard.Results.GetEnumerator() != null && liderboard.Results.GetEnumerator().Current.Score < scores.CurScore)
        {
            liderboard.AddResult(menu.CurName, scores.CurScore);
        }        
        scores.ResetCurScore();
        missedBalloon = 0;
        healthBar.fillAmount = 1;
        spawner.DisableAllActiveBalloons();
        playerIsDefeat = false;
    }
}
