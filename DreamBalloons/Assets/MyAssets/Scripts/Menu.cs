using TMPro;
using UnityEngine;
using LiderboardSystem;

public class Menu : MonoBehaviour
{
    private float oldTimeScale;
    [SerializeField]
    private TextMeshProUGUI textLedearBoard;
    [SerializeField]
    private Liderboard liderboard;

    public void OpenMenu()
    {        
        gameObject.SetActive(true);
        PauseGame(true);
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
        PauseGame(false);
    }

    public void PauseGame(bool enable)
    {
        if (enable)
        {
            oldTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = oldTimeScale;
            Time.fixedDeltaTime *= Time.timeScale;
        }
    }

    public void RefreshTextUI_Scores()
    {
        //liderboard.AddResult();
        textLedearBoard.text = liderboard.Results.ToString();
    }

    public void ExitFromeGame()
    {
        Application.Quit();
    }
}
