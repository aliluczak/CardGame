using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour {
	//different keyboards
	private TouchScreenKeyboard keyboard;
	TouchScreenKeyboard nameKeyboard;
	TouchScreenKeyboard emailKeyboard;
	TouchScreenKeyboard numberKeyboard;

	//logical variables
	private bool keyboardOpen = false;
	private bool nameKeyboardOpen = false;
	private bool emailKeyboardOpen = false;
	private bool numberKeyboardOpen = false;

	//input variables
	private string input = "";
	private string nameText = "";
	private string emailText = "";
	private string numberText = "";
	private Text errorText;

	// Use this for initialization
	void Start () {
		if (TouchScreenKeyboard.isSupported) {
			//check if keyboard is activated
			if (TouchScreenKeyboard.visible) {
				//what portion of the screen the keyboard is using
				//TO DO condition
				//TouchScreenKeyboard.area;

				//TO DO choose a keyboard to open
				///opens a keyboard
				//default keyboard
				keyboard = TouchScreenKeyboard.Open (input, TouchScreenKeyboardType.Default, true, false, false, false, "");
				keyboardOpen = true;
				//name keyboard
				nameKeyboard = TouchScreenKeyboard.Open(nameText, TouchScreenKeyboardType.NamePhonePad, false, false, false, false,"Your name (required)");
				nameKeyboardOpen = true;
				//email keyboard
				emailKeyboard = TouchScreenKeyboard.Open(emailText, TouchScreenKeyboardType.EmailAddress, false, false, false, false,"Your email (required)"); 
				emailKeyboardOpen = true;
				//numeric keyboard
				numberKeyboard = TouchScreenKeyboard.Open (numberText, TouchScreenKeyboardType.NumbersAndPunctuation, false, false, false, false, "");
				numberKeyboardOpen = true;
			}
			errorText.text = "Touch Screen Keyboard is not supported";
		}
	}
	
	// Update is called once per frame
	void Update () {
		/// Hides the keyboard if the device is facing down & resumes input if the device is facing up.
		// default keyboard
		if (keyboard != null)
		{
			if (Input.deviceOrientation == DeviceOrientation.FaceDown)
				keyboard.active = false;
			if (Input.deviceOrientation == DeviceOrientation.FaceUp)
				keyboard.active = true;
		}
		//name keyboard
		if (nameKeyboard != null)
		{
			if (Input.deviceOrientation == DeviceOrientation.FaceDown)
				nameKeyboard.active = false;
			if (Input.deviceOrientation == DeviceOrientation.FaceUp)
				nameKeyboard.active = true;
		}
		//email keyboard
		if (emailKeyboard != null)
		{
			if (Input.deviceOrientation == DeviceOrientation.FaceDown)
				emailKeyboard.active = false;
			if (Input.deviceOrientation == DeviceOrientation.FaceUp)
				emailKeyboard.active = true;
		}
		//numeric keyboard
		if (numberKeyboard != null)
		{
			if (Input.deviceOrientation == DeviceOrientation.FaceDown)
				numberKeyboard.active = false;
			if (Input.deviceOrientation == DeviceOrientation.FaceUp)
				numberKeyboard.active = true;
		}

		///shows what has been typed by the user
		//default keyboard
		if (keyboard != null && keyboard.done)
		{
			input = keyboard.text;
			print ("User input is: " + input);
			keyboardOpen = false;
		}
		//name keyboard
		if (nameKeyboard != null && nameKeyboard.done)
		{
			nameText = nameKeyboard.text;
			print ("User name is: " + nameText);
			nameKeyboardOpen = false;
		}
		//email keyboard
		if (emailKeyboard != null && emailKeyboard.done)
		{
			emailText = emailKeyboard.text;
			print ("User email is: " + emailText);
			emailKeyboardOpen = false;
		}
		//numeric keyboard
		if (numberKeyboard != null && numberKeyboard.done)
		{
			numberText = numberKeyboard.text;
			print ("Numbers: " + numberText);
			numberKeyboardOpen = false;
		}
	}
}