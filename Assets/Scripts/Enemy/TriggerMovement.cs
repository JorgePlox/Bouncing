using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMovement : MonoBehaviour
{
    public EnemyController enemy;
    public bool movingRight = false;
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Collectable" || otherCollider.tag == "Player")
            return;

        movingRight = !movingRight;
        enemy.goingRight = movingRight;

    }
}
