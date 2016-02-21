using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class CardImageController : MonoBehaviour {

    private Sprite texture;
    private SpriteRenderer renderer;
    // Use this for initialization
    void Start () {
        texture = gameObject.GetComponent<Sprite>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
	}

    public void showImage(Sprite image)
    {
        texture = image;
    }

    public void showImage()
    {
        renderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    public void hideImage()
    {
        texture = null;
    }
}
