using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Click : MonoBehaviour {
    [SerializeField] private Button finishRegistration, cancel = null;
    private Menu menu;
	// Use this for initialization
	void Start () {
        menu = GameObject.Find("Menu").GetComponent<Menu>();
        finishRegistration.onClick.AddListener(() => menu.finishRegistration());
        cancel.onClick.AddListener(() => menu.backButton());
	}
}
