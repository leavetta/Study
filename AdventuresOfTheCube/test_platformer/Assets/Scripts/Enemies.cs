using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : Unit
{
    protected virtual void Awake() { }
    protected virtual void Start() { }
    protected virtual void Update() { }

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        Bullet bullet = collider.GetComponent<Bullet>();

        if (bullet)
        {
            ReceiveDamage();
        }

        PlayerController character = collider.GetComponent<PlayerController>();

        if (character)
        {
            character.ReceiveDamage();
        }
    }
}
