using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public  class ArduinoConnection : MonoBehaviour
{
    public string usbPort;
    public int bautRate;
    private SerialPort dataStream;

    public float greenSwitchData;
    public float yellowSwitchData;
    public float blueSwitchData;
    public float redSwitchData;

    private  string recievedString;
    private  string[] recievedData;

    public  void Awake()
    {
        dataStream = new SerialPort(usbPort, bautRate);
    }

    public  void Start()
    {
        dataStream.Open(); //initiate stream
    }

    public  void Update()
    {
        recievedString = dataStream.ReadLine();
        recievedData = recievedString.Split(",");

        greenSwitchData = float.Parse(recievedData[0]);
        yellowSwitchData = float.Parse(recievedData[1]);
        blueSwitchData = float.Parse(recievedData[2]);
        redSwitchData = float.Parse(recievedData[3]);
    }

}