using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coleccionable : MonoBehaviour {

    // variable para saber si el personaje ha recogido el biberón o no
    bool isCollected = false;

    // varibale para dar valor 1 al biberón para poder contarlos 
    public int value = 1;

    // variable para el sonido al coger biberones
    public AudioClip collectSound;
    
    //public AudioClip collectSound2;
    //public AudioClip collectSound3;


    // método para activar el biberón y su animación
    void Show()
    {
        // se activa la imagen del biberon
        this.GetComponent<SpriteRenderer>().enabled = true;
        // se activa el collider para que pueda recogerse el biberón
        this.GetComponent<CapsuleCollider2D>().enabled = true;
        // partimos de la base de que no se ha cogido el binerón actual
        isCollected = false;
    }

    // método para desactivar el biberón y su collider (ocultarlo)
    void Hide()
    {
        // se oculta la imagen del biberon
        this.GetComponent<SpriteRenderer>().enabled = false;
        // se oculta el collider
        this.GetComponent<CapsuleCollider2D>().enabled = false;
    }

    // método para recolectar el biberón
    void Collect()
    {
        // cuando se recolecta el biberón
        isCollected = true;
        // después se llama al método Hide() para que lo oculte
        Hide();

        /* Solo puede haber un AudioSource, por lo que se van cambiando los sonidos según el tipo de collectsound
        Es por eso que en el inspector no se pone nada en AudioSource, para que podamos poner más de un sonido. Si solo
        fuera uno, con ponerlo en el inspector ya funcionaría
    */
        // Reprocude una el audio una vez por cada biberón
        AudioSource audio = GetComponent<AudioSource>();

        if (audio != null && this.collectSound != null)
        {
            audio.PlayOneShot(this.collectSound);
        }

        // el Game Manager se encarga de recolectar el biberón por valor de la variable value
        GameManager.sharedInstance.CollectBiberones(value);
    }


    // método para el personaje entre en el collider del biberón
    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.tag == "Jugador")
        {
            Collect();
        }
    }



}
