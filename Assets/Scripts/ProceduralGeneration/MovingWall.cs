using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour
{
    
    private Rigidbody2D muralla;
    private Vector3 startPosition;
    public Vector3 velocity = new Vector3(3f, 0f, 0f);

    public static MovingWall sharedInstance;
   



    private void Awake()
    {
        sharedInstance = this;
        startPosition = this.transform.position;
        muralla = GetComponent<Rigidbody2D>();
    }


    public void MoveWall()
    {
        muralla.velocity = velocity;
    }

    public void StopWall()
    {
        muralla.velocity = Vector3.zero;
    }

    public void ResetWallPosition()
    {
        muralla.transform.position = startPosition;
    }

}
