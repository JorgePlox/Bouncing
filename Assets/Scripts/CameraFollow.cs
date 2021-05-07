using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform tarjet;

    public Vector2 offset = new Vector2(7f, -1f);

    public float dampTime = 0.3f;

    public Vector3 velocity = Vector3.zero;

    public static CameraFollow sharedinstance;


    //Limites de la camara
    public float rightLimit;
    public float leftLimit;
    public float topLimit;
    public float bottomLimit;

    // Start is called before the first frame update
    void Awake()
    {
        sharedinstance = this;
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 destination = new Vector3(tarjet.position.x + offset.x, offset.y, -10);

        //if (this.transform.position.x - destination.x > 0)
        //    destination.x = this.transform.position.x;

        this.transform.position = Vector3.SmoothDamp(this.transform.position, destination, ref velocity, dampTime);


        switch (LevelManager.sharedInstance.currentGameMode)
        {
            case GameMode.levels:
                transform.position = new Vector3(
                    Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
                    Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
                    transform.position.z
                    );
                break;
        }

    }

    private void OnDrawGizmos()
    {
        //Bordes de una caja para ver los limites de la camara
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(rightLimit, topLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(leftLimit, topLimit));
        Gizmos.DrawLine(new Vector2(rightLimit, bottomLimit), new Vector2(rightLimit, topLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(rightLimit, bottomLimit));
    }

    public void ResetCameraPosition()
    {
        Vector3 destination = new Vector3(tarjet.position.x + offset.x, offset.y, -10);

        this.transform.position = destination;
    }
}
