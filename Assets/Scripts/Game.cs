using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    //Reference from Unity IDE
    public GameObject chesspiece;
    public Camera mainCamera;

    //Matrices needed, positions of each of the GameObjects
    //Also separate arrays for the players in order to easily keep track of them all
    //Keep in mind that the same objects are going to be in "positions" and "playerBlack"/"playerWhite"
    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];

    //current turn
    private string currentPlayer = "white";

    //Game Ending
    private bool gameOver = false;

    //Unity calls this right when the game starts, there are a few built in functions
    //that Unity can call for you
    public void Start()
    {
        mainCamera = Camera.main;

        playerWhite = new GameObject[] { Create("knight", 0, 0), Create("knight", 1, 0),
            Create("knight", 2, 0), Create("knight", 3, 0), Create("knight", 4, 0),
            Create("knight", 5, 0), Create("knight", 6, 0), Create("knight", 7, 0),
            Create("knight", 0, 1), Create("knight", 1, 1), Create("knight", 2, 1),
            Create("knight", 3, 1), Create("knight", 4, 1), Create("knight", 5, 1),
            Create("knight", 6, 1), Create("knight", 7, 1) };
        playerBlack = new GameObject[] { Create("black_knight", 0, 7), Create("black_knight",1,7),
            Create("black_knight",2,7), Create("black_knight",3,7), Create("black_knight",4,7),
            Create("black_knight",5,7), Create("black_knight",6,7), Create("black_knight",7,7),
            Create("black_knight", 0, 6), Create("black_knight", 1, 6), Create("black_knight", 2, 6),
            Create("black_knight", 3, 6), Create("black_knight", 4, 6), Create("black_knight", 5, 6),
            Create("black_knight", 6, 6), Create("black_knight", 7, 6) };

        //Set all piece positions on the positions board
        for (int i = 0; i < playerBlack.Length; i++)
        {
            SetPosition(playerBlack[i]);
            SetPosition(playerWhite[i]);
        }
    }
    
    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
        Chessman cm = obj.GetComponent<Chessman>(); //We have access to the GameObject, we need the script
        cm.name = name; //This is a built in variable that Unity has, so we did not have to declare it before
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate(); //It has everything set up so it can now Activate()
        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();

        //Overwrites either empty space or whatever was there
        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void NextTurn()
    {
        if (currentPlayer == "white")
        {
            currentPlayer = "black";
        }
        else
        {
            currentPlayer = "white";
        }

        RotateCameraToCurrentPlayer();

    }

    private void RotateCameraToCurrentPlayer()
    {
        if (mainCamera == null) mainCamera = Camera.main;

        if (currentPlayer == "white")
        {
            mainCamera.transform.position = new Vector3(0, 0, -10); // default position
            mainCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            mainCamera.transform.position = new Vector3(0, 0, -10); // same position
            mainCamera.transform.rotation = Quaternion.Euler(0, 0, 180); // flip view in 2D
        }

        RotateAllPieces();

    }

    private void RotateAllPieces()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                GameObject piece = GetPosition(x, y);
                if (piece != null)
                {
                    if (currentPlayer == "white")
                        piece.transform.rotation = Quaternion.Euler(0, 0, 0);
                    else
                        piece.transform.rotation = Quaternion.Euler(0, 0, 180);
                }
            }
        }
    }

    public void Update()
    {
        if (gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;

            //Using UnityEngine.SceneManagement is needed here
            SceneManager.LoadScene("Congrats!"); //Restarts the game by loading the scene over again
        }
    }
    
    public void Winner(string playerWinner)
    {
        gameOver = true;

        //Using UnityEngine.UI is needed here
        SceneManager.LoadScene("Menu");
    }   

    public void CheckWinByElimination()
    {
        int whiteCount = 0;
        int blackCount = 0;

        foreach (GameObject piece in playerWhite)
        {
            if (piece != null) whiteCount++;
        }

        foreach (GameObject piece in playerBlack)
        {
            if (piece != null) blackCount++;
        }

        if (whiteCount == 0)
        {
            Winner("black");
        }
        else if (blackCount == 0)
        {
            Winner("white");
        }
    }
    
    public GameObject[] GetPlayerBlack() 
    {
        return playerBlack;
    }

}
