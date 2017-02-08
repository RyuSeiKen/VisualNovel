using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterDisplay : MonoBehaviour {

	void Start () 
	{
		Monster();
	}
	
	void Monster()
	{
		GetComponent<Image>().SetNativeSize();
		Vector2 size = GetComponent<RectTransform>().sizeDelta;
		size *= 4;
		GetComponent<RectTransform>().sizeDelta = size;
	}
}
