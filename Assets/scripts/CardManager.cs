using UnityEngine;
using System.Collections;

public class CardManager : MonoBehaviour {

    public enum CardType { HERO, SKILL, BOOSTER };
    public CardType cardType;
    public int attack;
    public int defense;
    public Sprite[] textures;
    public int cardIndex;
    private GameObject gameObject;
    private CardNetworkManager cardNetworkManager;
    private string infoCard ="";

	// Use this for initialization
	void Start () {
        
        gameObject = GameObject.Find("CardNetworkManager");
        cardNetworkManager = gameObject.GetComponent<CardNetworkManager>();
	}

    void OnGUI()
    {
        if(GUI.Button(new Rect(200, 0, 100, 50), "Add Card"))
        {
            cardNetworkManager.sendCardRequest(cardType.ToString(), name);
        }

        GUI.Box(new Rect(200, 100, 300, 200), infoCard);
    }
        

    public void addCard(int obtainedAttack, int obtainedDefense)
    {
        attack = obtainedAttack;
        defense = obtainedDefense;
        infoCard += "succeeded, attack: " + attack + " , defense " + defense + "\n"; 
    }
}
