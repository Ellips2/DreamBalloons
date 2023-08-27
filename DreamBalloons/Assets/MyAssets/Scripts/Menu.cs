using UnityEngine;
using LiderboardSystem;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private float oldTimeScale;
    [SerializeField]
    private Text textLiderBoard;
    [SerializeField]
    private Liderboard liderboard;
    [SerializeField]
    private Text userNameUI;
    [SerializeField]
    private Text textPrefixUserName;
    [SerializeField]
    private Text inputField;
    [SerializeField]
    private Canvas canvasInGame;
    [SerializeField]
    private Canvas canvasUsername;
    private string curName = "";
    public string CurName => curName;
    
    private void Start() 
    {
        liderboard.Initialize();
        RefreshUserName();
        PauseGame(true);
    }

    public void OpenMenu()
    {        
        gameObject.SetActive(true);
        PauseGame(true);
    }

    public void CloseMenu()
    {        
        if (curName == "")
        {
            canvasUsername.gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
            PauseGame(false);
        }
    }

    public void PauseGame(bool enable)
    {
        if (enable)
        {
            canvasInGame.gameObject.SetActive(false);
            oldTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }
        else
        {
            canvasInGame.gameObject.SetActive(true);
            Time.timeScale = oldTimeScale;
            Time.fixedDeltaTime *= Time.timeScale;
        }
    }

    public void AcceptNewName()
    {
        curName = inputField.text;
        RefreshUserName();
    }

    public void RefreshUserName()
    {
        userNameUI.text = textPrefixUserName.text + curName;
    }

    public void RefreshTextUI_Scores()
    {
        textLiderBoard.text = "";
        foreach (var res in liderboard.Results)
            textLiderBoard.text += res.ToString()+"\n";
    }

    public void ClearScores()
    {
        liderboard.Clean();
        RefreshTextUI_Scores();
    }

    public void ExitFromeGame()
    {
        Application.Quit();
    }
}
