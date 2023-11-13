using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeFollowX : MonoBehaviour
{
    public Rigidbody playerRB;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = GameObject.Find("Player").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 particlePosition = new Vector3 (playerRB.transform.position.x, playerRB.transform.position.y, playerRB.transform.position.z); 
        transform.position = particlePosition;
    }
}
