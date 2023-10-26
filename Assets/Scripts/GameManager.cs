using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gameStarted;
    public int score;
    public Text scoreText;
    public Text highScoreText;

    private void Awake()
    {
        highScoreText.text = "Highscore: " + GetHighScore().ToString();
    }

    public void StartGame()
    {
        gameStarted = true;
        FindFirstObjectByType<Road>().StartBuilding();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }
    }

    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();

        if(score > GetHighScore())
        {
            PlayerPrefs.SetInt("Highscore", score);
            highScoreText.text = "Highscore: " + score.ToString();
        }
    }

    public int GetHighScore()
    {
        int i = PlayerPrefs.GetInt("Highscore");
        return i;
    }
}
