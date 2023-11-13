using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRb;
    private Rigidbody player;
    public float moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player").GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Goals"))
        {
            Destroy(gameObject);
        }
    }
}
