
// Este script se encarga de manejar el jugador y sus movimientos


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // instancia compartida para crear el singleton del jugador
    public static PlayerController sharedInstance;

    // variable pública para la fuerza del salto
    public float jumpForce = 6f;

    public Animator animator;

    // velocidad de gateo
    public float crawlSpeed = 0.8f; 

    private Rigidbody2D rigidbody;

    // variable para la posición inicial del personaje
    private Vector3 startPosition;

    [HideInInspector]
    public bool usingLadder = false;


    private void Awake() // arranque del juego por primera vez
    {
        sharedInstance = this; // inicialización del singleton para que solo haya un jugador
        rigidbody = GetComponent<Rigidbody2D>();
        // esta variable toma el valor de donde empieza la primera vez el personaje
        startPosition = this.transform.position; 
     }


    // Use this for initialization
    public void StartGame () {
        animator = this.GetComponent<Animator>();
        animator.SetBool("isGrounded", true);
        // cada vez que se reinicie la partida, el personaje se coloca en startPosition (en el mismo sitio inicial) 
        this.transform.position = startPosition;
    }

	
	// Update is called once per frame
	private void Update () {
        // el personaje solo puede saltar si el juego está en modo InGame
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // el jugador ha pulsado la tecla de espacio
                //animator.SetInteger("MovimientoPersonaje", value:2);
                Jump();
            }
            // Se le asigna a la animación el mismo valor que el método "IsTouchingTheGround" 
            animator.SetBool("IsGrounded", IsTouchingTheGround());
        }
    }
    

    // para aplicar fuerzas constantes mejor aquí que en el Update
    void FixedUpdate()
    {
        // el personaje solo puede moverse si el juego está en modo InGame
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            // Gateo hacia la derecha cuando se pulsa el cursor con la flecha derecha
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (GetComponent<SpriteRenderer>().flipX == true)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                animator.SetBool("gatear", true);
                transform.Translate(0.04f, 0, 0);
            }


            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (GetComponent<SpriteRenderer>().flipX == false)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                animator.SetBool("gatear", true);
                transform.Translate(-0.04f, 0, 0);
            }

            if(Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                animator.SetBool("gatear", false);
            }

           


            if (!usingLadder)
            {
                animator.SetFloat("vSpeed", rigidbody.velocity.y);
            }
        }
    }



    void Jump()
    {
        // F = m * a 
        if (IsTouchingTheGround())
        {
            // Impulse aplica toda la fuerza en un instante
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); 
        }  
    }


    // Creamos una Layermask (capa de objetos) para detectar el suelo 
    public LayerMask groundLayer;

    

    // método booleano para saber si el personaje está tocando el suelo
    // Devuelve true si lo está tocando y false si no es así
    bool IsTouchingTheGround()
    {
        // si se dan estas condiciones, traza un rayo desde la posición del personaje en dirección hacia abajo (la base del collider) y 
        // como máximo a 20 cm, entonces se choca contra el suelo (se encuentra con la capa del suelo)
        if(Physics2D.Raycast(this.transform.position,Vector2.down, 0.8f, groundLayer))
        {
            return true; // está tocando el suelo
        } else
        {
            return false; // no está tocando el suelo
        }
    }


    // método para matar al jugador
    public void Kill()
    {
        GameManager.sharedInstance.GameOver();
        //this.animator.SetBool("isAlive", false);
    }


    /*private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if(otherCollider.tag == "Enemy"){
            Kill();
        }

        if (otherCollider.tag == "Roca")
        {
            // muere
        }
    }*/



}
