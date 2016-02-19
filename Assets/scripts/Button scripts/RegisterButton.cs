using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RegisterButton : MonoBehaviour {

    [SerializeField]
    private Button confirm, back = null;
    private Menu menu;
    private GameObject inputs;
    private Text nickname, password, confirmPassword;


	// Use this for initialization
	void Start () {
        menu = GameObject.Find("Menu").GetComponent<Menu>();
        inputs = GameObject.Find("NickNameText");
        nickname = inputs.GetComponent<Text>();
        inputs = GameObject.Find("PasswordText");
        password = inputs.GetComponent<Text>();
        inputs = GameObject.Find("RepeatPasswordText");
        confirmPassword = inputs.GetComponent<Text>();

        confirm.onClick.AddListener(() => menu.registrationStepTwo());
        back.onClick.AddListener(() => menu.backButton());
	}
	
	
}
