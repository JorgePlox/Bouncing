using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{

    //bool isCollected = false;
    public int value = 0;


    //Sonido
    public AudioClip sonido;
    private AudioSource audioSource;

    //Se decide activar y desactivar el objeto debido a que en el modo infinito, eliminar un coleccionable, lo eliminaría de todos
    //los trozos de nivel
    //Activar el collecionable y su collider
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void Show()
    {
        //Activar sprite y también la animación
        this.GetComponent<SpriteRenderer>().enabled = true;
        //Activar collider
        this.GetComponent<CapsuleCollider2D>().enabled = true;
        //No se ha recogido el coleccionable
        //isCollected = false;

    }

    //Desactivar el collecionable y su collider
    void Hide()
    {
        //Desactivar sprite y también la animación
        this.GetComponent<SpriteRenderer>().enabled = false;
        //Desactivar collider
        this.GetComponent<CapsuleCollider2D>().enabled = false;


    }

    //Recoger el colecionable
    void Collect()
    {
        //isCollected = true;
        Hide();
        
        
        if (sonido != null)
        {
            audioSource.PlayOneShot(sonido);
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "Player")
        {
            Collect();
            GameManager.sharedInstance.CollectObjects(value);
        }
    }
}
