using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject hazard;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Transform PlayerReplica;


    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;

    private Vector3 regen;

    private bool gameOver;
    private bool restart;
    private int score;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves());

        PlayerReplica = GameObject.FindWithTag("PlayerReplica").transform;

    }

    private void Update()
    {
        regen.x = PlayerReplica.transform.position.x;
        regen.y = PlayerReplica.transform.position.y;
        regen.z = PlayerReplica.transform.position.z;


        if (restart)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(regen.x - spawnValues.x, regen.x + spawnValues.x), 
                                                    Random.Range(regen.y - spawnValues.y, regen.y + spawnValues.y), 
                                                    Random.Range(regen.z + spawnValues.z, regen.z + spawnValues.z* 5.0f));
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if(gameOver)
            {
                restartText.text = "Please 'R' for restart!";
                restart = true;
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }
    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }
}
