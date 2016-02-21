using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;


public class MyMSGTypes
{
    // msgs sent from server
    public static short SendCard = 1001;
    public static short NoCard = 1002;
    public static short CardRequest = 1003;
    public static short Win = 1004;
    public static short Lose = 1005;
    public static short UserRegistered = 1005;
    public static short UsernameExists = 1006;
    public static short UserNotFound = 1007;
    public static short WrongPassword = 1008;
    public static short LoginSuccess = 1009;
    public static short CardMoved = 1010;
    public static short CardCannotBeMoved = 1011;
    public static short MovingPhaseBegins = 1012;
    public static short DrawingCards = 1013;
    public static short WaitForAnotherPlayer = 1014;
    public static short Tie = 1022;
    public static short OpponentName = 1024;

    // msgs sent from client
    public static short Register = 1016;
    public static short MoveCardRequest = 1017;
    public static short Login = 1018;
    public static short CardAdded = 1019;
    public static short Magic = 1020;
    public static short endMovePhase = 1021;
    public static short gameLoaded = 1023;

}

public class CardNetworkManager : MonoBehaviour
{
    private static CardNetworkManager instance;
    //connection variables
    internal string connectionIP = "127.0.0.1";
    internal int connectionPort = 8000;
    public NetworkClient client;

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
    public string playerName;
   

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
    }



    void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            textController = GameObject.Find("TextController").GetComponent<TextController>();
        }
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
        CardNumberMessage msg = new CardNumberMessage();
        msg.cardNumber = number;
        client.Send(MyMSGTypes.CardAdded, msg);
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
            client = new NetworkClient();
            registerHandlers();
            client.Connect(connectionIP, connectionPort);

        }
        catch (Exception)
        {
            Debug.Log("Check IP and port"); 
        }
    }

    private void registerHandlers()
    {
        client.RegisterHandler(MsgType.Connect, OnConnected);
        client.RegisterHandler(MyMSGTypes.SendCard, addCard);
        client.RegisterHandler(MyMSGTypes.NoCard, noCard);
        //      client.RegisterHandler(MyMSGTypes.Win, Win);
        //     client.RegisterHandler(MyMSGTypes.Lose, Lose);
        client.RegisterHandler(MyMSGTypes.UserRegistered, userRegistered);
        client.RegisterHandler(MyMSGTypes.UsernameExists, usernameExists);
        client.RegisterHandler(MyMSGTypes.UserNotFound, userNotFound);
        client.RegisterHandler(MyMSGTypes.WrongPassword, wrongPassword);
        client.RegisterHandler(MyMSGTypes.LoginSuccess, loginSuccess);
        client.RegisterHandler(MyMSGTypes.CardMoved, cardMoved);
        client.RegisterHandler(MyMSGTypes.CardCannotBeMoved, cardCannotBeMoved);
        client.RegisterHandler(MyMSGTypes.MovingPhaseBegins, movingPhaseBegins);
        client.RegisterHandler(MyMSGTypes.DrawingCards, drawingCards);
        client.RegisterHandler(MyMSGTypes.WaitForAnotherPlayer, waitForAnotherPlayer);
        client.RegisterHandler(MyMSGTypes.OpponentName, setOpponentName);
    }
    
  //  public static short Tie = 1022;

    // disconnecting form server
    internal void disconnect()
    {
        client.Disconnect();
    }

    //register user
    internal void registerUser(string username, string password)
    {
        RegisterLoginMessage msg = new RegisterLoginMessage();
        msg.login = username;
        msg.password = password;
        client.Send(MyMSGTypes.Register, msg);
    }

    // login user
    internal void loginUser(string username, string password)
    {
        RegisterLoginMessage msg = new RegisterLoginMessage();
        msg.login = username;
        msg.password = password;
        client.Send(MyMSGTypes.Login, msg);
        playerName = username;
    }

    public void OnConnected(NetworkMessage msg)
    {
        menu.playerNotConnected = false;
    }

    void OnDisonnectedFromServer()
    {
        Debug.Log("Disconnected from server");
    }
   
    internal void sendMoveCardRequest(int from, int to)
    {
        MoveMessage msg = new MoveMessage();
        msg.from = from;
        msg.to = to;
        client.Send(MyMSGTypes.MoveCardRequest, msg);
    }
 
    internal void sendRegistrationCardRequest()
    {
        var msg = new IntegerMessage();
        client.Send(MyMSGTypes.CardRequest, msg);
    }

    internal void sendGameLoadedInfo()
    {
        var msg = new IntegerMessage();
        client.Send(MyMSGTypes.gameLoaded, msg);
    }


    // RPCs received from server

    //RPC to connect, register and login


    void userRegistered(NetworkMessage msg)
    {
        menu.userNotRegistered = false;
    }


    void usernameExists(NetworkMessage msg)
    {
        menu.userNameNotExists = false;
    }


    void userNotFound(NetworkMessage msg)
    {
        menu.userNameNotExists = true;
    }


    void wrongPassword(NetworkMessage msg)
    {
        menu.wrongPassword = true;
    }


    void loginSuccess(NetworkMessage msg)
    {
        menu.userLoggedIn = true;
    }

    // gameplay RPCs

    void noCard(NetworkMessage msg)
    {
        textController.showTextMessage("Nie wybrano karty");
    }


    void addCard(NetworkMessage msg)
    {
        var reader = msg.ReadMessage<CardMessage>();

        cardManager.addCard(reader.cardID, reader.cardName, reader.cardType, reader.cardHP, reader.cardAttack, reader.cardPassive, reader.cardDescription, reader.cardHealing, reader.cardIntercept);
    }

    void cardMoved(NetworkMessage msg)
    {
        cardManager.setMovingPhaseInactive();
        textController.showTextMessage("Tura przeciwnika");
    }



    void movingPhaseBegins(NetworkMessage msg)
    {
        cardManager.setMovingPhaseActive();
        textController.showTextMessage("Twoja tura");
    }


    void drawingCards(NetworkMessage msg)
    {
        cardManager.setDrawingCard();
    }

    void cardCannotBeMoved(NetworkMessage msg)
    {
        textController.showTextMessage("Błąd przesunięcia karty");
    }

    void waitForAnotherPlayer(NetworkMessage msg)
    {
        textController.showTextMessage("Oczekiwanie na drugiego gracza");
    }

    void setOpponentName(NetworkMessage msg)
    {
        StringMessage name = msg.ReadMessage<StringMessage>();
        cardManager.enemyName.text = name.value;
        cardManager.enemyHP.text = "10";
    }


}


