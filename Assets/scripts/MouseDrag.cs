//http://forum.unity3d.com/threads/drag-an-object-in-x-direction-using-c.56313/
using UnityEngine;
using System.Collections;

public class MouseDrag : MonoBehaviour
{
	public float posX;
	public float posY;
	public float posZ;
	public Vector3 mousePos;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		posX = transform.position.x;
		posY = transform.position.y;
		posZ = transform.position.z;
	}

	//click on an object and move it in the x direction, based on the mouse
	void OnMouseDrag()
	{
		Vector3 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		point.z = gameObject.transform.position.z;
		point.y = gameObject.transform.position.y;
		gameObject.transform.position = point;
	}
}
