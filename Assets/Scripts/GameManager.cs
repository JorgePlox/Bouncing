using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Posibles estados del juego
public enum GameState
{
    pauseMenu,
    inGame,
    gameOver
}


public class GameManager : MonoBehaviour
{
    //Variable para saber en que estado estamos. En principio queremos que esté en el menú
    public GameState currentGameState = GameState.pauseMenu;

    //Variable que referencia al propio GameManager
    public static GameManager sharedInstance;

    //Variables de los canvases
    public Canvas menuCanvas, gameCanvas, gameOverCanvas;

    //CContador de coleccionables
    public int collectedObjects = 0;


    private void Awake()
    {
        sharedInstance = this;
    }


    private void Start()
    {
        StartGame();
    }

    private void Update()
    {
        //if (Input.GetButtonDown("Start") && currentGameState == GameState.menu)
        //    StartGame();

        //else if (Input.GetButtonDown("Start") && currentGameState == GameState.inGame)
        //    BackToMenu();

        //else if (Input.GetButtonDown("Start") && currentGameState == GameState.gameOver)
        //    ResetGame();
    }


    //Se utiliza para salir del menu. Entra al juego y le da la velocidad pausa al player
    public void StartGame()
    {
        SetGameState(GameState.inGame);
        PlayerController.sharedInstance.ResumeVelocity();

        //wall
        MovingWall.sharedInstance.MoveWall();

        //reiniciar monedas
        collectedObjects = 0;
    }


    //Se reinicia el jeugo y sus variables
    public void ResetGame()
    {
        PlayerController.sharedInstance.StartGame();
        SetGameState(GameState.inGame);
        CameraFollow.sharedinstance.ResetCameraPosition();

        //wall
        MovingWall.sharedInstance.ResetWallPosition();
    }


    //Se acaba el juego 
    public void GameOver()
    {
        SetGameState(GameState.gameOver);
        PlayerController.sharedInstance.DeathVelocity();
    }

    //Se entra al menú y se guardan las velocidades del player para cuando se salga del menu
    public void BackToMenu()
    {
        SetGameState(GameState.pauseMenu);
        PlayerController.sharedInstance.PauseVelocity();

        //wall
        MovingWall.sharedInstance.StopWall();

    }
    //Exit game. En moviles no se utiliza.
    //public void ExitGame()
    //{
    //    Application.Quit();
    //}

    //Encargado de cambiar el estado del juego
    void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.pauseMenu)
        {
            menuCanvas.enabled = true;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = false;
        }

        else if (newGameState == GameState.inGame)
        {
            menuCanvas.enabled = false;
            gameCanvas.enabled = true;
            gameOverCanvas.enabled = false;
        }

        else if (newGameState == GameState.gameOver)
        {
            menuCanvas.enabled = false;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = true;
        }

        //Se asigna el estado nuevo
        this.currentGameState = newGameState;
    }

    public void CollectObjects(int value)
    {
        this.collectedObjects += value;
        Debug.Log("Puntos:" + collectedObjects);
    }



}
