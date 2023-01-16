using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level : MonoBehaviour {
    public TrainController trainController;

    [SerializeField]
    private int countdownDuration;
    private int countdownTimer;

    [SerializeField]
    private GameObject countdownCanvas;
    [SerializeField]
    private TMP_Text countdownText;

    private void Start() {
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown() {

        countdownCanvas.SetActive(true);
        countdownTimer = countdownDuration;

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
