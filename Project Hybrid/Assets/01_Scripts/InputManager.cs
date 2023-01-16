using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager {

    public static Action GreenSwitchPressed;
    public static Action YellowSwitchPressed;
    public static Action BlueSwitchPressed;
    public static Action RedSwitchPressed;

    public static Action AnySwitchPressed;
    
    public static bool isLevelPaused;

    public static void Update() {

        if(Input.GetKeyDown(KeyCode.A)) {
            if(!isLevelPaused) {
                GreenSwitchPressed?.Invoke();
            }
            AnySwitchPressed?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.S)) {
            if(!isLevelPaused) {
                YellowSwitchPressed?.Invoke();
            }
            AnySwitchPressed?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.D)) {
            if(!isLevelPaused) {
                BlueSwitchPressed?.Invoke();
            }
            AnySwitchPressed?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.F)) {
            if(!isLevelPaused) {
                RedSwitchPressed?.Invoke();
            }
            AnySwitchPressed?.Invoke();
        }

    }

}