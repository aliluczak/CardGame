using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextController : MonoBehaviour {

    Text textShown;

    void Start()
    {
        textShown = GetComponent<Text>();
    }

    public void showTextMessage(string message)
    {
        textShown.text = message;
        StartCoroutine(showingMessage());
    }

    IEnumerator showingMessage()
    {
        yield return new WaitForSeconds(1.5f);
        textShown.text = ""; 
    }

    public void setText(string info)
    {
        textShown.text = info;
    }
}
