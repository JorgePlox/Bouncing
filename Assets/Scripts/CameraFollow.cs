using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

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



            switch (LevelManager.sharedInstance.currentGameMode)
            {
                case GameMode.infinite:
                    Vector3 destination2 = new Vector3(target.position.x + offset.x, offset.y, -10);

                this.transform.position = Vector3.SmoothDamp(this.transform.position, destination2, ref velocity, dampTime);
                    break;

                case GameMode.levels:
                Vector3 destination = new Vector3(target.position.x + offset.x, target.position.y + offset.y, -10);

                this.transform.position = Vector3.SmoothDamp(this.transform.position, destination, ref velocity, dampTime);
                transform.position = new Vector3(
                        Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
                        Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
                        transform.position.z);
                    break;
                case GameMode.none:
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
        Vector3 destination = new Vector3(target.position.x + offset.x, offset.y, -10);

        this.transform.position = destination;
    }

    public IEnumerator CameraShake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.position;

        float elapsed = 0.0f;


        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.position = new Vector3(originalPos.x + x, originalPos.y + y, transform.position.z);

            elapsed += Time.deltaTime;

            yield return null;

        }

    }
}
