using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Feature.Gameplay {
    
    public delegate void ValidateClickHandler();
    
    public class DailyChoicesDisplay_A : MonoBehaviour, IDailyChoicesDisplay {

        /*************
         * NARRATIVE *
         *************/
        public Narrative.NarrativeManager narrativeManager;

        /**********
         * CHOICE *
         **********/
        // Containers
        /// <summary>
        /// The transform
        /// </summary>
        [Header("Containers")]
        public Transform mourningSubjectsContainer;
        public Transform afternoonSubjectsContainer;
        // Subjects
        public List<Data.Subject> subjectDataList;
        public Dictionary<Data.Subject, SubjectInstance> getSubjectInstanceFromSubjectDataDictionary;
        public void LoadGetSubjectInstanceFromSubjectDataDictionary() {
            getSubjectInstanceFromSubjectDataDictionary = new Dictionary<Data.Subject, SubjectInstance>();
            foreach (var subjectData in subjectDataList) {
                getSubjectInstanceFromSubjectDataDictionary.Add(subjectData, SubjectInstance.Create(subjectData));
            }
        }
        // Choices
        private DailyChoices dailyChoice = null;
        // Buttons
        // Morning
        private SubjectChoiceButton morningSubjectChoice = null;
        // Afternoon
        private SubjectChoiceButton afternoonSubjectChoice = null;
        // Validate envent
        private DailyChoicesValidatedHandler eDailyChoicesValidated;

        // Canvas
        //public Canvas canvas;

        private void Start() {
            // Initialise getSubjectInstanceFromSubjectDataDictionary
            LoadGetSubjectInstanceFromSubjectDataDictionary();
            // Bind OnSubjectClick handler to the subject choice class
            SubjectChoiceButton.eSubjectChoiceButtonWasClicked = OnSubjectChoiceButtonWasClicked;
            // Clean interface
            Clean();

            //Ink.Runtime.;
        }

        public void Init(DailyChoicesValidatedHandler dailyChoicesValidatedHandler) {
            dailyChoice = new DailyChoices();
            gameObject.SetActive(true);
            this.eDailyChoicesValidated = dailyChoicesValidatedHandler;
        }

        private void Clean() {
            // Release the previews daily choice
            dailyChoice = null;
            // Unselect previewsly selected choice
            if (morningSubjectChoice != null) morningSubjectChoice.Unselect();
            // Unselect previewsly selected choice
            if (afternoonSubjectChoice != null) afternoonSubjectChoice.Unselect();
            // Hide canvas
            gameObject.SetActive(false);
        }

        private void OnSubjectChoiceButtonWasClicked(SubjectChoiceButton subjectChoiceButton) {
            SubjectInstance subjectInstance;
            if (!getSubjectInstanceFromSubjectDataDictionary.TryGetValue(subjectChoiceButton.subjectData, out subjectInstance)) {
            }
            if(TryToSelectSubject(subjectInstance, subjectChoiceButton.timeOfTheDay)) {
                switch (subjectChoiceButton.timeOfTheDay) {
                    case TimeOfTheDay.MORNING:
                        // Unselect previewsly selected choice
                        if (morningSubjectChoice != null) morningSubjectChoice.Unselect();
                        // Select the new subject
                        morningSubjectChoice = subjectChoiceButton;
                        subjectChoiceButton.Select();
                        break;
                    case TimeOfTheDay.AFTERNOON:
                        // Unselect previewsly selected choice
                        if (afternoonSubjectChoice != null) afternoonSubjectChoice.Unselect();
                        // Select the new subject
                        afternoonSubjectChoice = subjectChoiceButton;
                        subjectChoiceButton.Select();
                        break;
                }
                
            }
        }

        private bool TryToSelectSubject(SubjectInstance subject, TimeOfTheDay timeOfTheDay) {
            // Try to chose subject depending of the selected time of the day
            switch (timeOfTheDay) {
                case TimeOfTheDay.MORNING:
                    if (dailyChoice.subjectChosenThisMorning == subject) {
                        // Trying to chose an already chosen subject
                        string log = string.Format("You already chosed the subject \"{0}\" this morning.", subject.name) ;
                        Debug.Log(log);
                        narrativeManager.narrativeDisplays.text.SetTextToDisplay(log);
                        return false;
                    } else {
                        // Chose a new subject this morning
                        SelectSubject(subject, timeOfTheDay);
                    }
                    break;
                case TimeOfTheDay.AFTERNOON:
                    if (dailyChoice.subjectChosenThisAfternoon == subject) {
                        // Trying to chose an already chosen subject
                        string log = string.Format("You already chosed the subject \"{0}\" this afternoon.", subject.name);
                        Debug.Log(log);
                        narrativeManager.narrativeDisplays.text.SetTextToDisplay(log);
                        return false;
                    } else {
                        // Chose a new subject afternoon
                        SelectSubject(subject, timeOfTheDay);
                    }
                    break;
            }
            return true;
        }

        private void SelectSubject(SubjectInstance subject, TimeOfTheDay timeOfTheDay) {
            switch (timeOfTheDay) {
                case TimeOfTheDay.MORNING:
                    // Select the new subject
                    dailyChoice.subjectChosenThisMorning = subject;
                    break;
                case TimeOfTheDay.AFTERNOON:
                    // Select the new subject
                    dailyChoice.subjectChosenThisAfternoon = subject;
                    break;
            }
        }

        public void ValidateClickHandler() {
            if(
                dailyChoice != null &&
                dailyChoice.IsValid() &&
                eDailyChoicesValidated != null
            ) {
                eDailyChoicesValidated(dailyChoice);
                Clean();
            } else {
                string log = string.Format("You must chose two study subjects tooday to proceed.");
                Debug.Log(log);
                narrativeManager.narrativeDisplays.text.SetTextToDisplay(log);
            }
        }

    }
}