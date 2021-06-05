using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] variants;

    public float timeBtwSpawn;
    public float startTimeBtwSpawn;
    public float decreaseTime;
    public float minTime;

    public float enemyHealth;
    public float increaseHealth;
    public float enemyDamage;
    public float increaseDamage;

    private void Update()
    {
        enemyDamage += Time.deltaTime * increaseDamage;
        enemyHealth += Time.deltaTime * increaseHealth;

        if (timeBtwSpawn <= 0)
        {
            //timeBtwSpawn = 1f;
            int rand = Random.Range(0, variants.Length);
            //Vector3 pos = transform.position; pos.z = 0f;
            Instantiate(variants[rand], transform.position, Quaternion.identity);
            timeBtwSpawn = startTimeBtwSpawn;
            if (startTimeBtwSpawn > minTime)
            {
                startTimeBtwSpawn -= decreaseTime;
            }
        }
        else
        {
            timeBtwSpawn -= Time.deltaTime;
        }
        
    }
    
}
