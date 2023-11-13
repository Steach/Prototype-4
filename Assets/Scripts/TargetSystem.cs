using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSystem : MonoBehaviour
{
    public string bulletTag = "Bullet";
    [SerializeField] List<string> enemyTag;
    private GameObject[] bullets;
    private GameObject[] enemies;
    private GameObject[] strEnemies;
    // Start is called before the first frame update
    void Start()
    {
        enemyTag = new List<string> {"Enemy", "StrEnemy", "BossEnemy"};
    }

    // Update is called once per frame
    void Update()
    {
        FindBullets();
        FindEnemies();
    }

    void FindBullets()
    {
        bullets = GameObject.FindGameObjectsWithTag(bulletTag);
    }

    void FindEnemies()
    {
        List<GameObject> enemyObjects = new List<GameObject>();
        foreach (string tag in enemyTag)
        {
            GameObject[] taggedEnemies = GameObject.FindGameObjectsWithTag(tag);
            enemyObjects.AddRange(taggedEnemies);
        }
        enemies = enemyObjects.ToArray();

        //enemies = GameObject.FindGameObjectsWithTag(enemyTag[0]);
        //strEnemies = GameObject.FindGameObjectsWithTag(enemyTag[1]);

        //Debug.Log("Enemies: " + enemies.Length);
    }

    void HomingBullets()
    {
        foreach (GameObject bullet in bullets)
        {
            GameObject nearestEnemy = FindNearestEnemy(bullet.transform.position);

            if (nearestEnemy != null)
            {
                bullet.transform.LookAt(nearestEnemy.transform);
            }
        }
    }

    public GameObject FindNearestEnemy(Vector3 position)
    {
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(position, enemy.transform.position);
            if(distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }
}
