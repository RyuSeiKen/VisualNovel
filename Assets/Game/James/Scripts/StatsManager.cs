using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class StatsManager : MonoBehaviour 
{
	public Story story = null;

	[Header("Config")]
	public string combatInkVarName;
	public string militaryInkVarName;
	public string desertInkVarName;
	public string dncInkVarName;

	[Header("VAR")]
	public int combat;
	public int military;
	public int desert;
	public int dnc;

	public void Init(Story story){
		this.story = story;
	}

	public void PullStats(){
		combat = (int) story.variablesState [combatInkVarName];
		military = (int) story.variablesState [militaryInkVarName];
		desert = (int) story.variablesState [desertInkVarName];
		dnc = (int) story.variablesState [dncInkVarName];
	}

	public void PushStats(){
		story.variablesState [combatInkVarName] = combat;
		story.variablesState [militaryInkVarName] = military;
		story.variablesState [desertInkVarName] = desert;
		story.variablesState [dncInkVarName] = dnc;
	}

	private void OnValidate(){
		if (story == null)
			return;
		PushStats ();
	}

	private void Update(){
		if (story == null)
			return;
		PullStats ();
	}

}
