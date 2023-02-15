using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> menuObjects = new List<GameObject>();
    [SerializeField] private List<GameObject> playObjects = new List<GameObject>();
    [SerializeField] private List<GameObject> gameOverObject = new List<GameObject>();
    [SerializeField] private Button menuButton;

    private EventSystem eventSystem;

    private void Start()
    {
        eventSystem = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<EventSystem>();
        ShowMenuObjects();
    }

    private void ActiveMenuObjects(bool value)
    {
        foreach (GameObject menu in menuObjects)
        {
            menu.SetActive(value);
        }
    }
    private void ActivePlayObjects(bool value)
    {
        foreach (GameObject play in playObjects)
        {
            play.SetActive(value);
        }
    }
    private void ActiveGameOverObjects(bool value)
    {
        foreach (GameObject gameOver in gameOverObject)
        {
            gameOver.SetActive(value);
        }
    }

    #region Methods called by buttons on canvas
    public void ShowMenuObjects()
    {
        ActiveMenuObjects(true);
        ActivePlayObjects(false);
        ActiveGameOverObjects(false);
        eventSystem.SetSelectedGameObject(menuButton.gameObject);
    }

    public void ShowPlayObjects()
    {
        ActiveMenuObjects(false);
        ActivePlayObjects(true);
        ActiveGameOverObjects(false);
    }

    public void ShowGameOverObjects()
    {
        ActiveMenuObjects(false);
        ActivePlayObjects(false);
        ActiveGameOverObjects(true);
    }
    #endregion
}
