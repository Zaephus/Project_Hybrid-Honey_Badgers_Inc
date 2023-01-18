using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level : MonoBehaviour {

    public bool IsPaused {
        get {
            return isPaused;
        }
        set {
            if(value && countdownTimer > 0) {
                //StopCoroutine(Countdown());
                StopAllCoroutines();
            }
            else if(!value && countdownTimer > 0) {
                StartCoroutine(Countdown());
            }
            else if(value && countdownTimer <= 0) {
                trainController.IsPaused = true;
            }
            else if(!value && countdownTimer <= 0) {
                trainController.IsPaused = false;
            }
            isPaused = value;
        }
    }
    private bool isPaused;

    public TrainController trainController;

    [SerializeField]
    private int countdownDuration;
    private int countdownTimer = 100;

    [SerializeField]
    private GameObject countdownCanvas;
    [SerializeField]
    private TMP_Text countdownText;

    private void Start() {

        countdownCanvas.SetActive(true);
        countdownTimer = countdownDuration;
        countdownText.text = countdownTimer.ToString();

        //StartCoroutine(Countdown());

    }

    // private void Update() {
    //     if(isPaused && countdownTimer > 0) {
    //         StopCoroutine(Countdown());
    //         trainController.IsPaused = true;
    //     }
    //     else if(!isPaused && countdownTimer > 0) {
    //         StartCoroutine(Countdown());
    //     }
    //     else if(isPaused && countdownTimer <= 0) {
    //         trainController.IsPaused = true;
    //     }
    //     else if(!isPaused && countdownTimer <= 0) {
    //         trainController.IsPaused = false;
    //     }
    // }

    private IEnumerator Countdown() {

        trainController.IsPaused = true;
        
        while(countdownTimer > 0) {
            yield return new WaitForSeconds(1f);
            countdownTimer--;
            countdownText.text = countdownTimer.ToString();
        }

        countdownCanvas.SetActive(false);
        trainController.IsPaused = false;

    }
}
