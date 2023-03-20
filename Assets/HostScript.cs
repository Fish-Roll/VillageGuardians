using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using DarkRift;
using DarkRift.Client;
using DarkRift.Client.Unity;
using UnityEngine;

public class HostScript : MonoBehaviour
{
    [SerializeField] private UnityClient client;
    [SerializeField] private float speed;
    void Start()
    {
        client = GetComponent<UnityClient>();
        client.MessageReceived += OnMessageReceived;
    }

    private void OnMessageReceived(object sender, MessageReceivedEventArgs e)
    {
        Message reqMess = e.GetMessage();
        if (reqMess.Tag == (ushort)MessageTag.UserWalkReq)
        {
            using (DarkRiftReader reader = reqMess.GetReader())
            {
                SendDataToHost(reader.ReadBoolean(), reader.ReadBoolean(), reader.ReadBoolean(), reader.ReadBoolean());
            }
        }
        reqMess.Dispose();
    }

    private void SendDataToHost(bool isWalkForward, bool isWalkBackward, bool isWalkLeft, bool isWalkRight)
    {
        DarkRiftWriter writer = DarkRiftWriter.Create();
        float x = 0;
        float z = 0;
        if (isWalkForward)
            x += speed;
        if (isWalkBackward)
            x -= speed;
        if (isWalkLeft)
            z += speed;
        if (isWalkRight)
            z -= speed;
        writer.Write(x);
        writer.Write(z);
        Message resMess = Message.Create((ushort)MessageTag.UserWalkRes, writer);
        client.SendMessage(resMess, SendMode.Reliable);
        resMess.Dispose();
        writer.Dispose();
    }
    private void ConnectCallback(Exception e)
    {
        Debug.Log("Connection successful");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    enum MessageTag : ushort
    {
        UserWalkReq = 0, UserWalkRes = 1
    }

}
