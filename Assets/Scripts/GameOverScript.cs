using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public TMP_Text result;
    public TMP_Text p1Points;
    public TMP_Text p2Points;
    public Button continueButton;
    
    private EventSystem eventSystem;
    private void Start()
    {
        eventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
    }

    public void SetWinner(string result, int p1Points, int p2Points)
    {
        this.result.text = result;
        this.p1Points.text = p1Points.ToString();
        this.p2Points.text = p2Points.ToString();

        eventSystem.SetSelectedGameObject(continueButton.gameObject);
    }
}
