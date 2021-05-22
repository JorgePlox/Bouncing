using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float runningSpeed = 1.5f;
    private Rigidbody2D enemyRigdbody;
    
    //Para girar la animación
    public bool goingRight = false;

    //Para comenzar a mover
    public bool activated = false;

    private void Awake()
    {
        enemyRigdbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (activated)
        {
            float currentSpeed = runningSpeed;

            if (goingRight == true)
            {
                this.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                currentSpeed = runningSpeed;
            }
            else if (goingRight == false)
            {
                this.transform.eulerAngles = new Vector3(0f, 180f, 0f);
                currentSpeed = -runningSpeed;
            }

            if (GameManager.sharedInstance.currentGameState == GameState.inGame)
            {
                enemyRigdbody.velocity = new Vector2(currentSpeed, enemyRigdbody.velocity.y);
            }
        }

    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "EnemyActivator")
            activated = true;
    }
}
