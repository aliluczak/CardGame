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

    internal void applyMove()
    {
        cardNetworkManager.sendMoveCardRequest(cardManager.getMovingCardFrom(), cardManager.getMovingCardTo());
        cardManager.hideCardDetail();
    }

    internal void cancelMove()
    {
        cardManager.cancelMove(cardManager.getMovingCardFrom(), cardManager.getMovingCardTo());
        cardManager.hideCardDetail();
    }

    internal void endMove()
    {

    }
}
