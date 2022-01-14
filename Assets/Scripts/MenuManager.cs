using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

[DefaultExecutionOrder(1000)]
public class MenuManager : MonoBehaviour
{
    [SerializeField] string userName;
    public TextMeshProUGUI playerName;

    public void StartGame()
    {
        MainManager.instance.playerName = playerName.text;
        SceneManager.LoadScene("main", LoadSceneMode.Single);
    }
}
