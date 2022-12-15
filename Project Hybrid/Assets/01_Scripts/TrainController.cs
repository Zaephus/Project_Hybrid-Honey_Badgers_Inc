using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainController : MonoBehaviour {

    [SerializeField]
    private float moveSpeed = 1;

    [SerializeField]
    private Transform[] pathPoints = new Transform[3];

    private void Start() {}

    private void Update() {

        if(Input.GetKeyDown(KeyCode.Space)) {
            transform.position = Vector3.zero;
            transform.eulerAngles = Vector3.zero;
            StartCoroutine(MoveOverPathPoints());
        }

    }

    private IEnumerator MoveOverPathPoints() {

        float completion = 0f;

        Quaternion startRotation = transform.rotation;

        while(Vector3.Distance(transform.position, pathPoints[2].position) >= 0.01f) {
            Vector3 lerpOne = Vector3.Lerp(pathPoints[0].position, pathPoints[1].position, completion);
            Vector3 lerpTwo = Vector3.Lerp(pathPoints[1].position, pathPoints[2].position, completion);

            transform.position = Vector3.Lerp(lerpOne, lerpTwo, completion);
            transform.rotation = Quaternion.Lerp(startRotation, pathPoints[2].rotation, completion);

            completion += moveSpeed * Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.rotation = pathPoints[2].rotation;

    }

}