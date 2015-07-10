using UnityEngine;
using System.Collections;

public class ControllManager : MonoBehaviour {

	private string information;
	private bool guiOn = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnMouseEnter(){}
	void OnMouseExit(){}

	void OnMouseDown(){
		//Fires off when the mouse is clicked while hovering over the object
		//Aim: Show card stats

		guiOn = true; // enable gui and define position at point clicked

		guiOn = false;
	}

    void dragObject()
    {

    }

    //
    void holdButtonDown()
    {

    }
}
