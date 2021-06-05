using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Obstacle : MonoBehaviour
{
    private Character ch;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        ch = collider.GetComponent<Character>();
        if (ch)
        {
            ch.ReceiveDamage();
        }  
    }
}
