using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class StatManager : MonoBehaviour 
{
	public Story story = null;

	public string levelInkVarName;
	public string maxHPInkVarName;
	public string currentHPInkVarName;
	public string attackInkVarName;
	public string defenceInkVarName;
	public string experienceInkVarName;

	public int level;
	public int maxHP;
	public int currentHP;
	public int attack;
	public int defence;
	public int experience;

	public Text levelText;
	public Text hpText;
	public Text attackText;
	public Text defenceText;

	public void Init(Story story)
	{
		this.story = story;
	}

	public void PullStats()
	{
		level = (int) story.variablesState [levelInkVarName];
		maxHP = (int) story.variablesState [maxHPInkVarName];
		currentHP = (int) story.variablesState [currentHPInkVarName];
		attack = (int) story.variablesState [attackInkVarName];
		defence = (int) story.variablesState [defenceInkVarName];
		experience = (int) story.variablesState [experienceInkVarName];
	}

	public void PushStats()
	{
		story.variablesState[levelInkVarName] = level;
		story.variablesState[maxHPInkVarName] = maxHP;
		story.variablesState[currentHPInkVarName] = currentHP;
		story.variablesState[attackInkVarName] = attack;
		story.variablesState[defenceInkVarName] = defence;
		story.variablesState[experienceInkVarName] = experience;
	}

	private void OnValidate()
	{
		if (story == null)
		{
			return;
		}
		PushStats ();
	}

	private void Update()
	{
		if (story == null)
		{
			return;
		}
		PullStats ();
		levelText.text = "" + level;
		hpText.text = "" + currentHP + "/" + maxHP;
		attackText.text = "" + attack;
		defenceText.text = "" + defence;
	}
}