using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameButtonController : MonoBehaviour {

    private bool buttonActive;
    private SpriteRenderer renderer;
    private GameObject cardNetworkManagerObject;
    private CardNetworkManager cardNetworkManager;
    private GameObject cardManagerObject;
    private CardManager cardManager;


    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        cardNetworkManagerObject = GameObject.Find("NetworkManager");
        cardNetworkManager = cardNetworkManagerObject.GetComponent<CardNetworkManager>();
        cardManagerObject = GameObject.Find("CardManager");
        cardManager = cardManagerObject.GetComponent<CardManager>();
        setButtonInactive();
    }

    internal void setButtonActive()
    {
        buttonActive = true;
        renderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    internal void setButtonInactive()
    {
        buttonActive = false;
        renderer.color = new Color(1.0f, 1.0f, 1.0f, 0);
    }

    public void applyMove()
    {
        if (buttonActive)
        {
            if (!cardManager.getMagicCard())
            {
                cardNetworkManager.sendMoveCardRequest(cardManager.getMovingCardFrom(), cardManager.getMovingCardTo());
                cardManager.changeCardPosition(cardManager.getMovingCardFrom(), cardManager.getMovingCardTo());
            }
            else
            {
                cardNetworkManager.sendMagic(cardManager.getMovingCardFrom());
            }

            cardManager.setAllButtonsInactive();
        }
    }

    public void cancelMove()
    {
        if(buttonActive)
            cardManager.cancelMove(cardManager.getMovingCardFrom(), cardManager.getMovingCardTo());
    }

    public void endMove()
    {

    }
}
