using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class MainManager: MonoBehaviour
{
    public static MainManager instance;
    public string playerName;
    public string bestScorePlayerName;
    public int bestScore;
    public float ballMaxSpeed;

    private Score bestLastScore;
    public BestScores bestScores;
    public Settings gameSettings;

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

        // Game Settings
        gameSettings = new Settings();
        gameSettings.ballMaxSpeed = 3f;
        MainManager.instance.ballMaxSpeed = gameSettings.ballMaxSpeed;

        // Best scores
        bestScores = new BestScores();
        bestScores.bestScores.Add(new Score("Miguel", 15));
        bestScores.bestScores.Add(new Score("Ana", 12));
        bestScores.bestScores.Add(new Score("Emma", 9));
        bestScorePlayerName = bestLastScore.playerName;
        bestScore = bestLastScore.score;
    }

    public void SaveData(string filename)
    {
        bestLastScore.score = bestScore;
        bestLastScore.playerName = bestScorePlayerName;
        string json = JsonUtility.ToJson(bestLastScore);
        File.WriteAllText(_savingPath + filename, json);

        // TODO: Guardar la lista de mejores puntuaciones
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
            return score;
        }
    }

    [Serializable]
    /// <summary>
    /// Class to store a player's score
    /// </summary>
    public class Score
    {
        public int score;
        public string playerName;

        public Score()
        {
            playerName = "-";
            score = 0;
        }

        public Score(string name, int score)
        {
            playerName = name;
            this.score = score;
        }
    }

    [Serializable]
    public class BestScores
    {
        public List<Score> bestScores;

        public BestScores()
        {
            bestScores = new List<Score>();
        }

    }

    [Serializable]
    public class Settings
    {
        public float ballMaxSpeed;
    }

}
