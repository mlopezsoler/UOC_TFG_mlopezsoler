
// Este script se encarga de manejar el jugador y sus movimientos


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    // instancia compartida para crear el singleton del jugador
    public static PlayerController sharedInstance;

    public Animator animator;

    // velocidad de gateo
    public float crawlSpeed = 0.8f; 

    private Rigidbody2D rigidbody;

    // variable para la posición inicial del personaje
    private Vector3 startPosition;

    [HideInInspector]
    public bool usingLadder = false;


    // Configuración de variables
    private void Awake() 
    {
        // inicialización del singleton para que solo haya un jugador
        sharedInstance = this; 
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
	private void Update ()
    {
        // el personaje solo puede saltar si el juego está en modo InGame
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
                 if (Input.GetKeyDown(KeyCode.Z))
                {
                // el jugador ha pulsado la tecla z
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
        // recupero las distintas capas del background para poder moverlo o pararlo
        GameObject capa1 = GameObject.FindGameObjectWithTag("capa1");
        GameObject capa2 = GameObject.FindGameObjectWithTag("capa2");
        GameObject capa3 = GameObject.FindGameObjectWithTag("capa3");
        GameObject capa4 = GameObject.FindGameObjectWithTag("capa4");
        GameObject capa5 = GameObject.FindGameObjectWithTag("capa5");
        GameObject capa6 = GameObject.FindGameObjectWithTag("capa6");
        GameObject capa11 = GameObject.FindGameObjectWithTag("capa11");
        GameObject capa22 = GameObject.FindGameObjectWithTag("capa22");
        GameObject capa33 = GameObject.FindGameObjectWithTag("capa33");
        GameObject capa44 = GameObject.FindGameObjectWithTag("capa44");
        GameObject capa55 = GameObject.FindGameObjectWithTag("capa55");
        GameObject capa66 = GameObject.FindGameObjectWithTag("capa66");

        // recupero el script Parallax de cada capa
        parallax parallax1 = capa1.GetComponent<parallax>();
        parallax parallax2 = capa2.GetComponent<parallax>();
        parallax parallax3 = capa3.GetComponent<parallax>();
        parallax parallax4 = capa4.GetComponent<parallax>();
        parallax parallax5 = capa5.GetComponent<parallax>();
        parallax parallax6 = capa6.GetComponent<parallax>();
        parallax parallax11 = capa11.GetComponent<parallax>();
        parallax parallax22 = capa22.GetComponent<parallax>();
        parallax parallax33 = capa33.GetComponent<parallax>();
        parallax parallax44 = capa44.GetComponent<parallax>();
        parallax parallax55 = capa55.GetComponent<parallax>();
        parallax parallax66 = capa66.GetComponent<parallax>();


        // el personaje solo puede moverse si el juego está en modo InGame
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            // Gateo hacia la derecha cuando se pulsa el cursor con la flecha derecha
            if (Input.GetKey(KeyCode.RightArrow))
            {
                // Al pulsar la flecha para que el personaje se mueva a la derecha, el fondo se mueve también
                parallax1.speed = 4.5f;
                parallax2.speed = 3.5f;
                parallax3.speed = 2.3f;
                parallax4.speed = 1.6f;
                parallax5.speed = 1.4f;
                parallax6.speed = 1.0f;
                parallax11.speed = 4.5f;
                parallax22.speed = 3.5f;
                parallax33.speed = 2.3f;
                parallax44.speed = 1.6f;
                parallax55.speed = 1.4f;
                parallax66.speed = 1.0f;


                if (GetComponent<SpriteRenderer>().flipX == true)
                {
                    GetComponent<SpriteRenderer>().flipX = false;
                }
                animator.SetBool("gatear", true);
                transform.Translate(0.04f, 0, 0);
            }


            if (Input.GetKey(KeyCode.LeftArrow))
            {
                // Al pulsar la flecha para que el personaje se mueva a la izquierda, el fondo se mueve también
                parallax1.speed = 4.5f;
                parallax2.speed = 3.5f;
                parallax3.speed = 2.3f;
                parallax4.speed = 1.6f;
                parallax5.speed = 1.4f;
                parallax6.speed = 1.0f;
                parallax11.speed = 4.5f;
                parallax22.speed = 3.5f;
                parallax33.speed = 2.3f;
                parallax44.speed = 1.6f;
                parallax55.speed = 1.4f;
                parallax66.speed = 1.0f;


                if (GetComponent<SpriteRenderer>().flipX == false)
                {
                    GetComponent<SpriteRenderer>().flipX = true;
                }
                animator.SetBool("gatear", true);
                transform.Translate(-0.04f, 0, 0);
            }


            if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                // Al dejar de pulsar las flechas el fondo se para
                parallax1.speed = 0.0f;
                parallax2.speed = 0.0f;
                parallax3.speed = 0.0f;
                parallax4.speed = 0.0f;
                parallax5.speed = 0.0f;
                parallax6.speed = 0.0f;
                parallax11.speed = 0.0f;
                parallax22.speed = 0.0f;
                parallax33.speed = 0.0f;
                parallax44.speed = 0.0f;
                parallax55.speed = 0.0f;
                parallax66.speed = 0.0f;

                animator.SetBool("gatear", false);
            }
        }


        /*if (!usingLadder)
        {
            animator.SetFloat("vSpeed", rigidbody.velocity.y);
        }*/

    }



    void Jump()
    {
        // F = m * a 
        if (IsTouchingTheGround())
        {   
            // si el personaje va hacia la izquierda,  x es negativo
            if (GetComponent<SpriteRenderer>().flipX == true)
            {
                rigidbody.velocity = new Vector3(-1, 5, 0);
            }
            // si el personaje va hacia la derecha,  x es negativa
            else if (GetComponent<SpriteRenderer>().flipX == false)
            {
                rigidbody.velocity = new Vector3(1, 5, 0);
            }
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
        if(Physics2D.Raycast(this.transform.position,Vector2.down, 0.6f, groundLayer))
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


    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        /*if(otherCollider.tag == "Enemy"){
            Kill();
        }*/

        if (otherCollider.tag == "Roca")
        {
            Kill();
        }
    }

}
