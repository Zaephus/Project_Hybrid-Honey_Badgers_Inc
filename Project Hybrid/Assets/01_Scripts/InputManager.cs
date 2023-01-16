using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InputManager {

    public static Action GreenSwitchPressed;
    public static Action YellowSwitchPressed;
    public static Action BlueSwitchPressed;
    public static Action RedSwitchPressed;

    public static Action<bool> GreenLeverPulled;
    public static Action<bool> YellowLeverPulled;
    public static Action<bool> BlueLeverPulled;
    public static Action<bool> RedLeverPulled;

    public static Action AnySwitchPressed;
    
    public static bool isLevelPaused;

    public static void Update() {

        #region keyboard

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

        #endregion

        #region levers

        if(ArduinoConnection.greenSwitchData <= 160) {
            GreenLeverPulled?.Invoke(false);
        }
        else if(ArduinoConnection.greenSwitchData >= 200) {
            GreenLeverPulled?.Invoke(true);
        }

        if(ArduinoConnection.yellowSwitchData <= 160) {
            YellowLeverPulled?.Invoke(false);
        }
        else if(ArduinoConnection.yellowSwitchData >= 200) {
            YellowLeverPulled?.Invoke(true);
        }

        if(ArduinoConnection.blueSwitchData <= 160) {
            BlueLeverPulled?.Invoke(false);
        }
        else if(ArduinoConnection.blueSwitchData >= 200) {
            BlueLeverPulled?.Invoke(true);
        }

        if(ArduinoConnection.redSwitchData <= 160) {
            RedLeverPulled?.Invoke(false);
        }
        else if(ArduinoConnection.redSwitchData >= 200) {
            RedLeverPulled?.Invoke(true);
        }

        #endregion

    }

}