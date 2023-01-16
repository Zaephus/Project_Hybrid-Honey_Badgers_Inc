using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrack : BaseTrack {

    public TrackPath pathOne;
    public TrackPath pathTwo;

    [SerializeField]
    private GameObject switchStateOneIndicator;
    [SerializeField]
    private GameObject switchStateTwoIndicator;

    private enum SwitchState { 
        One, 
        Two
    };
    [SerializeField]
    private SwitchState state;

    public Transform iconTransform;

    public GameObject greenInputIcon;
    public GameObject yellowInputIcon;
    public GameObject blueInputIcon;
    public GameObject redInputIcon;

    public enum InputType {
        Green = 0,
        Yellow = 1,
        Blue = 2,
        Red = 3
    }
    public InputType inputType;

    private void Start() {
        switch(inputType) {
            case InputType.Green:
                InputManager.GreenSwitchPressed += ChangeTracks;
                break;

            case InputType.Yellow:
                InputManager.YellowSwitchPressed += ChangeTracks;
                break;

            case InputType.Blue:
                InputManager.BlueSwitchPressed += ChangeTracks;
                break;

            case InputType.Red:
                InputManager.RedSwitchPressed += ChangeTracks;
                break;

        }
    }

    public void ChangeTracksInEditor()
    {
        if (state == SwitchState.One)
        {
            path.pathPoints = pathOne.pathPoints;
            switchStateOneIndicator.GetComponent<MeshRenderer>().enabled = true;
            switchStateTwoIndicator.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            path.pathPoints = pathTwo.pathPoints;
            switchStateOneIndicator.GetComponent<MeshRenderer>().enabled = false;
            switchStateTwoIndicator.GetComponent<MeshRenderer>().enabled = true;
        }
    }

    public void ChangeTracks()
    {
        Debug.Log(name + "with type of " + inputType + "switched.");
        if(state == SwitchState.One)
        {
            state = SwitchState.Two;
            path.pathPoints = pathTwo.pathPoints;
            switchStateOneIndicator.GetComponent<MeshRenderer>().enabled = false;
            switchStateTwoIndicator.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            state = SwitchState.One;
            path.pathPoints = pathOne.pathPoints;
            switchStateOneIndicator.GetComponent<MeshRenderer>().enabled = true;
            switchStateTwoIndicator.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void OnDestroy() {
        switch(inputType) {
            case InputType.Green:
                InputManager.GreenSwitchPressed -= ChangeTracks;
                break;

            case InputType.Yellow:
                InputManager.YellowSwitchPressed -= ChangeTracks;
                break;

            case InputType.Blue:
                InputManager.BlueSwitchPressed -= ChangeTracks;
                break;

            case InputType.Red:
                InputManager.RedSwitchPressed -= ChangeTracks;
                break;

        }
    }
}
