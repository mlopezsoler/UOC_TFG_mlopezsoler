using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movCamara : MonoBehaviour {

    public GameObject personaje;
    private Vector3 posicion;

	// Use this for initialization
	void Start () {
        posicion = transform.position - personaje.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = personaje.transform.position + posicion;
		
	}
}
