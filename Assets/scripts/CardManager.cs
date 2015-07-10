using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardManager : MonoBehaviour {

    private static CardManager instance;
	//public string name;
	public enum CardType { HERO, SPELL };
    public CardType cardType;
	public enum CardSubType {WARRIOR, TANK, MAGE};
	public CardSubType cardSubType;
    public int attack;
    public int defense;

    public List<Sprite> charactersList;
    private GameObject gameObject;
    private CardNetworkManager cardNetworkManager;
    private string infoCard ="";

    private GameObject heroCard;
    private GameObject supportCard;
    private GameObject random1Card;
    private GameObject random2Card;
    private GameObject random3Card;
    private GameObject enemyHeroCard;
    private GameObject enemySupportCard;

    private List<Image> board;

    private enum CardField { HERO, SUPPORT, RANDOM1, RANDOM2, RANDOM3 };


	// Use this for initialization
    void Start()
    {
        if (!instance)
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
        else
        {
            DestroyImmediate(this.gameObject);
        }
        
    }

    void OnLevelWasLoaded(int level)
    {
        if (level.Equals(1))
        {
            charactersList = new List<Sprite>();
            board = new List<Image>();

            heroCard = GameObject.Find("Hero");
            board.Add(heroCard.GetComponent<Image>());

            supportCard = GameObject.Find("Support");
            board.Add(supportCard.GetComponent<Image>());

            random1Card = GameObject.Find("RandomHero");
            board.Add(random1Card.GetComponent<Image>());

            random2Card = GameObject.Find("RandomCard1");
            board.Add(random2Card.GetComponent<Image>());

            random3Card = GameObject.Find("RandomCard2");
            board.Add(random3Card.GetComponent<Image>());

            enemyHeroCard = GameObject.Find("EnemyHero");
            board.Add(enemyHeroCard.GetComponent<Image>());

            enemySupportCard = GameObject.Find("EnemySupport");
            board.Add(enemySupportCard.GetComponent<Image>());
        }

        if (level == 5)
        {
            gameObject = GameObject.Find("NetworkManager");
            cardNetworkManager = gameObject.GetComponent<CardNetworkManager>();
        }
    }

    private void showImage(int image, Image card)
    {
        card.sprite = charactersList[image];
        Color c = card.color;
        c.a = 255;
        card.color = c;
    }

    private void showImage(Image image, Image card)
    {
        card.sprite = image.sprite;
        Color c = card.color;
        c.a = 255;
        card.color = c;
    }

    private void hideImage(Image card)
    {
        Color c = card.color;
        c.a = 0;
        card.color = c;
        card.sprite = null;
    }

    public void tryMoveCard(int from, int to)
    {
        bool move = true;
        if (board[from].sprite = null)
        {
            move = false;
        }

        if (board[to].sprite != null)
        {
            move = false;
        }

        if (move)
            cardNetworkManager.moveCard(from, to);
    }

    public void moveCard(int from, int to)
    {
        showImage(board[from], board[to]);
        hideImage(board[from]);
    }
    
    public void addCard(int obtainedAttack, int obtainedDefense)
    {
        attack = obtainedAttack;
        defense = obtainedDefense;
        infoCard += "succeeded, attack: " + attack + " , defense " + defense + "\n"; 
    }
    /*
    public string getFiledInfo(Image field)
    {
        if ()
    }
    */
}
