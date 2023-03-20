using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using DarkRift;
using DarkRift.Client;
using DarkRift.Client.Unity;
using UnityEngine;

public class ClientSccript : MonoBehaviour
{
    [SerializeField] private UnityClient client;

    [SerializeField] private Transform _position;
    // Start is called before the first frame update
    void Start()
    {
        client = GetComponent<UnityClient>();
        client.MessageReceived += OnMessageReceived;
    }

    private void OnMessageReceived(object sender, MessageReceivedEventArgs e)
    {
        Message reqMess = e.GetMessage();
        if (reqMess.Tag == (ushort)MessageTag.UserWalkRes)
        {
            using (DarkRiftReader reader = reqMess.GetReader())
            {
                _position.position += new Vector3(reader.ReadSingle(), 0, reader.ReadSingle());
            }
        }
        reqMess.Dispose();
    }

    private void ConnectCallback(Exception e)
    {
        Debug.Log("Connection successful");
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalkForward = Input.GetKey(KeyCode.W);
        bool isWalkLeft = Input.GetKey(KeyCode.A);
        bool isWalkBackward = Input.GetKey(KeyCode.S);
        bool isWalkRight = Input.GetKey(KeyCode.D);
        if(isWalkBackward || isWalkForward || isWalkLeft || isWalkRight)
            SendDataToHost(isWalkForward, isWalkBackward, isWalkLeft, isWalkRight);
    }

    private void SendDataToHost(bool isWalkForward, bool isWalkBackward, bool isWalkLeft, bool isWalkRight)
    {
        DarkRiftWriter write = DarkRiftWriter.Create();
        write.Write(isWalkForward);
        write.Write(isWalkBackward);
        write.Write(isWalkLeft);
        write.Write(isWalkRight);
        Message mes = Message.Create((ushort)MessageTag.UserWalkReq, write);
        client.SendMessage(mes, SendMode.Reliable);
        mes.Dispose();
        write.Dispose();
    }
    enum MessageTag : ushort
    {
        UserWalkReq = 0, UserWalkRes = 1
    }
}
