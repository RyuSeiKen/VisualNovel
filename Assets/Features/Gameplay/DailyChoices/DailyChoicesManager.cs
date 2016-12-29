using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Feature.Gameplay {
    
    public delegate void ValidateClickHandler();
    
    public class DailyChoicesManager : MonoBehaviour, IDailyChoicesDisplay {

        /************
         * NARATIVE *
         ************/
        public Narative.NarativeManager narativeManager;

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
        private SubjectChoice morningSubjectChoice = null;
        // Afternoon
        private SubjectChoice afternoonSubjectChoice = null;
        // Validate envent
        private DailyChoicesValidatedHandler eDailyChoicesValidated;

        // Canvas
        //public Canvas canvas;

        private void Start() {
            // Initialise getSubjectInstanceFromSubjectDataDictionary
            LoadGetSubjectInstanceFromSubjectDataDictionary();
            // Bind OnSubjectClick handler to the subject choice class
            SubjectChoice.eSubjectChoiceWasClicked = OnSubjectChoiceClicked;
            // Clean interface
            Clean();
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

        private void OnSubjectChoiceClicked(SubjectChoice subjectChoice) {
            SubjectInstance subjectInstance;
            if (!getSubjectInstanceFromSubjectDataDictionary.TryGetValue(subjectChoice.subjectData, out subjectInstance)) {
            }
            if(TryToSelectSubject(subjectInstance, subjectChoice.timeOfTheDay)) {
                switch (subjectChoice.timeOfTheDay) {
                    case TimeOfTheDay.MORNING:
                        // Unselect previewsly selected choice
                        if (morningSubjectChoice != null) morningSubjectChoice.Unselect();
                        // Select the new subject
                        morningSubjectChoice = subjectChoice;
                        subjectChoice.Select();
                        break;
                    case TimeOfTheDay.AFTERNOON:
                        // Unselect previewsly selected choice
                        if (afternoonSubjectChoice != null) afternoonSubjectChoice.Unselect();
                        // Select the new subject
                        afternoonSubjectChoice = subjectChoice;
                        subjectChoice.Select();
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
                        narativeManager.narativeDisplays.text.SetTextToDisplay(log);
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
                        narativeManager.narativeDisplays.text.SetTextToDisplay(log);
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
                narativeManager.narativeDisplays.text.SetTextToDisplay(log);
            }
        }

    }
}