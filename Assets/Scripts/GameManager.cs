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
    //Variable para saber en que estado estamos. En principio queremos que est� en el men�
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
        ResetGame();
    }



    //Se utiliza para salir del menu. Entra al juego y le da la velocidad pausa al player
    public void StartGame()
    {
        SetGameState(GameState.inGame);
        PlayerController.sharedInstance.ResumeVelocity();


        //Dependiendo del tipo de juego se inicia la Wall
        switch (LevelManager.sharedInstance.currentGameMode)
        {
            case GameMode.infinite:
                //wall
                MovingWall.sharedInstance.MoveWall();
                break;
        }


    }


    //Se reinicia el jeugo y sus variables
    public void ResetGame()
    {

        PlayerController.sharedInstance.StartGame();

        
        //Dependiendo del tipo de juego
        switch (LevelManager.sharedInstance.currentGameMode)
        {
            case GameMode.infinite:
                //Se remueven y generan los bloques del modo infinito
                LevelGenerator.sharedInstance.RemoveAllBlocks();
                LevelGenerator.sharedInstance.GenerateInitialBLocks();

                //wall
                MovingWall.sharedInstance.ResetWallPosition();
                MovingWall.sharedInstance.MoveWall();
                break;
            case GameMode.levels:
                break;
        }

        SetGameState(GameState.inGame);
        CameraFollow.sharedinstance.ResetCameraPosition();

        //reiniciar monedas
        collectedObjects = 0;

    }


    //Se acaba el juego 
    public void GameOver()
    {
        SetGameState(GameState.gameOver);
        PlayerController.sharedInstance.DeathVelocity();
    }

    //Se entra al men� y se guardan las velocidades del player para cuando se salga del menu
    public void BackToMenu()
    {
        SetGameState(GameState.pauseMenu);
        PlayerController.sharedInstance.PauseVelocity();

        //Dependiendo del tipo de juego se para la Wall
        switch (LevelManager.sharedInstance.currentGameMode)
        {
            case GameMode.infinite:
                //wall
                MovingWall.sharedInstance.StopWall();
                break;
        }


    }

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
