using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo2 : MonoBehaviour {

        // variable donde se va a indicar a qué punto queremos dirigir la plataforma
        public Transform target;

        // velocidad de movimiento de la plataforma
        public float speed;

        // posiciones inicial y final de la plataforma
        private Vector3 start, end;

        // Use this for initialization
        void Start()
        {
            if (target != null)
            {
                // de esta forma el hijo de target ya no es hijo de plataforma movil A
                target.parent = null;
                // posición inicial de la plataforma
                start = transform.position;
                // posición final de la plataforma
                end = target.position;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

    void FixedUpdate()
    {
        if (target != null)
        {
            float platSpeed = speed * Time.deltaTime;
            // Para mover hacia a adelante es con "MoveTorwards(posición original, posición al que 
            // queremos dirigir la plataforma, 
            transform.position = Vector3.MoveTowards(transform.position, target.position, platSpeed);
        }



        // Recupero el enemigo de la escena
        GameObject enemigo2 = GameObject.FindGameObjectWithTag("enemigo2");

        // Recupero el target
        GameObject destino = GameObject.FindGameObjectWithTag("Target2");

        // Para hacer que el enemigo regrese intercambiamos las posiciones de inicio y target
        // Comprobamos si el personaje ha llegado al final de su recorrido
        if (transform.position == target.position)
        {
            // asignamos la nueva posición del target. Si la posición final es igual al principio (ambos coinciden) 
            // entonces el principio está al final y viceversa
            target.position = (target.position == start) ? end : start;

            // Giramos al personaje 
            if (destino.transform.position.x <= -1.6)
            {
                enemigo2.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                enemigo2.GetComponent<SpriteRenderer>().flipX = false;
            }

        }
    }
  }


