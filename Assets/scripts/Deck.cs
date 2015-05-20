using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Deck : MonoBehaviour {

    GameObject cardManagerObject;
    GameObject cardObject;
    CardManager cardManager;
    Card card;

   internal List<Card> deck;


	// Use this for initialization
	void Start () {
        cardManagerObject = GameObject.Find("CardManager");
        cardManager = cardManagerObject.GetComponent<CardManager>();
        cardObject = GameObject.Find("Card");
        card = cardObject.GetComponent<Card>();
	}
	
}
