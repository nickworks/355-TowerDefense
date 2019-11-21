using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {


    [Tooltip("A list of all the levels to display on this screen.")]
    public LevelInfo[] levels;


    EventSystem events;

    void Start() {
        events = GetComponentInChildren<EventSystem>();
        
    }
    public void LoadLevel(string levelName) {
        SceneManager.LoadScene(levelName);
    }
    public void ExitGame() {
        Application.Quit();
    }
    void Update() {
        Focus();
    }
    void Focus() {
        if (events == null) return;
        if (events.currentSelectedGameObject != null) return;
        if (events.firstSelectedGameObject == null) return;
        events.SetSelectedGameObject(events.firstSelectedGameObject);
    }
}
