using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    public Enemy enemy;
    private Spawner spawner;
    private void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        //Vector3 pos = transform.position; //pos.z = 0f; pos.x = 16f; pos.y = -3f;
        //Debug.Log(transform.position);
        Instantiate(enemy, transform.position, transform.rotation);
        enemy.health = Mathf.RoundToInt(spawner.enemyHealth);
        enemy.damage = Mathf.RoundToInt(spawner.enemyDamage);
    }
}
