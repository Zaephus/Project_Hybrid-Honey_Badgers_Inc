using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrack : BaseTrack
{
    private enum SwitchState { One, Two};
    [SerializeField] SwitchState state;

    [SerializeField] public Transform[] pathOne;
    [SerializeField] public Transform[] pathTwo;

    public void Switch()
    {
        if (state == SwitchState.One)
        {
            pathPoints = pathOne;
        }
        else
        {
            pathPoints = pathTwo;
        }
    }

    public void ChangeTracks()
    {
        if(state == SwitchState.One)
        {
            state = SwitchState.Two;
            pathPoints = pathTwo;
        }
        else
        {
            state = SwitchState.One;
            pathPoints = pathOne;
        }
    }
}
