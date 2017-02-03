using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;

namespace Feature.Narrative 
{

	public class ChoiceDisplayer : MonoBehaviour, IChoiceDisplay {
		public GameObject choicePrefab;
		public Transform choiceContainer;

        public void Start() {
            ClearChoices();
        }

        /// <summary>
        /// Display a choice to be selected on screen.
        /// </summary>
        /// <param name="choice">The choice to be displayed.</param>
        /// <param name="handler">The handler to call when this choice is selected.</param>
        public void DisplayChoice(Choice choice, OnChooseChoiceHandler handler) {
            gameObject.SetActive(true);
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

        /// <summary>
        /// Instantly remove all displayed choices from screen.
        /// </summary>
        public void ClearChoices() 
		{
            TransformUtility.DestroyAllChildrens(choiceContainer);
            gameObject.SetActive(false);
		}
	}
}