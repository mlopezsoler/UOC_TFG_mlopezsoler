using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour {

    public float speed = 0.0f;

    private Rigidbody2D rigidbody;


	// Use this for initialization
	void Awake () {
        this.rigidbody = GetComponent<Rigidbody2D>();
	}
	

	// Update is called once per frame
	void FixedUpdate () {
        this.rigidbody.velocity = new Vector2(speed, 0);

        // Se necesita la posición del padre (la cámara) en el eje x
        float parentPosition = this.transform.parent.transform.position.x;

        // Si se ha desplazado más allá de lo que se ve en la pantalla
        if(this.transform.position.x - parentPosition >= 22.4f)
        {
            this.transform.position = new Vector3(parentPosition -22.9f, this.transform.position.y, this.transform.position.z);
        }
	}
}
