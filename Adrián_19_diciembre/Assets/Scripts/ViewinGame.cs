// script para el canvas del juego (CanvasGame). Lo separamos del script del Game Manager (Game Manager solo para
// controlar el juego y ViewinGame para la todo lo relacionado con la interfaz gráfica cuando se está jugando. También
// se usa para la pantalla de Game Over


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;  // para utilizar elementos de la interfaz gráfica

public class ViewinGame : MonoBehaviour {

    // variable para referenciar la caja de texto donde aparecerá el contador de biberones
    public Text labelbiberones;
    public Text labeltiempo;
    public Text labelmaxtiempo;
	
	// Update is called once per frame
	void Update () {

        // si estamos en modo juego
        if (GameManager.sharedInstance.currentGameState == GameState.inGame ||
            GameManager.sharedInstance.currentGameState == GameState.gameOver)
        {
            int biberones = GameManager.sharedInstance.collectedBiberones;
            this.labelbiberones.text = biberones.ToString();
        }

/*
 /*

        if (GameManager.sharedInstance.currentGameState == GameState.endGold || 
            GameManager.sharedInstance.currentGameState == GameState.endSilver ||
            GameManager.sharedInstance.currentGameState == GameState.endBronze)
       {
            int biberones = GameManager.sharedInstance.collectedBiberones;
            if (biberones == 1)
            {
                GameManager.sharedInstance.Bronze();
            }
            else if (biberones == 3)
            {
                GameManager.sharedInstance.Silver();
            }
            else if (biberones == 5)
            {
                GameManager.sharedInstance.Bronze();
            }
        }
 */
 


    }
}
