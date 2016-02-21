using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class CardManager : MonoBehaviour {

    private static CardManager instance;
	//public string name;

    
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
    private GameObject endMovingfButton;

    private GameObject infoTextObject;

    private spriteController random1Controller;
    private spriteController random2Controller;
    private spriteController random3Controller;
    private spriteController heroController;
    private spriteController supportController;
    private spriteController enemyHeroController;
    private spriteController enemySupportController;
    private TextController textController;
    public Text playerName;
    public Text playerHP;
    public Text enemyName;
    public Text enemyHP;

    private CardImageController backgroundImage;
    private CardImageController cardImage;
    private cardTextController cardAttack;
    private cardTextController cardHP;
    private cardTextController cardDescription;
    private cardTextController cardName;
    private cardTextController cardType;

    private List<string> supportDescrriptionInfo;
    private List<string> magicDescriptionInfo;

    private List<Card> board;
    private List<bool> cardOnBoard;

    private enum CardField { HERO, SUPPORT, RANDOM1, RANDOM2, RANDOM3 };

    private bool movingPhase;
    private int cardSelected;
    private bool drawingCard1;
    private bool drawingCard2;
    private bool drawingCard3;

    private int movingCardFrom;
    private int movingCardTo;


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

        supportDescrriptionInfo = new List<string>();
        magicDescriptionInfo = new List<string>();
        
    }

    void OnLevelWasLoaded(int level)
    {
        if (level.Equals(1))
        {
            setCardDeselected();
            charactersList = new List<Sprite>();
            board = new List<Card>();

            heroCard = GameObject.Find("Hero");
            board.Add(heroCard.GetComponent<Card>());
            heroController = heroCard.GetComponent<spriteController>();

            supportCard = GameObject.Find("Support");
            board.Add(supportCard.GetComponent<Card>());
            supportController = supportController.GetComponent<spriteController>();

            random1Card = GameObject.Find("RandomHero");
            board.Add(random1Card.GetComponent<Card>());
            random1Controller = random1Card.GetComponent<spriteController>();

            random2Card = GameObject.Find("RandomCard1");
            board.Add(random2Card.GetComponent<Card>());
            random2Controller = random2Card.GetComponent<spriteController>();

            random3Card = GameObject.Find("RandomCard2");
            board.Add(random3Card.GetComponent<Card>());
            random3Controller = random3Card.GetComponent<spriteController>();

            enemyHeroCard = GameObject.Find("EnemyHero");
            board.Add(enemyHeroCard.GetComponent<Card>());
            enemyHeroController = enemyHeroCard.GetComponent<spriteController>();

            enemySupportCard = GameObject.Find("EnemySupport");
            board.Add(enemySupportCard.GetComponent<Card>());
            enemySupportController = enemySupportCard.GetComponent<spriteController>();

            playerName = GameObject.Find("PlayerName").GetComponent<Text>();
            playerHP = GameObject.Find("PlayerHP").GetComponent<Text>();
            enemyName = GameObject.Find("OpponentName").GetComponent<Text>();
            enemyHP = GameObject.Find("OpponentHP").GetComponent<Text>();

            backgroundImage = GameObject.Find("CardDetail").GetComponent<CardImageController>();
            cardImage = GameObject.Find("CarddetailImage").GetComponent<CardImageController>();
            cardAttack = GameObject.Find("CardAttack").GetComponent<cardTextController>();
            cardHP = GameObject.Find("CardHP").GetComponent<cardTextController>();
            cardDescription = GameObject.Find("Description").GetComponent<cardTextController>();
            cardName = GameObject.Find("CardName").GetComponent<cardTextController>();
            cardType = GameObject.Find("Type").GetComponent<cardTextController>();

            supportDescrriptionInfo.Add("Wzmacnia atak o 1");
            supportDescrriptionInfo.Add("Wzmacnia atak o 2");
            supportDescrriptionInfo.Add("Leczy 1 HP");
            supportDescrriptionInfo.Add("Leczy 2 HP");
            supportDescrriptionInfo.Add("Przechwytuje 1 punkt ataku przeciwnika");
            supportDescrriptionInfo.Add("Przechwytuje 2 punkty ataku przeciwnika");

            magicDescriptionInfo.Add("Zadaje 2  punkty obrażeń");
            magicDescriptionInfo.Add("Zadaje 2  punkty obrażeń");
            magicDescriptionInfo.Add("Leczy 1 punkt życia");
            magicDescriptionInfo.Add("Leczy 2 punkty życia");
            magicDescriptionInfo.Add("Blokuje 1 punkt ataku");
            magicDescriptionInfo.Add("Blokuje 2 punkty ataku");
            magicDescriptionInfo.Add("Blokuje 1 punkt ataku");
            magicDescriptionInfo.Add("Blokuje 1 punkt ataku");
            magicDescriptionInfo.Add("Zadaje 1  punkt obrażeń");
            magicDescriptionInfo.Add("Zadaje 1  punkt obrażeń");
            magicDescriptionInfo.Add("Zadaje 2  punkty obrażeń");
            magicDescriptionInfo.Add("Zadaje 2  punkty obrażeń");



            setMovingPhaseInactive();
            infoTextObject = GameObject.Find("Info");
            textController = infoTextObject.GetComponent<TextController>();

            playerName.text = cardNetworkManager.playerName;
            playerHP.text = "10";

            for (int i = 0; i < 7; i++)
            {
                cardOnBoard.Add(false);
            }
            cardNetworkManager.sendGameLoadedInfo();
        }

        if (level == 5)
        {
            gameObject = GameObject.Find("NetworkManager");
            cardNetworkManager = gameObject.GetComponent<CardNetworkManager>();
        }
    }

    public void tryMoveCard(int from, int to)
    {
        bool possible = true;
        if (from == -1 || to == -1)
        {
            possible = false;
        }
        else
        {

            if (!cardOnBoard[from])
            {
                textController.showTextMessage("Nie wybrano karty");
                possible = false;
            }

            if (cardOnBoard[to])
            {
                textController.showTextMessage("Na danym polu już jest karta");
                possible = false;
            }
        }

        if (possible)
        {
            switch (from)
            {
                case 2:
                    {
                        random1Controller.possibleHideImage();
                        break;
                    }
                case 3:
                    {
                        random2Controller.possibleHideImage();
                        break;
                    }
                case 4:
                    {
                        random3Controller.possibleHideImage();
                        break;
                    }
            }

            switch (to)
            {
                case 0:
                    {
                        heroController.showImage(charactersList[from]);
                        break;
                    }
                case 1:
                    {
                        supportController.showImage(charactersList[from]);
                        break;
                    }
            }

            movingCardFrom = from;
            movingCardTo = to;
        }

    }

    internal void cancelMove(int from, int to)
    {
        switch (from)
        {
            case 2:
                {
                    random1Controller.showImage();
                    break;
                }
            case 3:
                {
                    random2Controller.showImage();
                    break;
                }
            case 4:
                {
                    random3Controller.showImage();
                    break;
                }
        }

        switch (to)
        {
            case 0:
                {
                    heroController.hideImage();
                    break;
                }
            case 1:
                {
                    supportController.hideImage();
                    break;
                }
        }

        from = -1;
        to = -1;
    }

    public void showCardDetail(int position)
    {
        switch (board[position].cardType)
        {
            case Card.CardType.HERO:
                {
                    backgroundImage.showImage(charactersList[48]);
                    cardImage.showImage(charactersList[board[position].id]);
                    cardAttack.showNumberText(board[position].cardAttack.ToString());
                    cardHP.showNumberText(board[position].cardHP.ToString());
                    cardDescription.showDescriptionText(supportDescrriptionInfo[board[position].cardPassive]);
                    cardName.showDescriptionText(board[position].cardName);
                    cardType.showDescriptionText(board[position].cardType.ToString());
                    break;
                }

            case Card.CardType.SPELL:
                {
                    backgroundImage.showImage(charactersList[49]);
                    cardImage.showImage(charactersList[board[position].id]);
                    cardAttack.hideText();
                    cardHP.hideText();
                    cardDescription.showDescriptionText(magicDescriptionInfo[board[position].id]);
                    cardName.showDescriptionText(board[position].cardName);
                    cardType.showDescriptionText(board[position].cardType.ToString());
                    break;
                }
        }

    }

    internal int getMovingCardFrom()
    {
        return movingCardFrom;
    }

    internal int getMovingCardTo()
    {
        return movingCardTo;
    }

    public void setMovingPhaseActive()
    {
        movingPhase = true;

    }

    public void setCardSelected(int position)
    {
        cardSelected = position;
    }

    public void setCardDeselected()
    {
        cardSelected = -1;
    }

    public int whichCardSelected()
    {
        return cardSelected;
    }

    public bool movingPhaseIsActive()
    {
        return movingPhase;
    }

    public void setMovingPhaseInactive()
    {
        movingPhase = false;
    }

    public bool cardOnPositionExists(int position)
    {
        return cardOnBoard[position];
    }

    public void highlightSprites()
    {
        heroController.highlightImage();
        supportController.highlightImage();
    }

    public void hideCardDetail()
    {
        backgroundImage.hideImage();
        cardImage.hideImage();
        cardAttack.hideText();
        cardHP.hideText();
        cardDescription.hideText();
        cardName.hideText();
        cardType.hideText();
    }

    public void addCard(int cardID, string cardName, string cardType, int cardHP, int cardAttack, int cardPassive, string cardDescription, int cardHealing, int cardIntercept)
    {
        if (!drawingCard1)
        {
            board.Add(new Card(cardID, cardName, cardType, cardHP, cardAttack, cardPassive, cardDescription, cardHealing, cardIntercept, 2));
            cardOnBoard[2] = true;
            random1Controller.showImage(charactersList[cardID]);
            setDrawingCard(1);
        }
        else if (!drawingCard2)
        {
            board.Add(new Card(cardID, cardName, cardType, cardHP, cardAttack, cardPassive, cardDescription, cardHealing, cardIntercept, 3));
            random2Controller.showImage(charactersList[cardID]);
            cardOnBoard[3] = true;
            setDrawingCard(2);
        }
        else if (drawingCard3)
        {
            board.Add(new Card(cardID, cardName, cardType, cardHP, cardAttack, cardPassive, cardDescription, cardHealing, cardIntercept, 4));
            random3Controller.showImage(charactersList[cardID]);
            cardOnBoard[4] = true;
            setDrawingCard(3);
        }
    }


    public void setDrawingCard()
    {
        drawingCard1 = false;
        drawingCard2 = false;
        drawingCard3 = false;
    }

    public void setDrawingCard(int number)
    {
        switch (number)
        {
            case 1:
                {
                    drawingCard1 = true;
                    break;
                }
            case 2:
                {
                    drawingCard2 = true;
                    break;
                }
            case 3:
                {
                    drawingCard3 = true;
                    break;
                }
        }
    }
    /*
    public string getFiledInfo(Image field)
    {
        if ()
    }
    */
}
