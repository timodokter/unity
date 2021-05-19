using System;
using System.Collections;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class GamemanagerX : MonoBehaviour
{
    private PlayerController _playerController;
    
    public bool isGameActive;

    private TextMeshProUGUI AmmoCountText;
    private TextMeshProUGUI ScoreText;
    private TextMeshProUGUI highScoreText;
    private TextMeshProUGUI endScreenYourScoreText;
    private TextMeshProUGUI timeToPlayTimerText;

    private float score;
    private float highScore;
    private float totalBullets;

    private float timer;
    private Canvas startScreen;
    private Canvas playScreen;
    private Canvas GameOverScreen;

    private float _timeToPlayTimer = 30;

    // Start is called before the first frame update
    void Start()
    {
        //accesing a different file
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        
        //calling text obejects to the variables
        AmmoCountText = GameObject.FindWithTag("AmmoCountText").GetComponent<TextMeshProUGUI>();
        ScoreText = GameObject.FindWithTag("ScoreText").GetComponent<TextMeshProUGUI>();
        highScoreText = GameObject.FindWithTag("HighScoreText").GetComponent<TextMeshProUGUI>();
        endScreenYourScoreText = GameObject.FindWithTag("EndScreenScoreText").GetComponent<TextMeshProUGUI>();
        timeToPlayTimerText = GameObject.FindWithTag("timeToPlayTimer").GetComponent<TextMeshProUGUI>();
        
        //calling the canvas object to the variables
        startScreen = GameObject.FindWithTag("Start Screen").GetComponent<Canvas>();
        playScreen = GameObject.FindWithTag("UI During Game").GetComponent<Canvas>();
        GameOverScreen = GameObject.FindWithTag("GameOverScreen").GetComponent<Canvas>();

        totalBullets = _playerController.currAmmo + _playerController.extraAmmo;
    }

    // Update is called once per frame
    void Update()
    {

        if (isGameActive)
        {
            _timeToPlayTimer -= Time.deltaTime;
            
            UpdateTimerText();
        }
        
        if (_timeToPlayTimer <= 0)
        {
            StopGame();
        }

        if (_playerController.totalBulletsShot >= totalBullets)
        {
            timer += Time.deltaTime;
            if (timer > 2.5f)
            {
                timer = 0;
                StopGame();
                Debug.Log("ik run");
            }
        }
        // CheckIfTheGameShouldStop();
    }
    
    //function for updating the timer in the top rightt
    private void UpdateTimerText()
    {
        timeToPlayTimerText.text = "Time: " + Mathf.Round(_timeToPlayTimer);
    }

    //function for keeping track of your amount of bullets
    public void UpdateAmmoCount()
    {
        AmmoCountText.text = "AMMO" +
                             "\nCurrent Magazine: " + _playerController.currAmmo + 
                             "\nExtra Ammo: " + _playerController.extraAmmo;
    }
    
    //function for checking and updating the current highscore
    private void UpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
        }

        highScoreText.text = "Current highscore: " + highScore;
        endScreenYourScoreText.text = "Your Score: " + score;
    }

    //function for updating the score showed during the game
    public void UpdateScore(float pointValue)
    {
        score += pointValue;
        ScoreText.text = "Score: " + score;
    }

    //start the game
    public void StartGame()
    {
        isGameActive = true;
        playScreen.enabled = true;
        startScreen.enabled = false;
    }

    // stop the game
    public void StopGame()
    {
        UpdateHighScore();
        isGameActive = false;
        playScreen.enabled = false;
        GameOverScreen.enabled = true;
        _playerController.totalBulletsShot = 0;
        _timeToPlayTimer = 30;
        Debug.Log("ik stop");
    }

    //restart the game
    public void RestartGame()
    {
        _playerController.bulletsShot = 0;
        _playerController.totalBulletsShot = 0;
        _playerController.currAmmo = 30;
        _playerController.extraAmmo = 30;
        score = 0;
        UpdateScore(score);
        isGameActive = true;
        playScreen.enabled = true;
        GameOverScreen.enabled = false;
    }
}
