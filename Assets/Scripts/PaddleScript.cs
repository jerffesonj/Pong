using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Contexts;
using UnityEditor.U2D.Path.GUIFramework;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PaddleScript : MonoBehaviour
{
    [SerializeField] protected float speed;
   
    protected Rigidbody2D rb;

    public enum Player
    {
        Player1,
        Player2
    }
    public Player player;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        if (player == Player.Player2)
            GameController.playerAIListener += ActivateIA;
    }
    private void OnDisable()
    {
        if (player == Player.Player2)
            GameController.playerAIListener -= ActivateIA;
    }

    public void ActivateIA()
    {
        if (GetComponent<PaddleIA>())
        {
            GetComponent<PaddleIA>().enabled = true;
            this.enabled = false;
        }
    }

    private void Update()
    {
        OnMoveP1(Input.GetAxis("VerticalP1"));
        OnMoveP2(Input.GetAxis("VerticalP2"));
    }

    public void OnMoveP1(float verticalValue)
    {
        if (player != Player.Player1)
            return;

        if (GameController.instance.vs == GameController.Vs.PvP || GameController.instance.vs == GameController.Vs.PvIA)
        {
            PaddleMovement(verticalValue);
        }

    }
    public void OnMoveP2(float verticalValue)
    {
        if (player != Player.Player2)
            return;
        if (GameController.instance.vs == GameController.Vs.PvP && player == Player.Player2)
        {
            PaddleMovement(verticalValue);
        }
    }

    protected void PaddleMovement(float vertical)
    {
        rb.velocity = new Vector2(0, vertical * speed * Time.fixedDeltaTime);
    }
}

