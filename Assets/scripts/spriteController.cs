using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class spriteController : MonoBehaviour {

    private GameObject cardManagerObject;
    private CardManager cardManager;
    private Image image;
	// Use this for initialization
	void Start () 
    {
        cardManagerObject = GameObject.Find("CardManager");
        cardManager = cardManagerObject.GetComponent<CardManager>();
        image = GetComponent<Image>();
	}

    public void imageClick()
    {
        if (cardManager.movingPhaseIsActive())
        {
            bool magicCard = false;
            switch (this.gameObject.name)
            {
                case "RandomHero":
                    {
                        cardManager.setCardSelected(2);
                        cardManager.highlightSprites();
                        Debug.Log("Card seleted");
                        break;
                    }
                case "RandomCard1":
                    {
                        cardManager.setCardSelected(3);
                        if (cardManager.checkCardType(3).Equals("HERO"))
                        {
                            cardManager.highlightSprites();
                        }
                        else
                        {
                            magicCard = true;
                        }
                        break;
                    }
                case "RandowmCard2":
                    {
                        cardManager.setCardSelected(4);
                        if (cardManager.checkCardType(4).Equals("HERO"))
                        {
                            cardManager.highlightSprites();
                        }
                        else
                        {
                            magicCard = true;
                        }
                        break;
                    }
                case "Hero":
                    {
                        if (cardManager.whichCardSelected() != -1 && !magicCard)
                        {
                            cardManager.tryMoveCard(cardManager.whichCardSelected(), 0);
                        }
                        else
                        {
                            cardManager.textMessage("Nie wybrano karty");
                        }

                        break;
                    }
                case "Support":
                    {
                        if (cardManager.whichCardSelected() != -1 && !magicCard)
                        {
                            cardManager.tryMoveCard(cardManager.whichCardSelected(), 1);
                        }

                        break;
                    }
            }

            if (magicCard)
            {
                cardManager.useMagicButton();
            }
        }
    }

    public void showImage(Sprite image)
    {
        this.image.sprite = image;
    }

    public void showImage()
    {
        this.image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
    public void highlightImage()
    {
        this.image.sprite = null;
        this.image.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    }

    public void hideImage()
    {
        this.image.sprite= null;
        this.image.color = new Color(1.0f, 1.0f, 1.0f, 0f);
    }

    public void possibleHideImage()
    {
        this.image.color = new Color (1.0f, 1.0f, 1.0f, 0f);
    }

}
