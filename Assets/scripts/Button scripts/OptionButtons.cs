using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionButtons : MonoBehaviour {

    [SerializeField]
    private Button saveChanges, back, cancel = null;
    private Menu menu;
	// Use this for initialization
	void Start () {
        menu = GameObject.Find("Menu").GetComponent<Menu>();
        saveChanges.onClick.AddListener(() => menu.saveChanges());
        back.onClick.AddListener(() => menu.backButton());
        cancel.onClick.AddListener(() => menu.cancel());
	}
	
}
