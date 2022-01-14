using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScore;
    public GameObject GameOverText;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;

    private string _playerName;
    private int _lastBestScore;
    private string _lastBestPlayerName;

    // Start is called before the first frame update
    void Start()
    {
        SetLastBestScore();
        SetPlayerName();
        BuildBrickWall();
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
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if(m_GameOver)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    public void GameOver()
    {
        if(m_Points > _lastBestScore)
        {
            print($"m_Points: {m_Points}, _lastBestScore: {_lastBestScore}");
            MainManager.instance.bestScore = m_Points;
            MainManager.instance.bestScorePlayerName = _playerName;
            SetLastBestScore();
            print($"Mejor puntuación para guardar: " + MainManager.instance.bestScore);
            print($"Mejor jugador para guardar: " + MainManager.instance.bestScorePlayerName);
            MainManager.instance.SaveData("scores.json");
        }
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    public void SetPlayerName()
    {
        _playerName = MainManager.instance.playerName;
        ScoreText.text = $"{_playerName} score: 0";
    }

    public void SetLastBestScore()
    {
        _lastBestScore = MainManager.instance.bestScore;
        _lastBestPlayerName = MainManager.instance.bestScorePlayerName;

        BestScore.text = $"Best Score: {_lastBestPlayerName}: {_lastBestScore}";
    }
}
