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

    public static ArduinoConnection arduinoConnection;

    private static bool isInGreenRange;
    private static bool isInYellowRange;
    private static bool isInBlueRange;
    private static bool isInRedRange;

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

        if(arduinoConnection.greenSwitchData >= 160 && arduinoConnection.greenSwitchData <= 200 && !isInGreenRange) {
            isInGreenRange = true;
            if(!isLevelPaused) {
                GreenSwitchPressed?.Invoke();
            }
            AnySwitchPressed?.Invoke();
        }
        else if(arduinoConnection.greenSwitchData < 160 || arduinoConnection.greenSwitchData > 200) {
            isInGreenRange = false;
        }

        if(arduinoConnection.yellowSwitchData >= 160 && arduinoConnection.yellowSwitchData <= 200 && !isInYellowRange) {
            isInYellowRange = true;
            if(!isLevelPaused) {
                YellowSwitchPressed?.Invoke();
            }
            AnySwitchPressed?.Invoke();
        }
        else if(arduinoConnection.yellowSwitchData < 160 || arduinoConnection.yellowSwitchData > 200) {
            isInYellowRange = false;
        }

        if(arduinoConnection.blueSwitchData >= 160 && arduinoConnection.blueSwitchData <= 200 && !isInBlueRange) {
            isInBlueRange = true;
            if(!isLevelPaused) {
                BlueSwitchPressed?.Invoke();
            }
            AnySwitchPressed?.Invoke();
        }
        else if(arduinoConnection.blueSwitchData < 160 || arduinoConnection.blueSwitchData > 200) {
            isInBlueRange = false;
        }

        if(arduinoConnection.redSwitchData >= 160 && arduinoConnection.redSwitchData <= 200 && !isInRedRange) {
            isInRedRange = true;
            if(!isLevelPaused) {
                RedSwitchPressed?.Invoke();
            }
            AnySwitchPressed?.Invoke();
        }
        else if(arduinoConnection.redSwitchData < 160 || arduinoConnection.redSwitchData > 200) {
            isInRedRange = false;
        }

        #endregion

    }

}