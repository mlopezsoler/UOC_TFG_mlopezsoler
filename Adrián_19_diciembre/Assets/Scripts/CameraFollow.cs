
// Script para que la cámara siga al jugador

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
 
    // variable para perseguir al objetivo, que será el jugador
    public Transform target;

    // en eje x a 0.1f (velocidad de la cámara en ese eje). En eje y a 0 para que se vea el suelo
    public Vector3 offset = new Vector3(0.1f, 0.0f, -10.0f);  

    // tiempo que estará quieta la cámara antes de empezar a moverse
    public float dampTime = 0.3f;

    // velocidad de movimiento de la cámara. Inicialmente parada
    public Vector3 velocity = Vector3.zero; 

    // Para que la actualización de frames sea constante
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }


    // método para resetear la cámara cuando se reinicia el juego
    // Lo mismo que Update, pero sin el barrido (SmoothDamp). Va a saltar directamente al inicio del escenario
    public void ResetCameraPosition()
    {
        Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
        Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(offset.x, offset.y, point.z));
        Vector3 destination = point + delta;
        destination = new Vector3(target.position.x, offset.y, offset.z);
        this.transform.position = destination;
    }



    // Update is called once per frame
    // Para que siga al target 
    void Update () {

        // coordenadas del personaje los transforma a coordenadas de pantalla, es decir,
        // al sistema de coordenadas de la cámara
        Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);

        // Movimiento que ha de realizar la cámara para que el personaje siga en el centro (delta)
        // Frame a frame: posición del personaje - donde esté la cámara ahora (la posición Z se mantiene a -10)
        // delta = donde quiero que esté - donde está ahora mismo
        Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(offset.x, offset.y, point.z));

        // Destino = donde estaba + pequeño incremento (desplazamiento)
        Vector3 destination = point + delta;

        // Corrección: para que la cámara solo se mueve en el eje x. Si el personaje sube y baja por el escenario, la cámara no sube ni baja, se mantiene fija 
        // en la coordenada y
        destination = new Vector3(target.position.x, offset.y, offset.z);

        // se asigna la posición de la cámara a destination
        // SmoothDamp mueve la cámara de forma continua, sin saltos
        // SmoothDamp (dónde estoy, dónde quiero ir, velocidad actual, damptime)
        this.transform.position = Vector3.SmoothDamp(this.transform.position, destination, ref velocity, dampTime);

	}
}
