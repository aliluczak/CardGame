using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class cardTextController : MonoBehaviour {

    Text textShown;
    // Use this for initialization
    void Start () {
        textShown = gameObject.GetComponent<Text>();
	}
	

    public void showNumberText(string text)
    {
        textShown.text = text;
        textShown.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    public void showDescriptionText(string text)
    {
        textShown.text = text;
        textShown.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);
    }

    public void hideText()
    {
        textShown.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    }
}
