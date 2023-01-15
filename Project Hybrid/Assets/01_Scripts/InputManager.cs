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

    public static void Update() {

        if(Input.GetKeyDown(KeyCode.A)) {
            GreenSwitchPressed?.Invoke();
            AnySwitchPressed?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.S)) {
            YellowSwitchPressed?.Invoke();
            AnySwitchPressed?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.D)) {
            BlueSwitchPressed?.Invoke();
            AnySwitchPressed?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.F)) {
            RedSwitchPressed?.Invoke();
            AnySwitchPressed?.Invoke();
        }

    }

}