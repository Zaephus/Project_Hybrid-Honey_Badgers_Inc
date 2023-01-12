using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum GameState {
        MainMenu = 0,
        NextLevelMenu = 1,
        EndMenu = 2,
        Running = 3
    }
    [SerializeField]
    private GameState gameState = GameState.MainMenu;

    [SerializeField]
    private List<GameObject> levels = new List<GameObject>();
    [SerializeField]
    private int levelIndex = 0;

    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject nextLevelMenu;
    [SerializeField]
    private GameObject Endmenu;

    private void Start() {
        EndPoint.EndPointReached += EndLevel;
    }

    private void Update() {
        InputManager.Update();
    }

    private void StartLevel() {

    }

    private void PauseLevel() {
        
    }

    private void EndLevel() {
        Debug.Log("End Reached");
    }

}