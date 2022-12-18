using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrack : BaseTrack
{

    public Transform[] pathOne;
    public Transform[] pathTwo;

    public BaseTrack nextTrackOne;
    public BaseTrack nextTrackTwo;

    private enum SwitchState { One, Two};
    [SerializeField] private SwitchState state;

    private enum InputType {
        Green = 0,
        Yellow = 1,
        Blue = 2,
        Red = 3
    }
    [SerializeField] private InputType inputType;

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
            pathPoints = pathOne;
            nextTrack = nextTrackOne;
        }
        else
        {
            pathPoints = pathTwo;
            nextTrack = nextTrackTwo;
        }
    }

    public void ChangeTracks()
    {
        Debug.Log(name + "with type of " + inputType + "switched.");
        if(state == SwitchState.One)
        {
            state = SwitchState.Two;
            pathPoints = pathTwo;
            nextTrack = nextTrackOne;
        }
        else
        {
            state = SwitchState.One;
            pathPoints = pathOne;
            nextTrack = nextTrackTwo;
        }
    }
}
