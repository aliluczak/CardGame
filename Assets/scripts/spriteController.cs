using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class spriteController : MonoBehaviour {

    private GameObject cardManagerObject;
    private CardManager cardManager;
    private Sprite texture;
    private SpriteRenderer renderer;
	// Use this for initialization
	void Start () 
    {
        cardManagerObject = GameObject.Find("CardManager");
        cardManager = cardManagerObject.GetComponent<CardManager>();
        texture = gameObject.GetComponent<Sprite>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
	}

    void OnMouseDown()
    {
        if (cardManager.movingPhaseIsActive())
        {
            switch (this.gameObject.name)
            {
                case "RandomHero":
                    {
                        cardManager.setCardSelected(2);
                        cardManager.highlightSprites();
                        break;
                    }
                case "RandomCard1":
                    {
                        cardManager.setCardSelected(3);
                        cardManager.highlightSprites();
                        break;
                    }
                case "RandowmCard2":
                    {
                        cardManager.setCardSelected(4);
                        cardManager.highlightSprites();
                        break;
                    }
                case "Hero":
                    {
                        if (cardManager.whichCardSelected()!=-1)
                        {
                            cardManager.tryMoveCard(cardManager.whichCardSelected(), 0);
                        }

                        break;
                    }
                case "Support":
                    {
                        if (cardManager.whichCardSelected() != -1)
                        {
                            cardManager.tryMoveCard(cardManager.whichCardSelected(), 1);
                        }
                        break;
                    }
            }
        }
        else
        {
            //TODO
        } 

    }

    public void showImage(Sprite image)
    {
        texture = image;
    }

    public void showImage()
    {
        renderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
    public void highlightImage()
    {
        texture = null;
        renderer.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    }

    public void hideImage()
    {
        texture = null;
    }

    public void possibleHideImage()
    {
        renderer.color = new Color (1.0f, 1.0f, 1.0f, 0.5f);
    }

}
