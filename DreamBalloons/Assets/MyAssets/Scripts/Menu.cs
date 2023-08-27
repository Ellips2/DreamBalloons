using TMPro;
using UnityEngine;
using LiderboardSystem;
using System;

public class Menu : MonoBehaviour
{
    private float oldTimeScale;
    [SerializeField]
    private TextMeshProUGUI textLedearBoard;
    [SerializeField]
    private Liderboard liderboard;
    private string curName = "";
    public string CurName => curName;
    [SerializeField]
    private TextMeshProUGUI userNameUI;
    [SerializeField]
    private TextMeshProUGUI inputField;
    private const string key = "Username";
    [SerializeField]
    private Canvas canvasInGame;
    [SerializeField]
    private Canvas canvasUsername;

    private void Start() 
    {        
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
        userNameUI.text = "????????????: "+curName;
        Save();
    }

    public void RefreshTextUI_Scores()
    {
        liderboard.AddResult(curName, 123);
        textLedearBoard.text = liderboard.Results.ToString();
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
        userNameUI.text = "????????????: " + curName;
    }

    private void Save()
    {
        string json = JsonUtility.ToJson(curName);
        PlayerPrefs.SetString(key, json);
        PlayerPrefs.Save();
    }
}
