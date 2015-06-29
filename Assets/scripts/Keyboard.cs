using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour {
	private TouchScreenKeyboard keyboard;

	private string input = "";
	private Text errorText;

	// Use this for initialization
	void Start () {
		if (TouchScreenKeyboard.isSupported) {
			//check if keyboard is activated
			if (TouchScreenKeyboard.visible) {
				//what portion of the screen the keyboard is using
				//TO DO condition
				//TouchScreenKeyboard.area;

				//opens a keyboard
				keyboard = TouchScreenKeyboard.Open (input, TouchScreenKeyboardType.Default, true, false, false, false, "");
			}
			errorText.text = "Touch Screen Keyboard is not supported";
		}
	}
	
	// Update is called once per frame
	void Update () {
		//TO DO: different types of keyboard (Numbers, Email, etc.)

		//shows what has been typed by the user
		if (keyboard != null && keyboard.done)
		{
			input = keyboard.text;
			print ("User input is: " + input);
		}
	}
}