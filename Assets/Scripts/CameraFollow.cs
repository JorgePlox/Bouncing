using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform tarjet;

    public Vector2 offset = new Vector2(7f, -1f);

    public float dampTime = 0.3f;

    public Vector3 velocity = Vector3.zero;


    // Start is called before the first frame update
    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 destination = new Vector3(tarjet.position.x + offset.x, offset.y, -10);

        this.transform.position = Vector3.SmoothDamp(this.transform.position, destination, ref velocity, dampTime);
    }
}
