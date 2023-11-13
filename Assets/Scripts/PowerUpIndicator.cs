using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpIndicator : MonoBehaviour
{
    public GameObject player;
    private Vector3 playerPosition;
    private float offsetY = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = new Vector3(player.transform.position.x, player.transform.position.y - offsetY, player.transform.position.z);
        transform.position = playerPosition;
    }
}
