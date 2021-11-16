using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    Text scoreText;

    void Start()
    {
        scoreText = GameObject.Find("HiScoreText").GetComponent<Text>();

        LoadScore();
    }

    void LoadScore()
    {
        if(GameManager.Instance.HiScore == 0 && GameManager.Instance != null)
        {
            scoreText.gameObject.SetActive(false);
            return;
        }

        int hiScore = GameManager.Instance.HiScore;
        string hiScoreName = GameManager.Instance.HiScoreName;

        scoreText.text = "Best Score : " + hiScoreName + " : " + hiScore;
    }
}
