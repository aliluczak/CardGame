//http://answers.unity3d.com/questions/19284/anyone-have-a-double-click-script-more-reliable-th.html
using UnityEngine;
using System.Collections;

public class DoubleClick : MonoBehaviour
{
	private float doubleClickStart = -1.0f;
	private bool disableClicks  = false;

	void OnDoubleClick()
	{   
		Debug.Log("Double Click");     
	}
	
	IEnumerator lockClicks()
	{
		disableClicks = true;
		yield return new WaitForSeconds(0.4f);
		disableClicks = false;
	}

	void Update()
	{
		if (Input.GetMouseButtonUp(0))
		{
			if (disableClicks)
				return;
			
			if (doubleClickStart > 0 && (Time.time - doubleClickStart) < 0.4)
			{
				this.OnDoubleClick();
				doubleClickStart = -1;
				lockClicks();
			}
			else
			{
				doubleClickStart = Time.time;
			}
		}
	}
}
