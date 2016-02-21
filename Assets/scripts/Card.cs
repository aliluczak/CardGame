using UnityEngine;
using System.Collections;
using System.Collections.Generic;

internal class Card : MonoBehaviour
{

    internal string cardName;
	internal enum CardType { HERO, SPELL};
    public int id;
    public int cardHP;
    public int cardAttack;
    public int cardPassive;
    public string cardDescription; 
    public int cardHealing;
    public int cardIntercept;
    internal CardType cardType;
    private int actualPosition;


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
