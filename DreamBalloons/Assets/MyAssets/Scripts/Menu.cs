using UnityEngine;
using LiderboardSystem;
using System;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private float oldTimeScale;
    [SerializeField]
    private Text textLiderBoard;
    [SerializeField]
    private Liderboard liderboard;
    private string curName = "";
    public string CurName => curName;
    [SerializeField]
    private Text userNameUI;
    [SerializeField]
    private Text textPrefixUserName;
    [SerializeField]
    private Text inputField;
    private const string key = "Username";
    [SerializeField]
    private Canvas canvasInGame;
    [SerializeField]
    private Canvas canvasUsername;

    private void Start() 
    {
        RefreshUserName();
        PauseGame(true);
        Load();
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
        Save();
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

    private void Load()
    {
        string json = PlayerPrefs.GetString(key);
        Debug.Log(json);
        if (string.IsNullOrEmpty(json))
            curName = "";
        else
            curName = JsonUtility.FromJson<String>(json);
        userNameUI.text += curName;
    }

    private void Save()
    {
        string json = JsonUtility.ToJson(curName);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }
}
