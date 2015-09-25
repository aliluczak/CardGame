using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoginButton : MonoBehaviour {

    [SerializeField]
    private Button login, cancel = null;
    private Menu menu;
    private GameObject inputs;
    private Text username;
    private Text password;
	// Use this for initialization
	void Start () {
        menu = GameObject.Find("Menu").GetComponent<Menu>();
        inputs = GameObject.Find("Textuser");
        username = inputs.GetComponent<Text>();
        inputs = GameObject.Find("Textpassword");
        password = inputs.GetComponent<Text>();

        login.onClick.AddListener(()=> menu.finishLogging(username.text, password.text));
        cancel.onClick.AddListener(() => menu.backButton());
	}
}
