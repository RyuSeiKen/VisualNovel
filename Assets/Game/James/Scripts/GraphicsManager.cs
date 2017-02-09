using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsManager : MonoBehaviour 
{
	public string slimeName;
	public string ratName;

	public Sprite slimeSprite;
	public Sprite ratSprite;

	Image image;
	RectTransform rect;

	void Start () 
	{
		image = GetComponent<Image>();
		rect = GetComponent<RectTransform>();
		James.NarrativeManager.eSwitchMonster += SwitchMonster;
		James.NarrativeManager.eKillMonster += KillMonster;
	}

	void SwitchMonster(string monsterName)
	{
		if(monsterName == slimeName)
		{
			ShowMonster(slimeSprite);
		}
		if(monsterName == ratName)
		{
			ShowMonster(ratSprite);
		}
	}

	void ShowMonster(Sprite sprite)
	{
		image.color = Color.white;
		image.sprite = sprite;
		image.SetNativeSize();
		Vector2 size = rect.sizeDelta;
		size *= 4;
		rect.sizeDelta = size;
	}

	void KillMonster()
	{
		image.color = Color.black;
	}
}