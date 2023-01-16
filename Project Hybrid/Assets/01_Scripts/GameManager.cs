using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum GameState {
        StartMenu = 0,
        NextLevelMenu = 1,
        FailedMenu = 2,
        EndMenu = 3,
        Running = 4
    }
    [SerializeField]
    private GameState gameState {
        get {
            return state;
        }
        set {
            switch(value) {

                case GameState.StartMenu:

                    startMenu.SetActive(true);
                    nextLevelMenu.SetActive(false);
                    failedMenu.SetActive(false);
                    endMenu.SetActive(false);

                    InputManager.isLevelPaused = true;
                    if(currentLevel != null) {
                        Destroy(currentLevel.gameObject);
                        currentLevel = null;
                    }
        
                    InputManager.AnySwitchPressed += NextLevel;

                    break;

                case GameState.NextLevelMenu:

                    startMenu.SetActive(false);
                    nextLevelMenu.SetActive(true);
                    failedMenu.SetActive(false);
                    endMenu.SetActive(false);

                    InputManager.isLevelPaused = true;
                    if(currentLevel != null) {
                        currentLevel.trainController.isPaused = true;
                    }
        
                    InputManager.AnySwitchPressed += NextLevel;

                    break;

                case GameState.FailedMenu:

                    startMenu.SetActive(false);
                    nextLevelMenu.SetActive(false);
                    failedMenu.SetActive(true);
                    endMenu.SetActive(false);

                    InputManager.isLevelPaused = true;
                    if(currentLevel != null) {
                        currentLevel.trainController.isPaused = true;
                    }

                    InputManager.AnySwitchPressed += RestartLevel;

                    break;

                case GameState.EndMenu:

                    startMenu.SetActive(false);
                    nextLevelMenu.SetActive(false);
                    failedMenu.SetActive(false);
                    endMenu.SetActive(true);

                    InputManager.isLevelPaused = true;
                    if(currentLevel != null) {
                        currentLevel.trainController.isPaused = true;
                    }

                    InputManager.AnySwitchPressed += EndGame;

                    break;

                case GameState.Running:

                    startMenu.SetActive(false);
                    nextLevelMenu.SetActive(false);
                    failedMenu.SetActive(false);
                    endMenu.SetActive(false);

                    InputManager.isLevelPaused = false;
                    if(currentLevel != null) {
                        currentLevel.trainController.isPaused = false;
                    }

                    break;

            }

            state = value;
        }
    }

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

    private void Start() {

        gameState = GameState.StartMenu;

        EndPoint.EndPointReached += EndLevel;
        TrainController.HitDeadEnd += HitDeadEnd;

        levelIndex = -1;

    }

    private void Update() {
        InputManager.Update();
    }

    private void RestartLevel() {

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

        InputManager.AnySwitchPressed -= RestartLevel;

    }

    private void NextLevel() {

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

        InputManager.AnySwitchPressed -= NextLevel;

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

    private void EndGame() {
        gameState = GameState.StartMenu;
        InputManager.AnySwitchPressed -= EndGame;
    }

}