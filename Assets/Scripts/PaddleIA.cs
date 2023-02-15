using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleIA : PaddleScript
{
    public float iaDistance;

    public BallScript ball;
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ball = FindObjectOfType<BallScript>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        IAMovement();
    }

    void IAMovement()
    {
        if (!ball.IsStopped)
        {
            float vertical = 0;
            if (ball.transform.position.y > this.transform.position.y + iaDistance)
            {
                vertical += speed * Time.smoothDeltaTime;
            }
            else if (ball.transform.position.y < this.transform.position.y - iaDistance)
            {
                vertical -= speed * Time.smoothDeltaTime;

            }
            PaddleMovement(vertical);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
