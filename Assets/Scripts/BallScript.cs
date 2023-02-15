using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class BallScript : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float randomAngle = 3f;

    private bool isStopped = true;
    private Rigidbody2D rb;

    public bool IsStopped { get => isStopped; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        GameController.onStartGame += StartBall;
        PointCollision.OnPlayer1PointMade += ResetPosition;
        PointCollision.OnPlayer2PointMade += ResetPosition;
    }
    private void OnDisable()
    {
        GameController.onStartGame -= StartBall;
        PointCollision.OnPlayer1PointMade -= ResetPosition;
        PointCollision.OnPlayer2PointMade -= ResetPosition;
    }

    public void StartBall()
    {
        ResetPosition();
        ShootBallRandomSide();
    }

    void ShootBallRandomSide()
    {
        int randomSide = Random.Range(0, 2);
        switch (randomSide)
        {
            case 0:
                rb.AddForce(new Vector2(Random.Range(0.7f, 1f), Random.Range(-1f, 1f)) * speed);
                break;
            case 1:
                rb.AddForce(new Vector2(Random.Range(-0.7f, -1f), Random.Range(-1f, 1f)) * speed);
                break;
        }
        isStopped = false;
    }

    public void StartBall(PaddleScript.Player player)
    {
        if (isStopped)
        {
            if (player == PaddleScript.Player.Player1)
            {
                rb.AddForce(new Vector2(Random.Range(-0.7f, -1f), Random.Range(-1f, 1f)) * speed);
            }
            if (player == PaddleScript.Player.Player2)
            {
                rb.AddForce(new Vector2(Random.Range(0.7f, 1f), Random.Range(-1f, 1f)) * speed);
            }
            isStopped = false;
        }
    }

    public void ResetPosition()
    {
        rb.velocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        transform.position = Vector3.zero;
        isStopped = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PointCollision>())
            return;

        Vector2 velocityTweak = new Vector2(0, Random.Range(0, randomAngle));

        rb.velocity += velocityTweak;

        if (rb.velocity.magnitude < 35)
        {
            rb.velocity *= 1.1f;
        }
    }
}
