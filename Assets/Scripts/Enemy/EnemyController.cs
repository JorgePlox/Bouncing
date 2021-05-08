using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float runningSpeed = 1.5f;
    private Rigidbody2D enemyRigdbody;
    //Para girar la animaci�n
    public bool goingRight = false;

    private void Awake()
    {
        enemyRigdbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float currentSpeed = runningSpeed;

        if (goingRight == true)
        {
            this.transform.eulerAngles = new Vector3 (0f,0f,0f);
            currentSpeed = runningSpeed;
        }
        else if (goingRight == false)
        {
            this.transform.eulerAngles = new Vector3 (0f,180f,0f);
            currentSpeed = -runningSpeed;
        }

        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            enemyRigdbody.velocity = new Vector2(currentSpeed, enemyRigdbody.velocity.y);
        }

    }
}