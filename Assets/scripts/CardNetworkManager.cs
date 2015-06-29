using UnityEngine;
using System.Collections;
using System;

public class CardNetworkManager : MonoBehaviour
{
    //connection variables
    internal string connectionIP = "127.0.0.1";
    internal int connectionPort = 8000;

    //game objects and class variables
    private NetworkView cardNetworkView;
    private GameObject card;
    private GameObject cardNetworkViewObject;
    private GameObject menuObject;
    private CardManager cardManager;
    private Menu menu;

    //load all the needed objects and classes
    void Awake()
    {
        //loading NetworkView
        cardNetworkView = GetComponent<NetworkView>();

        //loading Menu
        menuObject = GameObject.Find("Menu");
        menu = menuObject.GetComponent<Menu>();

        DontDestroyOnLoad(this);
    }

    //get and set for connection IP
    public void setConnectionIP(string IP)
    {
        connectionIP = IP;
    }

    public string getConnectionIP()
    {
        return connectionIP;
    }

    //get and set for connection port
    public void setConnectionPort(int port)
    {
        connectionPort = port;
    }

    public int getConnectionPort()
    {
        return connectionPort;
    }

    //send card request to server
    internal void sendCardRequest(string cardType, string gameObjectName)
    {
        cardNetworkView.RPC("cardRequest", RPCMode.Server, cardType, gameObjectName);
    }

    //connect to server
    internal void connectToSerwer(string newConnecionIP, int newConnectionPort)
    {
        if (!connectionIP.Equals(newConnecionIP))
            connectionIP = newConnecionIP;

        if (connectionPort != newConnectionPort)
            connectionPort = newConnectionPort;

        try
        {
            Network.Connect(connectionIP, connectionPort);
        }
        catch (Exception)
        {
            menu.addInfo("Server is not running or check IP and port \n");
        }
    }

    // disconnecting form server
    internal void disconnect()
    {
        Network.Disconnect();
    }

    //register user
    internal void registerUser(string username, string password)
    {
        cardNetworkView.RPC("Register", RPCMode.Server, username, password);
    }

    // login user
    internal void loginUser(string username, string password)
    {
        cardNetworkView.RPC("Login", RPCMode.Server, username, password);
    }

    //keep the connection through the game
    internal void keepRunningConnection()
    {
        Network.isMessageQueueRunning = false;
        DontDestroyOnLoad(this);
        Application.LoadLevel("MainApp");
        Network.isMessageQueueRunning = true;
    }

    void OnConnectedToServer()
    {
        menu.addInfo("Connected to server \n");
    }

    void OnDisonnectedFromServer()
    {
        menu.addInfo("Disconnected from server");
    }

    //RPCs sent to server
    [RPC]
    void Register(string username, string password) { }

    [RPC]
    void Login(string username, string password) { }

    [RPC]
    void cardRequest(string cardType, string gameObjectName) { }


    // RPCs received from server

    //RPC to connect, register and login

    [RPC]
    void userRegistered()
    {
        menu.addInfo("User succesfully registered\n");
        menu.endOfRegisteration();
    }

    [RPC]
    void usernameExists()
    {
        menu.addInfo("Username already exists \n");
    }

    [RPC]
    void userNotFound()
    {
        menu.addInfo("Wrong username\n");
    }

    [RPC]
    void wrongPassword()
    {
        menu.addInfo("Wrong password\n");
    }

    [RPC]
    void loginSuccess()
    {
        menu.addInfo("Log in succesfull\n");
    }

    // gameplay RPCs
    [RPC]
    void noCard()
    {
        menu.addInfo("No card selected \n");
    }

    [RPC]
    void addCard(int attack, int defense, string gameObjectName)
    {
        card = GameObject.Find(gameObjectName);
        cardManager = card.GetComponent<CardManager>();
        cardManager.addCard(attack, defense);
    }
}


