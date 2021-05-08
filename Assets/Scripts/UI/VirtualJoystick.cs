using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualJoystick : MonoBehaviour
{
    public static VirtualJoystick sharedInstance;

    //Booleanos para el movimiento
    public bool moveRight = false;
    public bool moveLeft = false;
    public bool moveJump = false;

    private void Awake()
    {
        sharedInstance = this;
    }

    public void LeftButtonPressed()
    {
        moveLeft = true;
    }

    public void LeftButtonReleased()
    {
        moveLeft = false;
    }

    public void RightButtonPressed()
    {
        moveRight = true;
    }

    public void RightButtonReleased()
    {
        moveRight = false;
    }

    public void JumpButtonPressed()
    {
        moveJump = true;
    }

}
