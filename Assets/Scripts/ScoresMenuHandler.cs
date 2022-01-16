using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoresMenuHandler : MonoBehaviour
{
    public TextMeshProUGUI scoresText;
    
    // Start is called before the first frame update
    void Start()
    {
        scoresText.text = "";
        for(int i=0; i<MainManager.instance.gameData.bestScores.Count; i++)
        {
            string playerName = MainManager.instance.gameData.bestScores[i].playerName;
            int score = MainManager.instance.gameData.bestScores[i].score;

            scoresText.text += $"{playerName}: {score}\n";
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("main");
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
        }
    }
}
