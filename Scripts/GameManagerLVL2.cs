using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerLVL2 : MonoBehaviour
{
    public float maxTime = 1;
    private float timer = 0;
    public GameObject GameOverCanvas;
    public GameObject WinCanvas;
    public PlayerMovementLVL2 playerMove;
    public Text scoreText;
    public float score;
    private float score2;
    public float timeScore;
    public float winCondition;
    public AudioSource loseSound;
    //public float oneUpScore;

    public void Start()
    {
        Time.timeScale = 1;
        scoreText.text = ("SCORE: " + score.ToString());
        score = PlayerPrefs.GetFloat("FinalScore", 0);
        
    }

    // Update is called once per frame
    public void GameOver()
    {
        PlayerPrefs.SetFloat("FinalScore", 0);
        GameOverCanvas.SetActive(true);
        loseSound.Play();
        //Time.timeScale = 0;
    }

    public void Replay()
    {
        SceneManager.LoadScene(3);
    }

    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (timer > maxTime)
        {
            timeScore+= 1;
            score+= 1;
            score2+= 1;
            timer = 0;
        }

        timer += Time.deltaTime;
        scoreText.text = ("SCORE: " + score.ToString());

        if (timeScore >= winCondition) 
        {
            LevelClear();
        }

    }
    public void LevelClear()
    {
        WinCanvas.SetActive(true);
        PlayerPrefs.SetFloat("FinalScore", score);
        PlayerPrefs.SetInt("Level2Cleared", 1);
    }


    public void CoinCollected()
    {
        score += 300;
        score2 += 300;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public IEnumerator wait(float time){
        yield return new WaitForSeconds(time);
    }


}
