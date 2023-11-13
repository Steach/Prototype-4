using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject[] enemyStrPrefabs;
    public GameObject enemyBossPrefab;
    public GameObject[] powerupPrefab;
    public GameObject bulletPrefab;
    private float spawnRange = 9;
    private int enemiesWave = 0;
    [HideInInspector] public int enemyCount;
    private Rigidbody playerRB;
    private bool playerInTheScene;
    private bool gameOver;
    private bool winGame;
    [SerializeField] TextMeshProUGUI winText;
    [SerializeField] TextMeshProUGUI waveText;
    private int bossAtScene;
    private int spawnInterval = 1;
    private float nextSpawn;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GameObject.Find("Player").GetComponent<Rigidbody>();
        IsPlayerInTheScene();
        winText.gameObject.SetActive(false);
        winGame = false;
        enemiesWave = 0;
        waveText.text = "Wave: " + enemiesWave;   
    }

    // Update is called once per frame
    void Update()
    {
        bossAtScene = FindObjectsOfType<BossEnemy>().Length;
        IsPlayerInTheScene();
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0 && !gameOver && !winGame)
        {
            enemiesWave++;
            waveText.text = "Wave: " + enemiesWave;
            if (enemiesWave > 10)
            {
                WinGame();
            }
            if(enemiesWave <= 10)
            {
                if (enemiesWave % 3 == 0)
                {
                    SpawnStrEnemy(enemiesWave);
                    SpawnPowerUp();
                }
                else if (enemiesWave % 10 == 0)
                {
                    SpawnBoss();
                    SpawnPowerUp();
                }
                else
                {
                    SpawnEnemy(enemiesWave);
                    SpawnPowerUp();
                } 
            }
                       
        }
        if (bossAtScene > 0) 
        {
            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnInterval;
                Instantiate(enemyPrefab, SpawnPosition(), enemyPrefab.transform.rotation);
            }
        }  
    }

    void SpawnEnemy(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {  
            Instantiate(enemyPrefab, SpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    void SpawnStrEnemy(int strEnemiesToSpawn)
    {
        for (int i = 0; i < strEnemiesToSpawn / 3; i++)
        {
            int strEnemyIndex = Random.Range(0, enemyStrPrefabs.Length);
            Instantiate(enemyStrPrefabs[strEnemyIndex], SpawnPosition(), enemyStrPrefabs[strEnemyIndex].transform.rotation);
        } 
    }

    void SpawnBoss()
    {
        Instantiate(enemyBossPrefab, SpawnPosition(), enemyBossPrefab.transform.rotation); 
    }

    Vector3 SpawnPosition()
    {
        float spawnX = Random.Range(-spawnRange, spawnRange);
        float spawnZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnX, 0.5f, spawnZ);
        return randomPos;
    }

    void SpawnPowerUp()
    {
        int indexPrefab = Random.Range(0, powerupPrefab.Length);
        Instantiate(powerupPrefab[indexPrefab], SpawnPosition(), powerupPrefab[indexPrefab].transform.rotation);
    }

    void IsPlayerInTheScene()
    {
        int playerTrue = FindObjectsOfType<PlayerController>().Length;
        if (playerTrue >= 1)
        {
            playerInTheScene = true;
            gameOver = false;
        }
        else
        {
            playerInTheScene = false;
            gameOver = true;
        }
    }

    void WinGame()
    {
        winGame = true;
        winText.gameObject.SetActive(true);
    }

    public bool TakeWinGame()
    {
        return winGame;
    }
}
