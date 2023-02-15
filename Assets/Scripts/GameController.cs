using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using static PointCollision;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    MenuScript menu;
    GameOverScript gameOver;
    
    public int pointsP1;
    public int pointsP2;

    public delegate void PlayerAIListener();
    public static PlayerAIListener playerAIListener;

    public delegate void OnStartGame();
    public static OnStartGame onStartGame;

    public int maxPoints;
    public enum Vs
    {
        PvP,
        PvIA
    }
    public Vs vs;

    private void Awake()
    {
        instance = this;
    }

    #region Delegates Initialization
    private void OnEnable()
    {
        OnPlayer1PointMade += AddPointsP1;
        OnPlayer1PointMade += EndGame;

        OnPlayer2PointMade += AddPointsP2;
        OnPlayer2PointMade += EndGame;
    }
    private void OnDisable()
    {
        OnPlayer1PointMade += AddPointsP1;
        OnPlayer1PointMade -= EndGame;
            
        OnPlayer2PointMade += AddPointsP2;
        OnPlayer2PointMade -= EndGame;
    }
    #endregion

    private void Start()
    {
        menu = FindObjectOfType<MenuScript>();
        gameOver = FindObjectOfType<GameOverScript>();
    }

    public void StartGameP1Routine()
    {
        StartCoroutine(StartGameP1Enum());
    }
    bool startingGame = false;
    IEnumerator StartGameP1Enum()
    {
        if (startingGame)
            yield break;
        ResetPoints();

        startingGame = true;
        menu.ShowPlayObjects();

        P1Play();

        yield return new WaitForSeconds(1f);

        StartGame();
        startingGame = false;
    }

    public void StartGameP2Routine()
    {
        StartCoroutine(StartGameP2Enum());
    }
    IEnumerator StartGameP2Enum()
    {
        if (startingGame)
            yield break;
        ResetPoints();

        startingGame = true;
        menu.ShowPlayObjects();

        P2Play();

        yield return new WaitForSeconds(1f);

        StartGame();
        startingGame = false;
    }

    void StartGame()
    {
        onStartGame?.Invoke();
    }
    
    void P1Play()
    {
        vs = Vs.PvIA;
        playerAIListener?.Invoke();
    }
    void P2Play()
    {
        vs = Vs.PvP;
    }

    void ResetPoints()
    {
        pointsP1 = 0;
        pointsP2 = 0;
    }

    void AddPointsP1()
    {
        pointsP1 += 1;
    }
    void AddPointsP2()
    {
        pointsP2 += 1;
    }

    void EndGame()
    {
        if (vs == Vs.PvIA)
        {
            if (pointsP1 >= maxPoints)
            {
                gameOver.SetWinner("You Win", pointsP1, pointsP2);
                menu.ShowGameOverObjects();

            }
            else if (pointsP2 >= maxPoints)
            {
                gameOver.SetWinner("You Lose", pointsP1, pointsP2);
                menu.ShowGameOverObjects();
            }
        }
        else
        {
            if (pointsP1 >= maxPoints)
            {
                gameOver.SetWinner("Player 1 Win", pointsP1, pointsP2);
                menu.ShowGameOverObjects();
            }
            else if (pointsP2 >= maxPoints)
            {
                gameOver.SetWinner("Player 2 Win", pointsP1, pointsP2);
                menu.ShowGameOverObjects();
            }
        }
    }
}
