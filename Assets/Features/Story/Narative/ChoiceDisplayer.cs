using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;

namespace Feature.Narative 
{
	public delegate void OnChooseChoiceHandler(Choice choice);

	public class ChoiceDisplayer : MonoBehaviour 
	{
		public GameObject choicePrefab;
		public Transform choiceContainer;

		public void DisplayChoice(Choice choice, OnChooseChoiceHandler handler)
		{
			GameObject choiceGo = Instantiate(choicePrefab);
			choiceGo.name = choice.text;
			choiceGo.transform.SetParent(choiceContainer);
			Button button = choiceGo.GetComponentInChildren<Button>();
			Text buttonText = choiceGo.GetComponentInChildren<Text>();
			buttonText.text = choice.text;
			button.onClick.AddListener(delegate 
				{
					handler(choice);
					ClearChoices();
				});
		}

		public void ClearChoices() 
		{
			int childCount = choiceContainer.childCount;
			for (int i = childCount - 1; i >= 0; --i) 
			{
				GameObject.Destroy(choiceContainer.GetChild(i).gameObject);
			}
		}
			
	}
}