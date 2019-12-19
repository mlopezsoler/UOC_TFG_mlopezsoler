
// ESte script se encarga de cambiar el estado del juego 


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Posibles estados de la partida
public enum GameState
{
    intro,     // introducción
    menu,     // menú del juego
    inGame,  // cuando se está jugando
    gameOver,  // cuando ha terminado la partida
    endGold, // cuando se supera el juego (medalla de oro)
    endSilver, // cuando se supera el juego (medalla de plata)
    endBronze // cuando se supera el juego (medalla de bronce)
}


public class GameManager : MonoBehaviour
{
    // Variable para referenciar al propio Game Manager
    public static GameManager sharedInstance;

    // Variable para indicar que en al inicio del juego se empieza con la pantalla intro
    public GameState currentGameState = GameState.intro;

    // Variables para referenciar el canvas
    public Canvas introCanvas, menuCanvas, gameCanvas, gameOverCanvas, endGoldCanvas, endSilverCanvas, endBronzeCanvas;

    // Variable para contar los biberones
    public int collectedBiberones = 0;


    private void Awake()
    {
        sharedInstance = this;
    }


    // el juego empieza en la pantalla con la introducción
    private void Start()
    {
        Intro();
    }


    // Formas de empezar, pausar y salir por teclado además de poder hacerlo a través de la UI
    private void Update()
    {
        // La partida se inicia/reinicia si se pulsa la tecla S y el estado del juego es distinto a inGame no se
        // está jugando). 
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

        // RESETEO DE LA POSICIÓN DEL PERSONAJE
        PlayerController.sharedInstance.StartGame();

        // RESETEO DE LA POSICIÓN DE LA CÁMARA
        // Recupero el objeto (la cámara) de la escena
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        // Ahora recupero el script CameraFollow
        CameraFollow cameraFollow = camera.GetComponent<CameraFollow>();
        // Invocamos al método que resetea la posición de la cámara
        cameraFollow.ResetCameraPosition();


        // en cada partida nueva el contador de los binerones vuelve a empezar desde cero 
        this.collectedBiberones = 0;
    }


    // Método cuando muera el jugador
    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }


    // Método para ir a la intro del juego
    public void Intro()
    {
        SetGameState(GameState.intro);
    }


    // Método para ir al menú principal
    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }


    // Método para ir a la pantalla final si se supera el juego (medalla de oro)
    public void Gold()
    {
        SetGameState(GameState.endGold);
    }

    // Método para ir a la pantalla final si se supera el juego (medalla de plata)
    public void Silver()
    {
        SetGameState(GameState.endSilver);
    }

    // Método para ir a la pantalla final si se supera el juego (medalla de bronce)
    public void Bronze()
    {
        SetGameState(GameState.endBronze);
    }


    // Método para finalizar el videojuego
    public void ExitGame()
    {
          #if UNITY_EDITOR
               UnityEditor.EditorApplication.isPlaying = false;
          #endif

       // Application.Quit();  // PONERLO CUANDO SE VAYA A CREAR EL EJECUTABLE
    }



    // Método que se encarga de cambiar el estado del juego
    void SetGameState(GameState newGameState)
    {

        // Posibles escenas que hay que llamar durante el transcurso de la partida
        if (newGameState == GameState.intro)
        {
            // escena del menú principal
            introCanvas.enabled = true;
            menuCanvas.enabled = false;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = false;
            endGoldCanvas.enabled = false;
            endSilverCanvas.enabled = false;
            endBronzeCanvas.enabled = false;
        }
        else if (newGameState == GameState.menu)
        {
            // escena del menú principal
            introCanvas.enabled = false;
            menuCanvas.enabled = true;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = false;
            endGoldCanvas.enabled = false;
            endSilverCanvas.enabled = false;
            endBronzeCanvas.enabled = false;
        }
        else if(newGameState == GameState.inGame)
        {
            // escena para jugar
            introCanvas.enabled = false;
            menuCanvas.enabled = false;
            gameCanvas.enabled = true;
            gameOverCanvas.enabled = false;
            endGoldCanvas.enabled = false;
            endSilverCanvas.enabled = false;
            endBronzeCanvas.enabled = false;
        }
        else if(newGameState == GameState.gameOver)
        {
            // escena de Game Over
            introCanvas.enabled = false;
            menuCanvas.enabled = false;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = true;
            endGoldCanvas.enabled = false;
            endSilverCanvas.enabled = false;
            endBronzeCanvas.enabled = false;
        }

        else if (newGameState == GameState.endGold)
        {
            // escena de Game Over
            introCanvas.enabled = false;
            menuCanvas.enabled = false;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = false;
            endGoldCanvas.enabled = true;
            endSilverCanvas.enabled = false;
            endBronzeCanvas.enabled = false;
        }

        else if (newGameState == GameState.endSilver)
        {
            // escena de Game Over
            introCanvas.enabled = false;
            menuCanvas.enabled = false;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = false;
            endGoldCanvas.enabled = false;
            endSilverCanvas.enabled = true;
            endBronzeCanvas.enabled = false;
        }

        else if (newGameState == GameState.endBronze)
        {
            // escena de Game Over
            introCanvas.enabled = false;
            menuCanvas.enabled = false;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = false;
            endGoldCanvas.enabled = false;
            endSilverCanvas.enabled = false;
            endBronzeCanvas.enabled = true;
        }


        // El estado de juego actual llega por parámetro
        this.currentGameState = newGameState;

    }

    // Aquí lo que hace es sumar el valor del objeto al valor de la variable
    public void CollectBiberones(int objectValue)
    {
        this.collectedBiberones += objectValue;
    }
}
