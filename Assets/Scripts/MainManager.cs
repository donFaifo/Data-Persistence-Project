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

    public GameData gameData;

    public static string dataFile;

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
        dataFile = Application.persistentDataPath + "/data.json";

        // Best scores
        gameData = LoadData(dataFile);
    }

    /// <summary>
    /// Guarda los datos del juego en el archivo indicado
    /// </summary>
    /// <param name="file">Ruta completa del archivo de guardado</param>
    public void SaveData(string file)
    {
        string json = JsonUtility.ToJson(gameData);
        File.WriteAllText(file, json);
    }

    /// <summary>
    /// Carga los datos del juego del archivo especificado
    /// </summary>
    /// <param name="file">Ruta completa del archivo con los datos del juego</param>
    /// <returns>Objeto GameData con los datos de las mejores puntuaciones.</returns>
    private GameData LoadData(string file)
    {
        GameData data;

        if(File.Exists(file))
        {
            string json = File.ReadAllText(file);
            data = JsonUtility.FromJson<GameData>(json);
            print("Datos cargados");
        } else
        {
            data = new GameData();
        }

        return data;
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

    /// <summary>
    /// Stores all the game data to be saved.
    /// </summary>
    [Serializable]
    public class GameData
    {
        public List<Score> bestScores;
        public float ballMaxSpeed;

        public GameData()
        {
            bestScores = new List<Score>();
            ballMaxSpeed = 3f;
        }

    }

}
