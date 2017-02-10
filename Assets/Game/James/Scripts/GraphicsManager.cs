using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsManager : MonoBehaviour 
{
	public string slimeName;
	public string ratName;
	public string lizardName;
	public string mushroomName;
	public string treeName;
	public string plantName;
	public string scorpionName;
	public string lionName;
	public string soldierName;
	public string flameName;
	public string dragonName;

	public string blacksmithName;
	public string tavernName;
	public string churchName;
	public string innName;

	public Sprite slimeSprite;
	public Sprite ratSprite;
	public Sprite lizardSprite;
	public Sprite mushroomSprite;
	public Sprite treeSprite;
	public Sprite plantSprite;
	public Sprite scorpionSprite;
	public Sprite lionSprite;
	public Sprite soldierSprite;
	public Sprite flameSprite;
	public Sprite dragonSprite;

	public Sprite blacksmithSprite;
	public Sprite tavernSprite;
	public Sprite churchSprite;
	public Sprite innSprite;

	Image image;
	RectTransform rect;

	void Start () 
	{
		image = GetComponent<Image>();
		rect = GetComponent<RectTransform>();
		James.NarrativeManager.eSwitchMonster += SwitchMonster;
		James.NarrativeManager.eSwitchPlace += ShowPlace;
		James.NarrativeManager.eKillMonster += KillMonster;
	}

	void OnDestroy () 
	{
		James.NarrativeManager.eSwitchMonster -= SwitchMonster;
		James.NarrativeManager.eSwitchPlace -= ShowPlace;
		James.NarrativeManager.eKillMonster -= KillMonster;
	}

	void ShowPlace(string placeName)
	{
		if(placeName == blacksmithName)
		{
			ShowMonster(blacksmithSprite);
		}
		if(placeName == tavernName)
		{
			ShowMonster(tavernSprite);
		}
		if(placeName == churchName)
		{
			ShowMonster(churchSprite);
		}
		if(placeName == innName)
		{
			ShowMonster(innSprite);
		}
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
		if(monsterName == lizardName)
		{
			ShowMonster(lizardSprite);
		}
		if(monsterName == mushroomName)
		{
			ShowMonster(mushroomSprite);
		}
		if(monsterName == treeName)
		{
			ShowMonster(treeSprite);
		}
		if(monsterName == plantName)
		{
			ShowMonster(plantSprite);
		}
		if(monsterName == scorpionName)
		{
			ShowMonster(scorpionSprite);
		}
		if(monsterName == lionName)
		{
			ShowMonster(lionSprite);
		}
		if(monsterName == soldierName)
		{
			ShowMonster(soldierSprite);
		}
		if(monsterName == flameName)
		{
			ShowMonster(flameSprite);
		}
		if(monsterName == dragonName)
		{
			ShowMonster(dragonSprite);
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