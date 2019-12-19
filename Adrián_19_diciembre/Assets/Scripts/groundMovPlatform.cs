using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundMovPlatform : MonoBehaviour {

    private PlayerController player;

	// Use this for initialization
	void Start () {
        player = GetComponent<PlayerController>();
	}

    void OnCollisionStay2D(Collision2D col)
    {
        // Si el personaje toca la plataforma móvil
        if (col.gameObject.tag == "plataformaMovil")
        {
            // Cambia el padre y pasa a ser el de la plataforma. 
            // De esta manera conseguimos que el personaje se mueve sobre ella
            player.transform.parent = col.transform;
        }
    }


    void OnCollisionExit2D(Collision2D col){
         // Si el personaje toca la plataforma móvil
         if (col.gameObject.tag == "plataformaMovil")
         {
             // Cambia el padre y pasa a ser el de la plataforma. 
             // De esta manera conseguimos que el personaje se mueve sobre ella
             player.transform.parent = null;
         }
     }      
}
