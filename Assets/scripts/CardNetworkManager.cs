using UnityEngine;
using System.Collections;
using System;

public class CardNetworkManager : MonoBehaviour
{
    private static CardNetworkManager instance;
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
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this.gameObject);

        //loading NetworkView
        cardNetworkView = GetComponent<NetworkView>();

        //loading Menu
        menuObject = GameObject.Find("Menu");
        menu = menuObject.GetComponent<Menu>();
        card = GameObject.Find("CardManager");
        cardManager = card.GetComponent<CardManager>();

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

    //get and set fo connection port
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
            Debug.Log("Check IP and port"); 
        }
    }

    internal void moveCard(int from, int to)
    {
        cardNetworkView.RPC("moveCard", RPCMode.Server, from, to);
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

    void OnConnectedToServer()
    {
        menu.playerNotConnected = false;
    }

    void OnDisonnectedFromServer()
    {
        Debug.Log("Disconnected from server");
    }

    //RPCs sent to server
    [RPC]
    void Register(string username, string password) { }

    [RPC]
    void Login(string username, string password) { }

    [RPC]
    void cardRequest(string cardType, string gameObjectName) { }

    [RPC]
    void moveCard() { }
    


    // RPCs received from server

    //RPC to connect, register and login

    [RPC]
    void userRegistered()
    {
        menu.userNotRegistered = false;
    }

    [RPC]
    void usernameExists()
    {
        menu.userNameNotExists = false;
    }

    [RPC]
    void userNotFound()
    {
        menu.userNameNotExists = true;
    }

    [RPC]
    void wrongPassword()
    {
        menu.wrongPassword = true;
    }

    [RPC]
    void loginSuccess()
    {
        menu.userLoggedIn = true;
    }

    // gameplay RPCs
    [RPC]
    void noCard()
    {
        Debug.Log("No card sellected");
    }

    [RPC]
    void addCard(int attack, int defense, string gameObjectName)
    {
        card = GameObject.Find(gameObjectName);
        cardManager = card.GetComponent<CardManager>();
        cardManager.addCard(attack, defense);
    }

    [RPC]
    void cardMoved(int from, int to)
    {
        cardManager.moveCard(from, to);
    }

    [RPC]
    void cardCannotBeMoved()
    {

    }

    [RPC]
    void movingPhaseBegins()
    {
        cardManager.setMovingPhaseActive();
    }
}


