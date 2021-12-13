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

    [Header("Audio while collecting gems")]
    [SerializeField] AudioClip gemCollectedAudio;

    [Header("Audio after the game is compeleted")]
    [SerializeField] AudioClip gameWon;

    [Header("Audio if players dies or run out of time")]
    [SerializeField] AudioClip gameLost;
    bool start = false;

    AudioSource audioSource;
    private void Awake()
    {
        gemCollectedText.text = "Gem Collected : " + gemCollected;
        timerText.text = "Remaining Time : " + time;
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
       
        GemController.CollectGem += GemController_CollectGem;
       
       
    }

    void PlayAudio(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
    private void Update()
    {
        if (!start &&( Input.GetMouseButtonDown(0) || Input.anyKey))
        {
            start = true;
            InvokeRepeating("scoreUpdate", 1.0f, 1.0f);
        }
    }
    /// <summary>
    /// Call to update the score
    /// </summary>
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
        PlayAudio(gemCollectedAudio);
        Destroy(gem);
    }


    public void GameOver(string msg)
    {
        CancelInvoke(nameof(scoreUpdate));
        PlayAudio(gameLost);
        gameOverCanvas.SetActive(true);
        totalTime.text = msg;
    }

    public void GameWon()
    {
        PlayAudio(gameWon);
        //need to cancel the score update that is called on repeat
        CancelInvoke(nameof(scoreUpdate));
        gameOverCanvas.SetActive(true);
        GameOverText.text = "Game Finished";
        totalTime.text = "Total Time : " +(60-time);
        totalScore.text = "Total Score : " + (60 - time) * 5 + gemCollected * 8;
        totalScore.gameObject.SetActive(true);
    }
}
