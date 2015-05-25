using UnityEngine;
using System.Collections;

public class CardManager : MonoBehaviour {

    public enum CardType { HERO, BOOSTER, SKILL };
    public CardType cardType;
	public enum CardSubType {MAGE, TANK, WARRIOR, COMMON};
	public CardSubType cardSubType;
    public int attack;
    public int defense;
    public Sprite[] textures;
    public int cardIndex;
    private GameObject gameObject;
    private CardNetworkManager cardNetworkManager;
    private string infoCard ="";

	// Use this for initialization
    void Start()
    {

        gameObject = GameObject.Find("CardNetworkManager");
        cardNetworkManager = gameObject.GetComponent<CardNetworkManager>();
    }
        

    public void addCard(int obtainedAttack, int obtainedDefense)
    {
        attack = obtainedAttack;
        defense = obtainedDefense;
        infoCard += "succeeded, attack: " + attack + " , defense " + defense + "\n"; 
    }

    void OnMouseDown()
    {
        

    }
}
