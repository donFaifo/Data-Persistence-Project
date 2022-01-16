using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

[DefaultExecutionOrder(1000)]
public class MenuManager : MonoBehaviour
{
    [SerializeField] string userName;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI ballSpeedInfo;

    private void Start()
    {
        ballSpeedInfo.text = $"Max Ball Speed: {MainManager.instance.gameData.ballMaxSpeed}";
    }

    public void StartGame()
    {
        MainManager.instance.playerName = playerName.text;
        SceneManager.LoadScene("main", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("settings", LoadSceneMode.Single);
    }
}
