using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ScoreManager : MonoBehaviour
{
    public Text highScore;
    public Text currentScore;
    public int score;

    private void Start()
    {
        
        highScore.text = PlayerPrefs.GetInt("highScore", 0).ToString();
    }

    public void addScore()
    {
        score += 1;
        currentScore.text = score.ToString();
        if(score > PlayerPrefs.GetInt("highScore", 0))
        {
            PlayerPrefs.SetInt("highScore", score);
            highScore.text = score.ToString();
        }
    }
}
