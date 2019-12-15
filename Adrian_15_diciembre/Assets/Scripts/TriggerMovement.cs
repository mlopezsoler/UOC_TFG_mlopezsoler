
// Informa al enemigo que hay un objeto cerca y que tiene que dar la vuelta 


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMovement : MonoBehaviour {

    // variable para mover hacia derecha/izquierda al enemigo
    public bool movingForward;

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Si se encuentra con un biberón no tiene que hacer nada
        if (otherCollider.tag == "Colectable"){
            return;
        }

        // Gira al enemigo
        if (movingForward == true){
            Enemy.turnAround = true;
        }
        // No lo gira
        else{
            Enemy.turnAround = false;
        }
       movingForward = !movingForward;
    }
}
