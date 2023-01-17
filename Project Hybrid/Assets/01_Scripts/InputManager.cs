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

    public static ArduinoConnection arduinoConnection;

    private static float lastGreenReading;
    private static float lastYellowReading;
    private static float lastBlueReading;
    private static float lastRedReading;

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

        if(arduinoConnection.greenSwitchData <= 160 && lastGreenReading > 160) {
            lastGreenReading = 160;
            GreenLeverPulled?.Invoke(false);
            AnySwitchPressed?.Invoke();
        }
        else if(arduinoConnection.greenSwitchData >= 200 && lastGreenReading < 200) {
            lastGreenReading = 160;
            GreenLeverPulled?.Invoke(true);
            AnySwitchPressed?.Invoke();
        }
        else {
            lastGreenReading = arduinoConnection.greenSwitchData;
        }

        if(arduinoConnection.yellowSwitchData <= 160 && lastYellowReading > 160) {
            lastYellowReading = 160;
            YellowLeverPulled?.Invoke(false);
            AnySwitchPressed?.Invoke();
        }
        else if(arduinoConnection.yellowSwitchData >= 200 && lastYellowReading < 200) {
            lastYellowReading = 160;
            YellowLeverPulled?.Invoke(true);
            AnySwitchPressed?.Invoke();
        }
        else {
            lastYellowReading = arduinoConnection.yellowSwitchData;
        }

        if(arduinoConnection.blueSwitchData <= 160 && lastBlueReading > 160) {
            lastBlueReading = 160;
            BlueLeverPulled?.Invoke(false);
            AnySwitchPressed?.Invoke();
        }
        else if(arduinoConnection.blueSwitchData >= 200 && lastBlueReading < 200) {
            lastBlueReading = 160;
            BlueLeverPulled?.Invoke(true);
            AnySwitchPressed?.Invoke();
        }
        else {
            lastBlueReading = arduinoConnection.blueSwitchData;
        }

        if(arduinoConnection.redSwitchData <= 160 && lastRedReading > 160) {
            lastRedReading = 160;
            RedLeverPulled?.Invoke(false);
            AnySwitchPressed?.Invoke();
        }
        else if(arduinoConnection.redSwitchData >= 200 && lastRedReading < 200) {
            lastRedReading = 160;
            RedLeverPulled?.Invoke(true);
            AnySwitchPressed?.Invoke();
        }
        else {
            lastRedReading = arduinoConnection.redSwitchData;
        }

        #endregion

    }

}