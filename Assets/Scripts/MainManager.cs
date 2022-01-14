using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager instance;
    public string playerName;
    public string bestScorePlayerName;
    public int bestScore;

    private Score bestLastScore;
    private string _savingPath;
    
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        _savingPath = Application.persistentDataPath + "/";
        print($"Directorio de guardado: {_savingPath}");
        bestLastScore = LoadData("scores.json");
        bestScorePlayerName = bestLastScore.playerName;
        bestScore = bestLastScore.score;
    }

    public void SaveData(string filename)
    {
        bestLastScore.score = bestScore;
        bestLastScore.playerName = bestScorePlayerName;
        string json = JsonUtility.ToJson(bestLastScore);
        File.WriteAllText(_savingPath + filename, json);
    }

    private Score LoadData(string filename)
    {
        Score score = new Score();

        if(File.Exists(_savingPath + filename))
        {
            string json = File.ReadAllText(_savingPath + filename);
            score = JsonUtility.FromJson<Score>(json);
            print("Datos cargados");
            return score;
        } else
        {
            score = new Score();
            score.score = 0;
            score.playerName = "Nonamed";
            return score;
        }
    }

    [Serializable]
    /// <summary>
    /// Class to store a player's score
    /// </summary>
    class Score
    {
        public int score;
        public string playerName;
    }
}
