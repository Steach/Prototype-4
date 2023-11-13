using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TrigerCollider : MonoBehaviour
{
    public bool hasPowerUp = false;
    private Vector3 playerPosition;
    private float powerUpStrange;
    private PlayerController playerController;
    public GameObject powerUpInd;
    private Rigidbody playerRB;
    private SpawnManager spawnManager;
    private BulletBehaviour bulletBehaviour;
    public GameObject bulletPrefab;
    private GameObject tmpBullet;
    private int countsOfBullets;
    [SerializeField] TextMeshProUGUI countsOfBulletsText;

    void Start() 
    {
        playerController = GetComponent<PlayerController>();
        powerUpStrange = playerController.playerPowerUpStrange;
        powerUpInd.gameObject.SetActive(false);
        playerRB = GetComponent<Rigidbody>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        countsOfBullets = 0;
        ShowCountsOfBullets();
    }
    void Update() 
    {
        playerPosition = transform.position;
        if (Input.GetKeyDown(KeyCode.Space) && countsOfBullets > 0)
        {
            LaunchBullet();
            countsOfBullets--;
            ShowCountsOfBullets();
        }
    }
    void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerUp = true;
            powerUpInd.gameObject.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine());
        }

        if (other.CompareTag("PowerUpBang"))
        {
            playerRB.AddForce(Vector3.up * 15, ForceMode.Impulse);
            StartCoroutine(WaitJump());
            Destroy(other.gameObject);
        }

        if(other.CompareTag("PowerUpStar"))
        {
            countsOfBullets += 5;
            ShowCountsOfBullets();
            Destroy(other.gameObject);
        }
    }

    void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - playerPosition;

            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrange, ForceMode.Impulse);
            //Debug.Log("Collision " + collision.gameObject.name + " power " + hasPowerUp);
        }

        if (collision.gameObject.CompareTag("StrEnemy") && hasPowerUp)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - playerPosition;

            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrange / 3, ForceMode.Impulse);
        }

        if (collision.gameObject.CompareTag("BossEnemy") && hasPowerUp)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - playerPosition;

            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrange / 5, ForceMode.Impulse);
        }
    }

    void LaunchBullet()
    {
        tmpBullet = Instantiate(bulletPrefab, transform.position + Vector3.up, Quaternion.identity);        
    }

    void KnockbackEnemies()
    {
        
        playerController.bangParticle.Play();
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] strEnemies = GameObject.FindGameObjectsWithTag("StrEnemy");
        GameObject bossEnemy = GameObject.FindGameObjectWithTag("BossEnemy");

        foreach (GameObject enemy in enemies)
        {
            Rigidbody enemyRigidbody = enemy.GetComponent<Rigidbody>();
            if (enemyRigidbody != null)
            {
                // Відкидання об'єкта "enemy" від ігрока
                Vector3 knockbackDirection = enemy.transform.position - transform.position;
                enemyRigidbody.AddForce(knockbackDirection.normalized * powerUpStrange, ForceMode.Impulse);
            }
        }

        foreach (GameObject strEnemy in strEnemies)
        {
            Rigidbody enemyRigidbody = strEnemy.GetComponent<Rigidbody>();
            if (enemyRigidbody != null)
            {
                // Відкидання об'єкта "enemy" від ігрока
                Vector3 knockbackDirection = strEnemy.transform.position - transform.position;
                enemyRigidbody.AddForce(knockbackDirection.normalized * powerUpStrange / 2, ForceMode.Impulse);
            }
        }

        Rigidbody enemyBossRigidbody = bossEnemy.GetComponent<Rigidbody>();
            if (enemyBossRigidbody != null)
            {
                // Відкидання об'єкта "enemy" від ігрока
                Vector3 knockbackDirection = bossEnemy.transform.position - transform.position;
                enemyBossRigidbody.AddForce(knockbackDirection.normalized * powerUpStrange / 4, ForceMode.Impulse);
            }
    }

    IEnumerator WaitJump()
    {
        yield return new WaitForSeconds(1);
        playerRB.AddForce(Vector3.down * 50, ForceMode.Impulse);
        yield return new WaitForSeconds(0.2f);
        KnockbackEnemies();
        playerController.bangParticle.Play();
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        powerUpInd.gameObject.SetActive(false);
    }

    IEnumerable PowerupStarCountdown()
    {
        yield return new WaitForSeconds(0.5f);
    }

    void ShowCountsOfBullets()
    {
        countsOfBulletsText.text = "Bullets: " + countsOfBullets;
    }
}
