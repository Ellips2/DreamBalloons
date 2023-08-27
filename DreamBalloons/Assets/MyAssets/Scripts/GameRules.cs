using UnityEngine;
using LiderboardSystem;

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
        if(deadBalloon.Health > 0)
        {
            missedBalloon++;
            if (missedBalloon >= MAX_MISSED_BALLOON)
            {
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
    }
}
