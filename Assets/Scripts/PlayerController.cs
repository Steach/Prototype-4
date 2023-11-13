using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody playerRB;
    private GameObject focalPoint;
    private TrigerCollider trigerCollider;
    private bool powerUp = false;
    public float playerPowerUpStrange;
    public ParticleSystem bangParticle;
    private Coroutine powerupCountdown;
    private bool activeGame;
    [SerializeField] TextMeshProUGUI gameOverText;
    private SpawnManager spawnManager;
    private bool winGame;
    // Start is called before the first frame update
    void Start()
    {
        gameOverText.gameObject.SetActive(false);
        playerRB = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
        trigerCollider = GetComponent<TrigerCollider>();
        bangParticle.Stop();
        activeGame = true;
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        SetWinGame();
    }

    // Update is called once per frame
    void Update()
    {
        SetWinGame();
        if (activeGame && !winGame)
        {
            Movement();
        }
        DestroyPlayer();
    }

    void DestroyPlayer()
    {
        if (transform.position.y <= -5 && !winGame)
        {
            activeGame = false;
            gameOverText.gameObject.SetActive(true);
            Destroy(gameObject);
        }
    }

    void Movement()
    {
        float verticalInput = Input.GetAxis("Vertical");
        playerRB.AddForce(focalPoint.transform.forward * moveSpeed * verticalInput * Time.deltaTime);
        powerUp = trigerCollider.hasPowerUp;
    }

    public bool TakeActiveGame()
    {
        return activeGame;
    }

    void SetWinGame()
    {
        winGame = spawnManager.TakeWinGame();
    }
}
