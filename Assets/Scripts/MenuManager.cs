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

    /// <summary>
    /// Starts the game launching main scene
    /// </summary>
    public void StartGame()
    {
        MainManager.instance.playerName = playerName.text;
        SceneManager.LoadScene("main", LoadSceneMode.Single);
    }

    /// <summary>
    /// Quit the game or exits from play mode
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    /// <summary>
    /// Loads settings scene
    /// </summary>
    public void OpenSettings()
    {
        SceneManager.LoadScene("settings", LoadSceneMode.Single);
    }
}
