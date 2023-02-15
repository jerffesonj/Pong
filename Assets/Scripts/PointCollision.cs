using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCollision : MonoBehaviour
{
    public PaddleScript.Player player;
   
    public delegate void PointsListener(int points, int pointsP2);
    public static event PointsListener OnPointMade;
    
    public delegate void PointMadeListener();
    public static event PointMadeListener OnPlayer1PointMade;
    public static event PointMadeListener OnPlayer2PointMade;

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<BallScript>())
        {
            if (player == PaddleScript.Player.Player1)
            {
                StartCoroutine(P1IEnum(collision));
            }
            if (player == PaddleScript.Player.Player2)
            {
                StartCoroutine(P2IEnum(collision));
            }
        }
    }

    IEnumerator P1IEnum(Collision2D collider)
    {
        BallScript ball = collider.gameObject.GetComponent<BallScript>();
        if (!ball)
            yield break;

        OnPlayer2PointMade?.Invoke();
        OnPointMade?.Invoke(GameController.instance.pointsP1, GameController.instance.pointsP2);

        yield return new WaitForSeconds(1);
        
        ball.StartBall(player);
    }
    IEnumerator P2IEnum(Collision2D collider)
    {
        BallScript ball = collider.gameObject.GetComponent<BallScript>();

        if (!ball)
            yield break;
        OnPlayer1PointMade?.Invoke();
        OnPointMade?.Invoke(GameController.instance.pointsP1, GameController.instance.pointsP2);

        yield return new WaitForSeconds(1);
        
        ball.StartBall(player);
    }
}
