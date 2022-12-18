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
