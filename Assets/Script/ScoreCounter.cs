using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] Text gemCollectedText;

    [SerializeField] Text timerText;
    [SerializeField] Text GameOverText;
    [SerializeField] Text totalScore;
    [SerializeField] Text totalTime;

    int gemCollected = 0;

    int time = 60;
    [Header("Add the game over canvas")]
    [SerializeField] GameObject gameOverCanvas;

    private void Awake()
    {
        gemCollectedText.text = "Gem Collected : " + gemCollected;
        timerText.text = "Remaining Time : " + time;
    }
    void Start()
    {
       
        GemController.CollectGem += GemController_CollectGem;
       
        InvokeRepeating("scoreUpdate", 1.0f, 1.0f);
    }
    void scoreUpdate()
    {
        time -= 1;
        timerText.text = "Remaining Time : " + time;
        if (time == 0)
            GameOver("Get to portal before time ends");

    }
    private void OnDestroy()
    {
        GemController.CollectGem -= GemController_CollectGem;

    }
    // Update is called once per frame
    private void GemController_CollectGem(GameObject gem)
    {
        gemCollected++;
        gemCollectedText.text = "Gem Collected : " + gemCollected;

        Destroy(gem);
    }

    public void GameOver(string msg)
    {
        Time.timeScale = 0f;
        gameOverCanvas.SetActive(true);
        totalTime.text = msg;
    }

    public void GameWon()
    {
        Time.timeScale = 0f;
        gameOverCanvas.SetActive(true);
        GameOverText.text = "Game Finished";
        totalTime.text = "Total Time : " +(60-time);
        totalScore.text = "Total Score : " + (60 - time) * 5 + gemCollected * 8;
        totalScore.gameObject.SetActive(true);
    }
}
