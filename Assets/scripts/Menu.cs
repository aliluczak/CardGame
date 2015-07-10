using UnityEngine;
using System.Collections;
using System;
using System.IO;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    
    //variables needed for connection
    private string info, connectionIP;
    private int connectionPort;
    
    private string username;
    private string password;
    private string passwordConfirmation;
	private string serverip;
	private string serverport;
    
	private bool registering = false; 
	//private logging, playing = false;
	//private bool options = false;

	//private bool toggleMage = false;
	//private bool toggleWarrior = true;
	//private bool toggleTank = false;
	//private bool toggleMage1 = true;
	//private bool toggleWarrior1 = false;
	//private bool toggleTank1 = false;

    //needed objects
	private GameObject gameObject;
    private GameObject networkManager;
    private CardNetworkManager cardNetworkManager;
    private CardManager cardManager;
    private Text serverPortText;
    private Text serverIPText;
    private Text errorText;
    private Text nicknameText;
    private Text passwordText;
    private Text confirmationpasswordText;
	//private Card card;

    //functions for menu and cardnetwork manager
    
    //return success info
    internal void addInfo(string addedInfo)
    {
        info += addedInfo;
    }

    //signal of end of registration
    internal void endOfRegisteration()
    {
        registering = false;
    }

    // functions for menu management

    //loads all the gameobject and needed variables
    void Awake()
    {
        networkManager = GameObject.Find("NetworkManager");
        cardNetworkManager = networkManager.GetComponent<CardNetworkManager>();
        connectionIP = networkManager.GetComponent<CardNetworkManager>().connectionIP;
        connectionPort = networkManager.GetComponent<CardNetworkManager>().connectionPort;

    }


    // functions for menu
    //save changes in Option Menu
    public void saveChanges()
    {
        cardNetworkManager.setConnectionIP(serverIPText.text);
        cardNetworkManager.setConnectionPort(int.Parse(serverPortText.text));
    }

    //loads options from main menu
    public void showOptions()
    {
        Application.LoadLevel("Options");
    }

    //loads regiser and login scene
    public void playButton()
    {
        Application.LoadLevel("RegisterLogin");
    }

    //loads registering
    public void register()
    {
        Application.LoadLevel("Register");
    }

    // TODO OLA all the functions below perhaps need a variable of keyboard to be added above
    //could try to set corresponded variables in these functions, otherwise create 3 new public void functions, 
    //that set these values; in that case keyboard variable needed
 
    public void typeNickname()
    {
        
    }

    public void typePassword()
    {

    }

    public void typeConfirmationPassword()
    {

    }

    public void typeIP()
    {

    }

    public void typeConnectionPort()
    {

    }

    //returns password error info
    public void showPasswordError()
    {
        errorText = GameObject.Find("ErrorText").GetComponent<Text>();
        errorText.text = "Hasła się różnią";
    }


    //loads loging in
    public void login()
    {
        
    }

    //return from options menu to main menu
    public void backButton()
    {
        Application.LoadLevel("Menu");
    }

    //cancel changes in options menu
    public void cancel() 
    {
        serverPortText.text = cardNetworkManager.getConnectionPort().ToString();
        serverIPText.text = cardNetworkManager.getConnectionIP();
  
    }

    //managing loading scenes
    void OnLevelWasLoaded(int level)
    {
        if (level == 2)
        {
            serverPortText = GameObject.Find("serverPortText").GetComponent<Text>();
            serverPortText.text = connectionPort.ToString();

            serverIPText = GameObject.Find("serverIPText").GetComponent<Text>();
            serverIPText.text = connectionIP;
        }

        if (level == 4)
        {
             nicknameText = GameObject.Find("NickNameText").GetComponent<Text>();
             passwordText= GameObject.Find("PasswordText").GetComponent<Text>();
             confirmationpasswordText = GameObject.Find("RepeatPasswordText").GetComponent<Text>();
        }
        //TODO request to server several random warrior cards and display their images, player picks chosen cards
        //cards can be saved in new variables (index for ex.)
        if (level == 5)
        {

        }
    }

    public void registrationStepTwo()
    {
        if (String.IsNullOrEmpty(nicknameText.text) || String.IsNullOrEmpty(passwordText.text) || String.IsNullOrEmpty(confirmationpasswordText.text))
        {
            errorText.text = "Nie wszystkie pola zostały wypełnione";
        }
        else
        {
            if (!confirmationpasswordText.text.Equals(passwordText.text))
                showPasswordError();
            else
                Application.LoadLevel("CardRegisteration");
        } 
    }


    //TODO split to functions (needed for UI buttons
  /*  void OnGUI (){
              
        GUILayout.Box(info);
              
        //if disconnected allows connection by typing chosen ip and port
        if (Network.peerType==NetworkPeerType.Disconnected)
            {
                GUILayout.Label("Connection IP:");
                connectionIP = GUILayout.TextField(connectionIP);
                GUILayout.Label("Port:");
                connectionPort = int.Parse(GUILayout.TextField(connectionPort.ToString()));
                
                if (GUILayout.Button("Connect"))
                    cardNetworkManager.connectToSerwer(connectionIP, connectionPort);
            }

        else if (Network.peerType == NetworkPeerType.Connecting)
        {
            addInfo("Connecting... \n");
        }

        else
            {

                if (GUILayout.Button("Sign in"))
                {
                    logging= true;
                    registering = false;
                }

                if (GUILayout.Button("Sign up")) 
                 {
                     registering = true;
                     logging = false;
                 }

                if (GUILayout.Button("Disconnect"))
                {
                    registering = false;
                    logging = false;
                    cardNetworkManager.disconnect();
                }

				if (GUILayout.Button("Choose hero"))
				{
					registering = false;
					logging = true;
					playing = true;
				}

                if (GUILayout.Button("Choose support"))
                {
                    registering = false;
                    logging = true;
                    playing = true;
                }

				if (GUILayout.Button("Play"))
				{
					registering = false;
					logging = true;
					playing = true;
				}
                
                if (registering)
                {
                    GUILayout.Label("Enter username:");
                    username = GUILayout.TextField(username);
                    GUILayout.Label("Enter password");
                    password = GUILayout.TextField(password);
                    GUILayout.Label("Confirm password");
                    passwordConfirmation = GUILayout.TextField(passwordConfirmation);

                    if (GUILayout.Button("Finish registration"))
                    {
                        if (password.Equals(passwordConfirmation))
                        {
                            cardNetworkManager.registerUser(username, password);
                            username = "";
                            password = "";
                            passwordConfirmation = "";
                        }
                        else
                        {
                            addInfo("Password and confirmations are different. \n");
                            username = "";
                            password = "";
                            passwordConfirmation = "";
                        }
                    }
                 }
                
                if (logging) {
                    GUILayout.Label("Enter username:");
                    username = GUILayout.TextField(username);
                    GUILayout.Label("Enter password");
                    password = GUILayout.TextField(password);

                    if (GUILayout.Button("Sign in"))
                    {
                            cardNetworkManager.loginUser(username, password);
                            username = "";
                            password = "";
                            cardNetworkManager.keepRunningConnection();
                    }
                }

				if (playing) {
					GUILayout.Label("Welcome to the Card Game!");

					if (GUILayout.Button("Choose hero")) {
						toggleMage = GUI.Toggle (new Rect (25, 25, 100, 30), toggleMage, "Mage");
						toggleTank = GUI.Toggle( new Rect (25, 25, 100, 30), toggleTank, "Tank");
						toggleWarrior = GUI.Toggle( new Rect (25, 25, 100, 30), toggleWarrior, "Warrior");
						if (toggleMage == true) {}
						if (toggleTank == true) {}
						if (toggleWarrior == true) {}
					}
					if (GUILayout.Button("Choose support")) {
						toggleMage1 = GUI.Toggle (new Rect (25, 25, 100, 30), toggleMage1, "Support Mage");
						toggleTank1 = GUI.Toggle( new Rect (25, 25, 100, 30), toggleTank1, "Support Tank");
						toggleWarrior1 = GUI.Toggle( new Rect (25, 25, 100, 30), toggleWarrior1, "Support Warrior");
						if (toggleMage1 == true) {}
						if (toggleTank1 == true) {}
						if (toggleWarrior1 == true) {}
					}
					if (GUILayout.Button("Play")) {

					}
				}

				if (options)
				{
					showOptions();
					GUILayout.Label("IP Serwera:");
					serverip = GUILayout.TextField(serverip);
					GUILayout.Label("Port Serwera");
					serverport = GUILayout.TextField(serverport);
				}
        }
    
    }*/
}