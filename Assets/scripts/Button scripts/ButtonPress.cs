using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour {

	private string buttonName;
	private float buttonPressStart = -1.0f;
	private bool buttonDown = false;

	void OnButtonPress()
	{   
		Debug.Log("Button Press");     
	}

	IEnumerator lockPress()
	{
		buttonDown = true;
		yield return new WaitForSeconds(0.4f);
		buttonDown = false;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Returns true during the frame the user pressed down the virtual button identified by buttonName.
		//if (Input.GetButtonDown()) {
		//}

		//Returns true while the virtual button identified by buttonName is held down.
		if (Input.GetButton(buttonName)) {
			buttonDown = true;
			if (buttonPressStart > 0 && (Time.time - buttonPressStart) < 0.4)
			{
				this.OnButtonPress();
				buttonPressStart = -1;
				lockPress();
			}
			else
			{
				buttonPressStart = Time.time;
			}
		}
	
	}

}
