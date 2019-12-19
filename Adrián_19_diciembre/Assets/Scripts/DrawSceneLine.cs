using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSceneLine : MonoBehaviour {

    public Transform from; // origen de la línea a dibujar
    public Transform to; // destino de la línea a dibujar
    
    
    // Método para dibujar la línea
	void OnDrawGizmosSelected () {
		if(from != null && to != null){
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(from.position, to.position);
            Gizmos.DrawSphere(from.position, 0.15f);
            Gizmos.DrawSphere(to.position, 0.15f);
        }
	}
	
}
