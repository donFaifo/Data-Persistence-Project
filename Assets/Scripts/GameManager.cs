using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager: MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public GameObject Ball;

    public Text ScoreText;
    public Text BestScore;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;

    private string _playerName;
    private int _lastBestScore;
    private string _lastBestPlayerName;
    private Rigidbody _ballRb;

    // Start is called before the first frame update
    void Start()
    {
        SetLastBestScore();
        SetPlayerName();
        BuildBrickWall();
        _ballRb = Ball.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!m_Started)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                _ballRb.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if(m_GameOver)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("best scores");
            }
        }
    }

    void BuildBrickWall()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for(int i = 0; i < LineCount; ++i)
        {
            for(int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"{_playerName} score : {m_Points}";
    }

    /// <summary>
    /// Ends the game and sets boolean m_GameOver true
    /// </summary>
    public void GameOver()
    {
        print($"m_Points: {m_Points}, _lastBestScore: {_lastBestScore}");

        // To maintain the bests three scores, a new Socore is added to the list, then
        // sorted using a comparer interface to finally trim the list to the three firsts scores
        MainManager.instance.gameData.bestScores.Add(new MainManager.Score(_playerName, m_Points));
        MainManager.instance.gameData.bestScores.Sort(new ScoreComparer());
        if(MainManager.instance.gameData.bestScores.Count > 3)
        {
            MainManager.instance.gameData.bestScores.RemoveRange(3, MainManager.instance.gameData.bestScores.Count-3);
        }

        MainManager.instance.SaveData(MainManager.dataFile);

        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    /// <summary>
    /// Sets the player's info on the screen
    /// </summary>
    public void SetPlayerName()
    {
        _playerName = MainManager.instance.playerName;
        ScoreText.text = $"{_playerName} score: 0";
    }

    /// <summary>
    /// Sets the last best score taken from the last three bests scores
    /// </summary>
    public void SetLastBestScore()
    {
        if(MainManager.instance.gameData.bestScores.Count != 0)
        {
            _lastBestScore = MainManager.instance.gameData.bestScores[0].score;
            _lastBestPlayerName = MainManager.instance.gameData.bestScores[0].playerName;
        }
        else
        {
            _lastBestPlayerName = "No player";
            _lastBestScore = 0;
        }
        

        BestScore.text = $"Best Score: {_lastBestPlayerName}: {_lastBestScore}";
    }


    class ScoreComparer : IComparer<MainManager.Score>
    {
        int IComparer<MainManager.Score>.Compare(MainManager.Score x, MainManager.Score y)
        {
            if(x.score < y.score) return 1;
            if(x.score == y.score) return 0;
            if(x.score > y.score) return -1;
            return 0;
        }
    }
}
