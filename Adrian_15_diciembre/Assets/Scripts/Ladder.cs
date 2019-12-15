using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour {

    Rigidbody2D rb;

    Animator myAnim;

    PlayerController controller;

    // Collider de la plataforma al extremo de las escaleras
    public BoxCollider2D platformGround;

    // Variable booleana para saber si el jugador está en la escalera
    [HideInInspector]
    public bool onLadder = false;

    // Variable para la velocidad de subida/baja por la escalera
    public float climbSpeed;


    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
	}


    private void OnTriggerStay2D(Collider2D otherCollider)
    {
        // Si el jugador se encuentra con la escalera (otherCollider es el collider de la escalera)
        if (otherCollider.CompareTag("escalera"))
        {
            // El jugador se está moviendo hacia arriba/abajo mientras el valor no sea 0
            if (Input.GetAxisRaw("Vertical") != 0)
            {
                // velocidad de movimiento
                rb.velocity = new Vector2(rb.velocity.x, Input.GetAxisRaw("Vertical") * climbSpeed);

                // Se le quita la gravedad cuando esté en la escalera para que no se caiga
                rb.gravityScale = 0;

                onLadder = true;

                // Se desactiva el collider de la plataforma para que el jugador no se atasque y pueda subir/bajar
                platformGround.enabled = false;

                controller.usingLadder = onLadder;
            }
            // si el jugador está en la escalera, pero no sube ni baja, se queda quieto
            else if (Input.GetAxisRaw("Vertical") == 0 && onLadder)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }

            // onLadder es true
            myAnim.SetBool("onLadder", onLadder);
            // La velocidad la transformamos instrucción matemática
            myAnim.SetFloat("vSpeed", Mathf.Abs(Input.GetAxisRaw("Vertical")));
        }
    }

    // Cuanbdo el jugador está fuera de la escalera
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("escalera") && onLadder)
        {
            rb.gravityScale = 1;
            onLadder = false;
            controller.usingLadder = onLadder;
            platformGround.enabled = true; // ya no está en la escalera y lo necesita para poder moverse

            myAnim.SetBool("onLadder", onLadder);
        }
    }
}
