using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal class Card : MonoBehaviour
{

    internal string cardName;
	internal enum CardType { HERO, SPELL};
    private int id;
    private int cardHP;
    private int cardAttack;
    private int cardPassive;
    private string cardDescription; 
    private int cardHealing;
    private int cardIntercept;
    internal CardType cardType;
    private int actualPosition;
    private int cardID;
    private string cardName1;
    private string cardType1;
    private int p;

    public Card(int cardID, string cardNameParameter, string type, int HP, int attack, int passive, string description, int healing, int intercept, int position)
    {
        this.id = cardID;
        this.cardName = cardNameParameter;
        switch (type)
        {
            case "Hero":
                {
                    this.cardType = CardType.HERO;
                    break;
                }
            case "Spell":
                {
                    this.cardType = CardType.SPELL;
                    break;
                }
        }

        this.cardHP = HP;
        this.cardPassive = passive;
        this.cardDescription = description;
        this.cardHealing = healing;
        this.cardIntercept = intercept;
        this.actualPosition = position;
    }


}
