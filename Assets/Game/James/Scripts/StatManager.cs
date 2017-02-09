using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class StatManager : MonoBehaviour 
{
	public Story story = null;


	public string dayNumberInkVarName;
	public string levelInkVarName;
	public string maxHPInkVarName;
	public string currentHPInkVarName;
	public string attackInkVarName;
	public string defenceInkVarName;
	public string moneyInkVarName;
	public string bagInkVarName;

	public int dayNumber;
	public int level;
	public int maxHP;
	public int currentHP;
	public int attack;
	public int defence;
	public int money;
	public int bag;

	public Text dayNumberText;
	public Text levelText;
	public Text hpText;
	public Text attackText;
	public Text defenceText;
	public Text moneyText;
	public Text bagText;

	public void Init(Story story)
	{
		this.story = story;
	}

	public void PullStats()
	{
		dayNumber = (int) story.variablesState [dayNumberInkVarName];
		level = (int) story.variablesState [levelInkVarName];
		maxHP = (int) story.variablesState [maxHPInkVarName];
		currentHP = (int) story.variablesState [currentHPInkVarName];
		attack = (int) story.variablesState [attackInkVarName];
		defence = (int) story.variablesState [defenceInkVarName];
		money = (int) story.variablesState [moneyInkVarName];
		bag = (int) story.variablesState [bagInkVarName];
	}

//	public void PushStats()
//	{
//		story.variablesState[dayNumberInkVarName] = dayNumber;
//		story.variablesState[levelInkVarName] = level;
//		story.variablesState[maxHPInkVarName] = maxHP;
//		story.variablesState[currentHPInkVarName] = currentHP;
//		story.variablesState[attackInkVarName] = attack;
//		story.variablesState[defenceInkVarName] = defence;
//		story.variablesState[moneyInkVarName] = money;
//		story.variablesState[bagInkVarName] = bag;
//
//	}
//
//	private void OnValidate()
//	{
//		if (story == null)
//		{
//			return;
//		}
//		PushStats ();
//	}

	private void Update()
	{
		if (story == null)
		{
			return;
		}
		PullStats ();
		dayNumberText.text = "Day " + dayNumber;
		levelText.text = "" + level;
		hpText.text = "" + currentHP + "/" + maxHP;
		attackText.text = "" + attack;
		defenceText.text = "" + defence;
		moneyText.text = "" + money + "g";
		bagText.text = "" + bag + "/5";
	}
}