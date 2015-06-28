using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardManager : MonoBehaviour {

	//public string name;
	public enum CardType { HERO, BOOSTER, SKILL };
    public CardType cardType;
	public enum CardSubType {MAGE, TANK, WARRIOR, COMMON};
	public CardSubType cardSubType;
    public int attack;
    public int defense;
    public Dictionary<int, Image> charactersList;
    private GameObject gameObject;
    private CardNetworkManager cardNetworkManager;
    private string infoCard ="";

	// Use this for initialization
    void Start()
    {

        gameObject = GameObject.Find("CardNetworkManager");
        cardNetworkManager = gameObject.GetComponent<CardNetworkManager>();

        fillCharactersList();
    }

    private void fillCharactersList()
    {

    }
    public void addCard(int obtainedAttack, int obtainedDefense)
    {
        attack = obtainedAttack;
        defense = obtainedDefense;
        infoCard += "succeeded, attack: " + attack + " , defense " + defense + "\n"; 
    }

}
