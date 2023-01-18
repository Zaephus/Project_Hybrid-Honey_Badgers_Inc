using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum GameState {
        StartMenu = 0,
        NextLevelMenu = 1,
        FailedMenu = 2,
        EndMenu = 3,
        PauseMenu = 4,
        Running = 5
    }
    [SerializeField]
    private GameState gameState {
        get {
            return state;
        }
        set {
            switch(value) {

                case GameState.StartMenu:

                    EscapePressed -= PauseGame;

                    levelIndex = -1;

                    startMenu.SetActive(true);
                    nextLevelMenu.SetActive(false);
                    failedMenu.SetActive(false);
                    endMenu.SetActive(false);
                    pauseMenu.SetActive(false);

                    buttonCanvas.SetActive(false);
                    
                    AudioManager.instance.StopAll();
                    AudioManager.instance.Play("Main Menu Music");
                    if(currentLevel != null) {
                        Destroy(currentLevel.gameObject);
                        currentLevel = null;
                    }

                    break;

                case GameState.NextLevelMenu:

                    EscapePressed -= PauseGame;

                    startMenu.SetActive(false);
                    nextLevelMenu.SetActive(true);
                    failedMenu.SetActive(false);
                    endMenu.SetActive(false);
                    pauseMenu.SetActive(false);

                    buttonCanvas.SetActive(false);

                    if(currentLevel != null) {
                        currentLevel.IsPaused = true;
                    }

                    break;

                case GameState.FailedMenu:

                    EscapePressed -= PauseGame;

                    startMenu.SetActive(false);
                    nextLevelMenu.SetActive(false);
                    failedMenu.SetActive(true);
                    endMenu.SetActive(false);
                    pauseMenu.SetActive(false);

                    buttonCanvas.SetActive(false);

                    if(currentLevel != null) {
                        currentLevel.IsPaused = true;
                    }

                    break;

                case GameState.EndMenu:

                    EscapePressed -= PauseGame;

                    startMenu.SetActive(false);
                    nextLevelMenu.SetActive(false);
                    failedMenu.SetActive(false);
                    endMenu.SetActive(true);
                    pauseMenu.SetActive(false);

                    buttonCanvas.SetActive(false);

                    if(currentLevel != null) {
                        currentLevel.IsPaused = true;
                    }

                    break;

                case GameState.PauseMenu:

                    EscapePressed -= PauseGame;

                    startMenu.SetActive(false);
                    nextLevelMenu.SetActive(false);
                    failedMenu.SetActive(false);
                    endMenu.SetActive(false);
                    pauseMenu.SetActive(true);

                    buttonCanvas.SetActive(false);

                    if(currentLevel != null) {
                        currentLevel.IsPaused = true;
                    }

                    break;

                case GameState.Running:

                    EscapePressed += PauseGame;

                    startMenu.SetActive(false);
                    nextLevelMenu.SetActive(false);
                    failedMenu.SetActive(false);
                    endMenu.SetActive(false);
                    pauseMenu.SetActive(false);

                    buttonCanvas.SetActive(true);

                    AudioManager.instance.Stop("Main Menu Music");
                    AudioManager.instance.Play("Ambient Sound");

                    if(currentLevel != null) {
                        currentLevel.IsPaused = false;
                    }

                    break;

            }

            state = value;
        }
    }

    public static Action GreenSwitchPressed;
    public static Action YellowSwitchPressed;
    public static Action BlueSwitchPressed;
    public static Action RedSwitchPressed;

    private GameState state;

    [SerializeField]
    private List<GameObject> levels = new List<GameObject>();
    private int levelIndex = -1;
    private Level currentLevel;

    [SerializeField]
    private GameObject startMenu;
    [SerializeField]
    private GameObject nextLevelMenu;
    [SerializeField]
    private GameObject failedMenu;
    [SerializeField]
    private GameObject endMenu;
    [SerializeField]
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject buttonCanvas;

    private Action EscapePressed;

    private void Start() {

        if(Display.displays.Length > 1) {
            Display.displays[1].Activate();
            Display.displays[1].SetParams(1920, 1080, 0 , 0);
        }

        gameState = GameState.StartMenu;

        EndPoint.EndPointReached += EndLevel;
        TrainController.HitDeadEnd += HitDeadEnd;

        levelIndex = -1;

    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            EscapePressed?.Invoke();
        }
    }

    public void RestartLevel() {

        if(currentLevel != null) {
            Destroy(currentLevel.gameObject);
        }

        if(levelIndex >= 0 && levelIndex < levels.Count) {
            GameObject level = Instantiate(levels[levelIndex], transform.position, Quaternion.identity);
            currentLevel = level.GetComponent<Level>();
            gameState = GameState.Running;
        }
        else {
            Debug.LogWarning("Level " + levelIndex + " does not exist!");
        }

    }

    public void ButtonPressed(string _colour) {

        switch(_colour) {

            case "Green":
                GreenSwitchPressed?.Invoke();
                break;

            case "Yellow":
                YellowSwitchPressed?.Invoke();
                break;

            case "Blue":
                BlueSwitchPressed?.Invoke();
                break;
            
            case "Red":
                RedSwitchPressed?.Invoke();
                break;

        }

    }

    public void ResumeLevel() {
        gameState = GameState.Running;
    }

    public void NextLevel() {

        levelIndex++;

        if(currentLevel != null) {
            Destroy(currentLevel.gameObject);
        }

        if(levelIndex >= 0 && levelIndex < levels.Count) {
            GameObject level = Instantiate(levels[levelIndex], transform.position, Quaternion.identity);
            currentLevel = level.GetComponent<Level>();
            gameState = GameState.Running;
        }
        else {
            Debug.LogWarning("Level " + levelIndex + " does not exist!");
        }

    }

    private void EndLevel() {
        Debug.Log("End Reached");
        if(levelIndex == levels.Count-1) {
            gameState = GameState.EndMenu;
        }
        else {
            gameState = GameState.NextLevelMenu;
        }
    }

    private void HitDeadEnd() {
        gameState = GameState.FailedMenu;
    }

    public void ReturnToStart() {
        gameState = GameState.StartMenu;
    }

    private void PauseGame() {
        gameState = GameState.PauseMenu;
    }

}