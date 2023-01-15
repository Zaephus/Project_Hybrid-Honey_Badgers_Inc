using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum GameState {
        StartMenu = 0,
        NextLevelMenu = 1,
        EndMenu = 2,
        Running = 3
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
                    endMenu.SetActive(false);
                    break;

                case GameState.NextLevelMenu:
                    startMenu.SetActive(false);
                    nextLevelMenu.SetActive(true);
                    endMenu.SetActive(false);
                    break;

                case GameState.EndMenu:
                    startMenu.SetActive(false);
                    nextLevelMenu.SetActive(false);
                    endMenu.SetActive(true);
                    break;

                case GameState.Running:
                    startMenu.SetActive(false);
                    nextLevelMenu.SetActive(false);
                    endMenu.SetActive(false);
                    break;

            }

            state = value;
        }
    }

    private GameState state;

    [SerializeField]
    private List<GameObject> levels = new List<GameObject>();
    [SerializeField]
    private int levelIndex = 0;

    [SerializeField]
    private GameObject startMenu;
    [SerializeField]
    private GameObject nextLevelMenu;
    [SerializeField]
    private GameObject endMenu;

    private void Start() {

        gameState = GameState.StartMenu;

        EndPoint.EndPointReached += EndLevel;

        levelIndex = 0;
        InputManager.AnySwitchPressed += NextLevel;

    }

    private void Update() {
        InputManager.Update();
    }

    private void StartLevel() {

    }

    private void NextLevel() {

        if(levelIndex >= 0 && levelIndex < levels.Count) {
            GameObject level = Instantiate(levels[levelIndex], transform.position, Quaternion.identity);
            gameState = GameState.Running;
        }
        else {
            Debug.LogWarning("Level " + levelIndex + " does not exist!");
        }

    }

    private void EndLevel() {
        Debug.Log("End Reached");
    }

}