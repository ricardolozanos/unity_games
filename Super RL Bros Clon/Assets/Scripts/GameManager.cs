using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Posibles estados del videojuego
public enum GameState
{
    menu,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{

    //Variable que referencia al propio Game Manager
    public static GameManager sharedInstance;

    //Variable para saber en qué estado del juego nos encontramos ahora mismo
    //Al inicio, queremos que empiece en el menú principal
    public GameState currentGameState = GameState.menu;

    public Canvas menuCanvas, gameCanvas, gameOverCanvas;

    public int collectedItems = 0;

    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        BackToMenu();
    }

    private void Update()
    { //presionar start, y solo si no esta vivo
        if (Input.GetButtonDown("Start") && this.currentGameState != GameState.inGame)
        {
            
            StartGame();
        }
        if (Input.GetButtonDown("Pause"))
        {
            BackToMenu();

            PlayerController.sharedInstance.StopCoroutine("TirePlayer");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGame();
        }
    }


    public void StartGame()// Metodo para iniciar el juego
    {
        SetGameState(GameState.inGame);
        

        if (PlayerController.sharedInstance.transform.position.x > 25) { 
        LevelGenerator.sharedInstance.RemoveAllTheBlocks();

        LevelGenerator.sharedInstance.GenerateInitialBlocks();
        }
        PlayerController.sharedInstance.StartGame();

        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraFollow cameraFollow = camera.GetComponent<CameraFollow>();
        cameraFollow.ResetCameraPosition();

        collectedItems = 0;

        

    }

    public void GameOver()//Metodo cuando el jugador muera
    {
        SetGameState(GameState.gameOver);
    }

    public void BackToMenu()//Metodo para volver al menu principal cuando se requiera
    {
        SetGameState(GameState.menu);
    }

    //metodo para cerrar la aplicacion
    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }


    void SetGameState(GameState newGameState)
    {
        if(newGameState == GameState.menu)
        {
            menuCanvas.enabled = true;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = false;


            //preparar la escena de Unity para el menu
        } else if (newGameState == GameState.inGame)
        {
            menuCanvas.enabled = false;
            gameCanvas.enabled = true;
            gameOverCanvas.enabled = false;


            //preparar la escena de Unity para el juego
        } else if (newGameState == GameState.gameOver)
        {
            menuCanvas.enabled = false;
            gameCanvas.enabled = false;
            gameOverCanvas.enabled = true;

            //preparar la escena de Unity para el gameover, estar muerto
        }



        //asignamos el estado de juego actual
        this.currentGameState = newGameState;
    }


    public void CollectObject(int objectValue)
    {
        this.collectedItems += objectValue;

        Debug.Log(collectedItems);
    }

}
