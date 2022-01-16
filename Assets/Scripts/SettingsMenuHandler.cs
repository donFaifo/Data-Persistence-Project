using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenuHandler : MonoBehaviour
{
    public TextMeshProUGUI speedText;
    public Scrollbar scrollBar;

    [SerializeField] private float _ballSpeed;
    private float _baseSpeed = 3f;  // Minimum speed will be 3, maximum speed will be 10

    // Start is called before the first frame update
    void Start()
    {
        _ballSpeed = MainManager.instance.gameData.ballMaxSpeed;
        scrollBar.value = (_ballSpeed - _baseSpeed)/7;
    }

    /// <summary>
    /// Set the text of the label next to the scrollbar
    /// </summary>
    public void SetSpeedText()
    {
        _ballSpeed = _baseSpeed + scrollBar.value * 7f;  // This sets 7 steps in the scroll bar so the maximum speed will be limited to 10 (3 + 7)
        speedText.text = Mathf.Round(_ballSpeed).ToString();
    }

    /// <summary>
    /// Save the settings to be persistent through the game and sessions
    /// </summary>
    public void SaveSettings()
    {
        _ballSpeed = Mathf.Round(_ballSpeed);
        Debug.Log("Ball speed: " + _ballSpeed);
        MainManager.instance.gameData.ballMaxSpeed = _ballSpeed;
        MainManager.instance.SaveData(MainManager.dataFile);
        SceneManager.LoadScene("menu", LoadSceneMode.Single);
    }
}
