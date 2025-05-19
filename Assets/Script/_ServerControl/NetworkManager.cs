using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Script._ServerControl;
using Script._ServerControl.Session;
using ServerCore;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance;
    
    MultiLobbyManager multiLobbyManager;
    
    WifiLinkController wifiLinkController;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            wifiLinkController = new WifiLinkController();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("NetworkManager is already exist");
            Destroy(gameObject);
        }
    }
    
    ServerSession _session = new ServerSession();

    public void Send(ArraySegment<byte> sendBuff)
    {
        _session.Send(sendBuff);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        EstablishConnection();
    }

    private void EstablishConnection()
    {
        // DNS(Domain Name System
        IPAddress ipAddr = IPAddress.Parse("192.168.219.100"); 
        IPEndPoint endPoint = new IPEndPoint(ipAddr, 8088);
        Connector connector = new Connector();
        connector.Connect(endPoint, () =>
        {
            return _session;
        },1);
    }

    // Update is called once per frame
    void Update()
    {
        List<IPacket> list = PacketQueue.Instance.PopAll();

        foreach (var packet in list)
        {
            PacketManager.Instance.HandlePacket(_session, packet);
        }
    }
    
    public void disconnect()
    {
        _session.DisConnect();
    }
}
