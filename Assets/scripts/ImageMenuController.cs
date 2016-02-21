using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImageMenuController : MonoBehaviour {

    private Sprite texture;
    private SpriteRenderer renderer;
    private Menu menu;
	// Use this for initialization
	void Start () {
        texture = gameObject.GetComponent<Sprite>();
        renderer = gameObject.GetComponent<SpriteRenderer>();
        menu = GameObject.Find("Menu").GetComponent<Menu>();
   
    }
	
	// Update is called once per frame
	void OnMouseDown()
    {
        switch(gameObject.name)
        {

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
}
