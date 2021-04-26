using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Posibles estados del juego
public enum GameState
{
    menu,
    inGame,
    gameOver
}


public class GameManager : MonoBehaviour
{
    //Variable para saber en que estado estamos. En principio queremos que esté en el menú
    public GameState currentGameState = GameState.menu;

    //Variable que referencia al propio GameManager
    public static GameManager sharedInstance;


    private void Awake()
    {
        sharedInstance = this;
    }


    private void Start()
    {
        BackToMenu();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Start") && currentGameState == GameState.menu)
            StartGame();

        else if (Input.GetButtonDown("Start") && currentGameState == GameState.inGame)
            BackToMenu();

        else if (Input.GetButtonDown("Start") && currentGameState == GameState.gameOver)
            ResetGame();
    }


    //Se utiliza para salir del menu. Entra al juego y le da la velocidad pausa al player
    public void StartGame()
    {
        SetGameState(GameState.inGame);
        PlayerController.sharedInstance.ResumeVelocity();

        //wall
        MovingWall.sharedInstance.MoveWall();
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
        SetGameState(GameState.menu);
        PlayerController.sharedInstance.PauseVelocity();

        //wall
        MovingWall.sharedInstance.StopWall();

    }

    //Encargado de cambiar el estado del juego
    void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.menu)
        { }

        else if (newGameState == GameState.inGame)
        { }

        else if (newGameState == GameState.gameOver)
        { }

        //Se asigna el estado nuevo
        this.currentGameState = newGameState;
    }
}
