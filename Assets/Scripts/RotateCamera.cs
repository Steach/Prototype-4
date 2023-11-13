using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotateSpeed;
    private PlayerController playerController;
    private SpawnManager spawnManager;
    private bool activeGame;
    private bool winGame;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        SetActiveGame();
        SetWinGame();
    }

    // Update is called once per frame
    void Update()
    {
        SetActiveGame();
        if (activeGame && !winGame)
        {
            RotateCam();
        }
    }

    void RotateCam()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.down, horizontalInput * rotateSpeed * Time.deltaTime); 
    }

    void SetActiveGame()
    {
        activeGame = playerController.TakeActiveGame();
    }

    void SetWinGame()
    {
        winGame = spawnManager.TakeWinGame();
    }
}
