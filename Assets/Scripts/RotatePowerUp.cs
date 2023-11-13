using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePowerUp : MonoBehaviour
{
    private GameObject powerUp;
    private float turnSpeed = 70;
    // Start is called before the first frame update
    void Start()
    {
        powerUp = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
    }
}
