using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Feature.Gameplay {

    public class SubjectChoiceButton : MonoBehaviour, IPointerClickHandler {
        
        public Data.Subject subjectData;

        public TimeOfTheDay timeOfTheDay = TimeOfTheDay.NONE;

        public delegate void SubjectChoiceWasClickedHandler(SubjectChoiceButton subjectChoiceButton);
        public static SubjectChoiceWasClickedHandler eSubjectChoiceButtonWasClicked;

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData) {
            if (eSubjectChoiceButtonWasClicked != null) eSubjectChoiceButtonWasClicked(this);
        }

        public void Select() {
            // Make button ininteractible
            Button buttonUi = GetComponentInChildren<Button>();
            if (buttonUi != null) {
                buttonUi.interactable = false;
            }
            // Set button text color to selected color
            Text textUi = GetComponentInChildren<Text>();
            if (textUi != null) {
                textUi.color = Color.red;
            }
        }

        public void Unselect() {
            // Make button interactible
            Button buttonUi = GetComponentInChildren<Button>();
            if (buttonUi != null) {
                buttonUi.interactable = true;
            }
            // Restore button text color
            Text textUi = GetComponentInChildren<Text>();
            if (textUi != null) {
                textUi.color = Color.black;
            }
        }

        private void OnValidate() {
            if(subjectData != null) {
                gameObject.name = subjectData.name + " Button";
                Text textUi = GetComponentInChildren<Text>();
                if(textUi != null) {
                    textUi.text = subjectData.name;
                }
            }
        }

    }
}