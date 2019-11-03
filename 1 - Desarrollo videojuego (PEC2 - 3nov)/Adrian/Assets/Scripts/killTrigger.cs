using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killTrigger : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Jugador")
        {
            //Debug.Log("El jugador ha entrado en una zona de muerte");
            PlayerController.sharedInstance.Kill(); 
        }
    }
}
