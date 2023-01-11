using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour {

    public static Action EndPointReached;

    private void OnTriggerEnter(Collider _other) {
        if(_other.GetComponent<TrainController>() != null) {
            EndPointReached?.Invoke();
        }
    }

}