using UnityEngine;
using System.Collections;
using System;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    
    //variables needed for connection
    public static bool notCreated = true;

    private string info, connectionIP;
    private int connectionPort;
    
    private string username;
    private string password;
    private string passwordConfirmation;
    
    internal bool playerNotConnected;
    internal bool userNotRegistered;
    internal bool userNameNotExists;
    internal bool userLoggedIn;
    internal bool wrongPassword;

    private bool registrationKeyPressed;
    private bool logButtonPressed;
    private bool registrationRequestSent;
    private bool logRequestSent;

    private bool registrationCardRequestSent;
    private bool cardsReceived;
	//keyboard

    

    //variables for buttons
/*	private bool toggleMage = false;
	private bool toggleWarrior = true;
	private bool toggleTank = false;
	private bool toggleMage1 = true;
	private bool toggleWarrior1 = false;
	private bool toggleTank1 = false;
*/
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
    
    //return success inf

    // functions for menu management

    //loads all the gameobject and needed variables
    void Awake()
    {
        if (notCreated)
        {   
            DontDestroyOnLoad(this);
            notCreated = false;

        }
        else
        {
            DestroyImmediate(this.gameObject);
        }

        networkManager = GameObject.Find("NetworkManager");
        cardNetworkManager = networkManager.GetComponent<CardNetworkManager>();
        connectionIP = networkManager.GetComponent<CardNetworkManager>().connectionIP;
        connectionPort = networkManager.GetComponent<CardNetworkManager>().connectionPort;

     
        playerNotConnected = true;
        userNotRegistered = true;
        userNameNotExists = true;
        wrongPassword = false;
        userLoggedIn = false;

        registrationKeyPressed = false;
        logButtonPressed = false;

        registrationCardRequestSent = false;
        cardsReceived = false;

    }


    // functions for menu
    //save changes in Option Menu
    public void saveChanges()
    {
        PlayerPrefs.SetString("IP", serverIPText.text);
        PlayerPrefs.SetInt("Port", int.Parse(serverPortText.text));
    }

    //loads options from main menu
    public void showOptions()
    {
        SceneManager.LoadScene("Options");
    }

    //loads regiser and login scene
    public void playButton()
    {
        SceneManager.LoadScene("RegisterLogin");
    }

    //loads registering
    public void register()
    {
        SceneManager.LoadScene("Register");
    }

    public void login()
    {
        SceneManager.LoadScene("Login");
    }

    // TODO OLA all the functions below perhaps need a variable of keyboard to be added above
    //could try to set corresponded variables in these functions, otherwise create 3 new public void functions, 
    //that set these values; in that case keyboard variable needed
 
    public void typeNickname()
    {
//		Keyboard nick = new Keyboard(nickname);
	}

    public void typePassword()
    {
//		Keyboard pass = new Keyboard (passw);
    }

    public void typeConfirmationPassword()
    {
//		Keyboard conf = new Keyboard (passConfirmation);
    }

    public void typeIP()
    {
//		Keyboard ip = new Keyboard (serverIP);
    }

    public void typeConnectionPort()
    {
//		Keyboard port = new Keyboard (serverPort);
    }

    //returns password error info
    public void showPasswordError()
    {
        errorText = GameObject.Find("ErrorText").GetComponent<Text>();
        errorText.text = "Hasła się różnią";
    }


    //loads logging in
    public void finishLogging(string username, string password)
    {
        userNameNotExists = false;
        if (!logButtonPressed)
        {
            setLogButtonPressed();
            try
            {
                cardNetworkManager.connectToSerwer(PlayerPrefs.GetString("IP"), PlayerPrefs.GetInt("Port"));
                Debug.Log("Połączono z " + PlayerPrefs.GetString("IP") + ", " + PlayerPrefs.GetInt("Port").ToString());
            }
            catch
            {
                Debug.Log("Błąd połączenia");
            }
            Debug.Log("Połączono z " + PlayerPrefs.GetString("IP") + ", " + PlayerPrefs.GetInt("Port").ToString());
            StartCoroutine(LogUser(username, password));
        }
    }

    IEnumerator LogUser(string username, string password)
    {
        while (playerNotConnected)
        {
            yield return null;
        }

        if (!logRequestSent)
        {
            setLogRequestSent();
            cardNetworkManager.loginUser(username, password);
        }

        while (!userLoggedIn && !userNameNotExists && !wrongPassword)
        {
            yield return null;
        }

        if (userLoggedIn)
        {
            userLoggedIn = false;

            Application.LoadLevel(1);
        }
        else if (userNameNotExists)
        {
            userNameNotExists = false;
            Debug.Log("Username not exists");
        }
        else
        {
            wrongPassword = false;
            Debug.Log("Wrong password");
        }
    }

    //return from options menu to main menu
    public void backButton()
    {
        Application.LoadLevel("Menu");
    }

    //cancel changes in options menu
    public void cancel() 
    {
        serverPortText.text = PlayerPrefs.GetInt("Port").ToString();
        serverIPText.text = PlayerPrefs.GetString("IP");
    }

    //managing loading scenes
    void OnLevelWasLoaded(int level)
    {
        if (level == 2)
        {
            serverPortText = GameObject.Find("serverPortText").GetComponent<Text>();
            serverPortText.text = PlayerPrefs.GetInt("Port").ToString();

            serverIPText = GameObject.Find("serverIPText").GetComponent<Text>();
            serverIPText.text = PlayerPrefs.GetString("IP");
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
                username = nicknameText.text;
                password = passwordText.text;
                Application.LoadLevel("CardRegisteration");
        } 
    }

    public void cardRequest()
    {
        if (!registrationCardRequestSent)
        {
            cardNetworkManager.connectToSerwer(PlayerPrefs.GetString("IP"), PlayerPrefs.GetInt("Port"));
            StartCoroutine(waitForConnection());
            registrationCardRequestSent = true;
        }
    }

    IEnumerator waitForConnection()
    {
        while (playerNotConnected)
        {
            yield return null;
        }
        cardNetworkManager.sendRegistrationCardRequest();
        StartCoroutine(waitForCards());

    }

    IEnumerator waitForCards()
    {
        while (!cardsReceived)
        {
            yield return null;
        }

    }
    public void finishRegistration()
    {
        if (!registrationKeyPressed)
        {
            cardNetworkManager.connectToSerwer(PlayerPrefs.GetString("IP"), PlayerPrefs.GetInt("Port"));
            StartCoroutine(registerPlayer(username, password));
            setRegistrationButtonPressed();
        }
    }


    IEnumerator registerPlayer(string username, string password)
    {
        while (playerNotConnected)
        {
            yield return null;
        }
        Debug.Log("PlayerConnected");
        cardNetworkManager.registerUser(username, password);
        StartCoroutine(ifUserRegistered());
    }

    IEnumerator ifUserRegistered()
    {
        while(userNotRegistered && userNameNotExists) {
            yield return null;
        }

        if (!userNotRegistered)
        {
            Debug.Log("User registered");
            userNotRegistered = true;
            Application.LoadLevel(3);
        }
        else
        {
            Debug.Log("Username exists");
            userNameNotExists = true;
        }
    }

    public void setPlayerConnected()
    {
        playerNotConnected = false;
    }

    public void setPlayerDisconnected()
    {
        playerNotConnected = true;
    }

    private void setRegistrationButtonPressed()
    {
        registrationKeyPressed = true;
    }

    private void setRegistrationButtonInactive() 
    {
        registrationKeyPressed = false;
    }

    private void setRegistrationRequestSent()
    {
        registrationRequestSent = true;
    }

    private void setNoRegistrationRequest()
    {
        registrationRequestSent = false;
    }

    private void setLogRequestSent()
    {
        logRequestSent = true;
    }

    private void setNoLogRequest()
    {
        logRequestSent = false;
    }

    private void setLogButtonPressed()
    {
        logButtonPressed = true;
    }

    private void setLogButtonInactive()
    {
        logButtonPressed = false;
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
        }
    
    }*/
}