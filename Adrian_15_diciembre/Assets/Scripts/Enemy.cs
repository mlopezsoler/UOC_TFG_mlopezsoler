using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    // velocidad del movimiento
    public float speed = 1.0f;

    private Rigidbody2D rigidbody;

    // Variable booleana que obliga girar al enemigo
    public static bool turnAround;

    //private Vector3 startPosition;



    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        //startPosition = this.transform.position;
    }


   /* private void Start()
    {
        this.transform.position = startPosition;
    }*/


    private void FixedUpdate()
    {
       float currentSpeed = speed;


       if (turnAround == true){
            // Gira al enemigo sobre su propio eje vertical 180 grados
            // Aquí la velocidad es positiva-------------------------------------------
            currentSpeed = -speed;
            this.transform.eulerAngles = new Vector3(0, 180.0f, 0);
        }else{
            // Se mantiene mirando en la dirección original
            // Aquí la velocidad es negativa--------------------------------------------
            currentSpeed = speed;
            this.transform.eulerAngles = new Vector3(0, 0, 0);
        }


        if(GameManager.sharedInstance.currentGameState == GameState.inGame){
              rigidbody.velocity = new Vector2(currentSpeed, rigidbody.velocity.y);        
        }
    }


}

