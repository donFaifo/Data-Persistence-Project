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
    private float _baseSpeed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        _ballSpeed = MainManager.instance.ballMaxSpeed;
        scrollBar.value = (_ballSpeed - _baseSpeed)/7;
    }


    public void SetSpeedText()
    {
        _ballSpeed = _baseSpeed + scrollBar.value * 7f;
        speedText.text = Mathf.Round(_ballSpeed).ToString();
    }

    public void SaveSettings()
    {
        _ballSpeed = Mathf.Round(_ballSpeed);
        Debug.Log("Ball speed: " + _ballSpeed);
        MainManager.instance.ballMaxSpeed = _ballSpeed;
        SceneManager.LoadScene("menu", LoadSceneMode.Single);
    }
}
