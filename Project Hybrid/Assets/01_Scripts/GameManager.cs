using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private void Start() {
        EndPoint.EndPointReached += EndLevel;
    }

    private void Update() {
        InputManager.Update();
    }

    private void EndLevel() {
        Debug.Log("End Reached");
    }

}