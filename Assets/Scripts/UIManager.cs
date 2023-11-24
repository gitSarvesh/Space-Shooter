using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Image livesImg;

    [SerializeField]
    private Sprite[] livesSprites;

    [SerializeField]
    private Text gameOverText;

    [SerializeField]
    private Text restartLevelText;

    [SerializeField]
    private gameManager gM;
    [SerializeField]
    private Text bestScoreText;
    private int bestScore;
    void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        scoreText.text = "Score : " + 0;
        gameOverText.gameObject.SetActive(false);
        restartLevelText.gameObject.SetActive(false);
        gM = GameObject.Find("GameManager").GetComponent<gameManager>();
        UpdateBestScore(bestScore);
    }

    public void UpdateScore(int Addpoints)
    {
        scoreText.text = "Score : " + Addpoints;
    }

    public void UpdateBestScore(int bestScore)
    {
        this.bestScore = bestScore;
        bestScoreText.text = "Best : " + bestScore;
        PlayerPrefs.SetInt("BestScore", bestScore);
    }


    public void UpdateLives(int currentLives) 
    {
        livesImg.sprite = livesSprites[currentLives];
        if(currentLives <= 0)
        {
            GameOverSequence();
        }
    }

    void GameOverSequence()
    {
        gM.GameOver();

        gameOverText.gameObject.SetActive(true);
        restartLevelText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            gameOverText.text = " ";
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ResumePlay(){
        gM.ResumeGame();
    }

    public void BackToMainMenu(){
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1f;
    }
}
