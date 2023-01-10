using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class ArduinoConnection : MonoBehaviour
{
    [SerializeField] string usbPort;
    [SerializeField] int bautRate;
    SerialPort dataStream;

    private string recievedString;
    private string[] recievedData;

    public float greenSwitchData;
    public float yellowSwitchData;
    public float blueSwitchData;
    public float redSwitchData;

    private void Awake()
    {
        dataStream = new SerialPort(usbPort, bautRate);
    }

    private void Start()
    {
        dataStream.Open(); //initiate stream
    }

    private void Update()
    {
        recievedString = dataStream.ReadLine();
        recievedData = recievedString.Split(",");

        greenSwitchData = float.Parse(recievedData[0]);
        yellowSwitchData = float.Parse(recievedData[1]);
        blueSwitchData = float.Parse(recievedData[2]);
        redSwitchData = float.Parse(recievedData[3]);
    }
}
