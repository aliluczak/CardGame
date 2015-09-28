using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameButtonController : MonoBehaviour {

    private bool buttonActive;
    private GameObject cardNetworkManagerObject;
    private CardNetworkManager cardNetworkManager;
    private GameObject cardManagerObject;
    private CardManager cardManager;
    private Button button;


    void Start()
    {
        button = GetComponent<Button>();
 //       cardNetworkManagerObject = GameObject.Find("NetworkManager");
 //       cardNetworkManager = cardNetworkManagerObject.GetComponent<CardNetworkManager>();
        cardManagerObject = GameObject.Find("CardManager");
        cardManager = cardManagerObject.GetComponent<CardManager>();
        setButtonInactive();
    }

    internal void setButtonActive()
    {
        buttonActive = true;
        button.image.color = new Color(255f, 255f, 255f, 1.0f);
    }

    internal void setButtonInactive()
    {
        buttonActive = false;
        button.image.color = new Color(255f, 255f, 255f, 0f);
    }

    public void applyMove()
    {
        if (buttonActive)
        {
            if (!cardManager.getMagicCard())
            {
               // cardNetworkManager.sendMoveCardRequest(cardManager.getMovingCardFrom(), cardManager.getMovingCardTo());
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
