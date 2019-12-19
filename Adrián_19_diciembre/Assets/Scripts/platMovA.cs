using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platMovA : MonoBehaviour {

    // variable donde se va a indicar a qué punto queremos dirigir la plataforma
    public Transform target;

    // velocidad de movimiento de la plataforma
    public float speed;

    // posiciones inicial y final de la plataforma
    private Vector3 start, end;
    
    // Use this for initialization
	void Start () {
		if(target != null){
            // de esta forma el hijo de target ya no es hijo de plataforma movil A
            target.parent = null;
            // posición inicial de la plataforma
            start = transform.position;
            // posición final de la plataforma
            end = target.position;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        if(target != null){
            float platSpeed = speed * Time.deltaTime;
            // Para mover hacia a adelante es con "MoveTorwards(posición original, posición al que 
            // queremos dirigir la plataforma, 
            transform.position = Vector3.MoveTowards(transform.position, target.position, platSpeed);
        }

        // Para hacer que la plataforma regrese vamos a intercambiar las posiciones de la
        // plataforma (inicio y target)
        // Comprobamos si la plataforma ha llegado al final de su recorrido
        if(transform.position == target.position) {
            // asignamos la nueva posición del target. Si la posición final es igual al principio (ambos coinciden) 
            // entonces el principio está al final y viceversa, sino que queda al principio
            target.position = (target.position == start) ? end : start;
        }
    }
}
