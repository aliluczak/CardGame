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
    private GameObject heroRandomCard;
    private GameObject randomCard1;
    private GameObject randomCard2;
    private GameObject heroCard;
    private GameObject supportCard;
    private GameObject textInfoObject;
    private CardManager cardManager;
    private Menu menu;
    private TextController textController;
   

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
        textInfoObject = GameObject.Find("Info");
        textController = textInfoObject.GetComponent<TextController>();
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

    internal void sendCardAddedInfo(int number)
    {
        cardNetworkView.RPC("cardAdded", RPCMode.Server, number);
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
        cardNetworkView.RPC("moveCardRequest", RPCMode.Server, from, to);
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

    internal void sendMoveCardRequest(int from, int to)
    {
        cardNetworkView.RPC("moveCardRequest", RPCMode.Server, from, to);
    }

    //RPCs sent to server
    [RPC]
    void Register(string username, string password) { }

    [RPC]
    void Login(string username, string password) { }

    [RPC]
    void moveCardRequest(int from, int to) { }

    [RPC]
    void cardAdded(int number) {}
    


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
    void addCard(int cardID, string cardName, string cardType, int cardHP, int cardAttack, int cardPassive, string cardDescription, int cardHealing, int cardIntercept)
    {
        cardManager.addCard(cardID, cardName, cardType, cardHP, cardAttack, cardPassive, cardDescription, cardHealing, cardIntercept);
    }

    void cardMoved(int from, int to)
    {
        cardManager.setMovingPhaseInactive();
        textController.showTextMessage("Tura przeciwnika");
    }


    [RPC]
    void movingPhaseBegins()
    {
        cardManager.setMovingPhaseActive();
        textController.showTextMessage("Twoja tura");
    }

    [RPC]
    void drawingCards()
    {
        cardManager.setDrawingCard();
    }

    [RPC]
    void cardCannotBeMoved()
    {
        textController.showTextMessage("Błąd przesunięcia karty");
    }
}


