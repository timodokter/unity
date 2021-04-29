using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GamemanagerX : MonoBehaviour
{
    private Target Target;
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI LivesText;
    public TextMeshProUGUI gameOverText;
    public Button RestartButton;

    private int score;
    public bool isGameActive;

    public List<GameObject> targetPrefabs;
    public List<Vector3> targetCoördinates;

    public GameObject enemyPrefab;
    public List<Vector3> enemyCoördinates;

    private int SpawnPos1;
    private int SpawnPos2;
    private int SpawnPos3;

    private int index1;
    private int index2;
    private int index3;

    public float TargetCount;

    public float Lives;

    public float SpawnTimer;
    
    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;
        FirstSpawn();
        TargetCount = 3f;
        StartCoroutine(SpawnTargets());
        StartCoroutine(SpawnEnemys());
        Lives = 5f;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(SpawnTimer);
        if (Lives <= 0)
        {
            GameOver();
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(int livesToLose)
    {
        Lives -= livesToLose;
        LivesText.text = "Remaing lives: " + Lives;
    }

    public void UpdateSpawnTimer()
    {
        SpawnTimer = 7.5f;
        SpawnTimer = 7.5f - (score / 100f);
    }

    private void FirstSpawn()
    {
        Instantiate(targetPrefabs[0], targetCoördinates[Random.Range(0, 2)], targetPrefabs[0].transform.rotation);
        Instantiate(targetPrefabs[1], targetCoördinates[Random.Range(2, 4)], targetPrefabs[1].transform.rotation);
        Instantiate(targetPrefabs[2], targetCoördinates[Random.Range(4, 6)], targetPrefabs[2].transform.rotation);
    }

    IEnumerator SpawnTargets()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(5);
            if (TargetCount == 0)
            {
                Instantiate(targetPrefabs[0], targetCoördinates[Random.Range(0, 2)], targetPrefabs[0].transform.rotation);
                Instantiate(targetPrefabs[1], targetCoördinates[Random.Range(2, 4)], targetPrefabs[1].transform.rotation);
                Instantiate(targetPrefabs[2], targetCoördinates[Random.Range(4, 6)], targetPrefabs[2].transform.rotation);
                TargetCount = 3;
            }
        }
    }

    IEnumerator SpawnEnemys()
    {
        while (isGameActive)
        {
            UpdateSpawnTimer();
            yield return new WaitForSeconds(SpawnTimer);
            Instantiate(enemyPrefab, enemyCoördinates[Random.Range(0, 2)], enemyPrefab.transform.rotation);
        }
    }

    public void GameOver()
    {
        Debug.Log("is GameOver");
        gameOverText.gameObject.SetActive(true);
        RestartButton.gameObject.SetActive(true);
        isGameActive = false;
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        Debug.Log("is restarting");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}