using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float speed = 50f;
    public string enemyTag = "Enemy";
    private GameObject target;
    private TargetSystem targetSystem;
    private Rigidbody bulletRB;
    void Start()
    {
        bulletRB = GetComponent<Rigidbody>();
        targetSystem = GameObject.Find("TargetSystem").GetComponent<TargetSystem>();
        Invoke("Timer", 2f);
    }

    void Update()
    {
        SetTarget();
        Vector3 direction = target.transform.position - transform.position;
        float distanceThisFrame = speed;
        bulletRB.AddForce(direction.normalized * distanceThisFrame);
    }

    public void SetTarget()
    {
        target = targetSystem.FindNearestEnemy(transform.position);
    }

    void HitTarget()
    {
        Debug.Log("Bullet hit the target!");
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            Debug.Log("Bullet hit the target!");
        }

        if (col.gameObject.CompareTag("StrEnemy"))
        {
            Destroy(gameObject);
            Debug.Log("Bullet hit the target!");
        }

         if (col.gameObject.CompareTag("BossEnemy"))
        {
            Destroy(gameObject);
            Debug.Log("Bullet hit the target!");
        }
    }

    void Timer()
    {
        Destroy(gameObject);
    }
}