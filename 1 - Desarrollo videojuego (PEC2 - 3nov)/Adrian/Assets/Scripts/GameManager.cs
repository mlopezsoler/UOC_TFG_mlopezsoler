using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Posibles estados de la partida
public enum GameState
{
    menu,
    inGame,  // cuando se está jugando
    gameOver  // cuando ha terminado la partida
}


public class GameManager : MonoBehaviour
{
    // Varianle para referenciar al propio Game Manager
    public static GameManager sharedInstance;

    // Variable para indicar que en al inicio del juego se empieza en el menú principal
    public GameState currentGameState = GameState.menu;

    // Variables para referenciar el canvas
    public Canvas menuCanvas, gameCanvas, gameOverCanvas;


    private void Awake()
    {
        sharedInstance = this;
    }



    // el juego empieza en la pantalla del menú
    private void Start()
    {
        BackToMenu();
    }


    // Formas de empezar, pausar y salir por teclado además de poder hacerlo a través de la UI
    private void Update()
    {
        // La partida se inicia/reinicia si se pulsa la tecla S y el estado del juego es distinto a inGame (no se
        // está/estaba jugando)
        if (Input.GetKeyDown(KeyCode.S) && this.currentGameState != GameState.inGame)
        {
            StartGame();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            BackToMenu();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
    }



    // Método para iniciar el juego
    public void StartGame()
    {
        SetGameState(GameState.inGame);
        PlayerController.sharedInstance.StartGame(); // el manager es quien resetea la posición del personaje
    }


    // Método cuando muera el jugador
    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }


    // Método para ir al menú principal
    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }


    // Método para finalizar el videojuego
    public void ExitGame()
    {
        /*  #if UNITY_EDITOR
               UnityEditor.EditorApplication.isPlaying = false;
          #endif*/

        Application.Quit();
    }



    // Método que se encarga de cambiar el estado del juego
    void SetGameState(GameState newGameState)
    {

        // Posibles escenas que hay que llamar durante el transcurso de la partida
        if(newGameState == GameState.menu)
        {
            // escena del menú principal
            menuCanvas.enabled = true;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = false;
        }
        else if(newGameState == GameState.inGame)
        {
            // escena para jugar
            menuCanvas.enabled = false;
            gameCanvas.enabled = true;
            gameOverCanvas.enabled = false;
        }
        else if(newGameState == GameState.gameOver)
        {
            // escena de Game Over
            menuCanvas.enabled = false;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = true;
        }


        // El estado de juego actual llega por parámetro
        this.currentGameState = newGameState;

    }



}
