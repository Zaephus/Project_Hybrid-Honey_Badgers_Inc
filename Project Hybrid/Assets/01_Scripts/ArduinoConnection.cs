using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public static class ArduinoConnection
{
    public static string usbPort;
    public static int bautRate;
    private static SerialPort dataStream;

    public static float greenSwitchData;
    public static float yellowSwitchData;
    public static float blueSwitchData;
    public static float redSwitchData;

    private static string recievedString;
    private static string[] recievedData;

    public static void Awake()
    {
        dataStream = new SerialPort(usbPort, bautRate);
    }

    public static void Start()
    {
        dataStream.Open(); //initiate stream
    }

    public static void Update()
    {
        recievedString = dataStream.ReadLine();
        recievedData = recievedString.Split(",");

        greenSwitchData = float.Parse(recievedData[0]);
        yellowSwitchData = float.Parse(recievedData[1]);
        blueSwitchData = float.Parse(recievedData[2]);
        redSwitchData = float.Parse(recievedData[3]);
    }

}