using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Ink.Runtime;
using Feature.Gameplay;
using Feature.Narrative;

namespace James {
    
    public class NarrativeManager : MonoBehaviour
	{
		public StatManager sManager;
        /*******
         * INK *
         *******/
        // Ink story setup
        [System.Serializable]
        public class InkSetup {
            /// <summary>
            /// .json file compiled from the a .ink file by inklecate.
            /// </summary>
            public TextAsset inkJSONAsset;

            /// <summary>
            /// Load the story from compiled ink .json file.
            /// </summary>
            /// <returns></returns>
            public Story Setup() {
                return new Story(inkJSONAsset.text);
            }
        }
        [Header("Ink Setup")]
        [SerializeField]
        public InkSetup inkSetup;

        /// <summary>
        /// The Ink story represent a complete Ink narrative, and manage the evaluation and state of it.
        /// </summary>
        private Story story;


        /***********
         * DISPLAY *
         ***********/

        // Naration
        /// <summary>
        /// Contains everythign related to narrative display. Such as though, dialogs and choices.
        /// </summary>
        [System.Serializable]
        public class NarrativeDisplays {

            [SerializeField]
			// private ShaderBasedTextDisplayAnimator textAnimator;
			private GameObject textAnimator;

            [SerializeField]
            private GameObject choiceDisplay;

            /// <summary>
            /// The display system used to animate the display of regular sentences and dialogs.
            /// </summary>
			public ITextDisplayAnimator text;
            
            /// <summary>
            /// The display system used to display choices.
            /// </summary>
			public IChoiceDisplay choice;

			public void Init(){
				text = textAnimator.GetComponent<ITextDisplayAnimator>();
			}
        }

        /// <summary>
        /// Contains everythign related to narrative display. Such as though, dialogs and choices.
        /// </summary>
        [Header("Display Setup")]
        [SerializeField]
        public NarrativeDisplays narrativeDisplays;

        // Gameplay
        /// <summary>
        /// Contains every display related to this game's specific gameplay. Such as daily subject choices.
        /// </summary>
        [System.Serializable]
        public class GameplayDisplays {

            [SerializeField]
			public DayPlanifier dayPlanifier;
        }

        /// <summary>
        /// Contains everythign related to gameplay display. Such as daily subject choices.
        /// </summary>
        [SerializeField]
        public GameplayDisplays gameplayDisplays;


        /*********
         * UNITY *
         *********/
         /// <summary>
         /// The Start function is called the next frame this component is loaded.
         /// </summary>
        private void Start() {
            Init();
        }

        /// <summary>
        /// The Start function is called once every frames. Use it for Input related logic.
        /// </summary>
        private void Update() {

            if (Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.LeftControl)) 
			{
                Continue();
            }

        }

        /*********
         * LOGIC *
         *********/
        /// <summary>
        /// Initialise this narrative manager. Please refrain from calling it more than once.
        /// </summary>
        public void Init() {
            story = inkSetup.Setup();

			sManager.story = story;

			narrativeDisplays.Init();

        }
        
        /// <summary>
        /// This is the core method of the narrative manager. It advance in the narrative.
        /// </summary>
        public void Continue() {

            // Compleatly display text if was in the process of displaying it
            if (!narrativeDisplays.text.HasAnimationReachedLastCharacter()) {

                narrativeDisplays.text.DisplayAllTextNow();

            }
            else
            // Continue storry
            if (story.canContinue) {
                    
                // Ink : Continue the story
                string text = story.Continue();
                text = text.TrimEnd('\r', '\n');

                // Check for tags
                TagHandingOutput tagHandingOutput = TagHandingOutput.NONE;
                List<string> tagList = story.currentTags;
                foreach (var tag in tagList) {

                    // Handle tag
                    tagHandingOutput |= HandleTag(tag);

                }

                // Display text
                // Skip text display if requested by a tag
                if ((tagHandingOutput & TagHandingOutput.SKIP_TEXT_DISPLAY) == 0) {

                    // Display the naration text.
                    narrativeDisplays.text.SetTextToDisplay(text);

                } 

                // Display choices
                // Skip choice display if requested by a tag
                if ((tagHandingOutput & TagHandingOutput.SKIP_CHOICE) == 0) {
                    if (story.currentChoices.Count > 0) {

                        narrativeDisplays.choice.ClearChoices();
                        foreach (Choice choice in story.currentChoices) {

                            narrativeDisplays.choice.DisplayChoice(choice, HandleRegualrChoiceChosed);

                        }

                    }
                }

            }
            else
            // Reset storry
            if (story.currentChoices.Count <= 0) {

                // Reset story if it both can't continue and there is no choices left to do.
                story.ResetState();

                // Debug : Display the main input used to play.
                narrativeDisplays.text.SetTextToDisplay("Start (Left click)");

            }
        }
			
        /*****************************
         * INK CONVENTIONAL HANDLERS *
         *****************************/

        /// <summary>
        /// Called whenever the player select a basic choice.
        /// </summary>
        /// <param name="choice"></param>
        public void HandleRegualrChoiceChosed(Choice choice) {
            story.ChooseChoiceIndex(choice.index);
            Continue();
        }

        
        // Tag handling

        /// <summary>
        /// Describe all effect a tag can have on the narrative Continue() method.
        /// </summary>
        public enum TagHandingOutput {
            NONE,
            SKIP_TEXT_DISPLAY,
            SKIP_CHOICE
        }

        /// <summary>
        /// Called whenever a tag is detected on a 
        /// </summary>
        /// <param name="tag"></param>
        public TagHandingOutput HandleTag(string tag) {

            TagHandingOutput output = TagHandingOutput.NONE;

            switch (tag) {
                case "choose":
                    HandleDailyChoices();
                    break;
                case "skipText":
                    output |= TagHandingOutput.SKIP_TEXT_DISPLAY;
                    break;
                case "skipChoice":
                    output |= TagHandingOutput.SKIP_CHOICE;
                    break;
                default:
                    Debug.Log(string.Format("Tag : \"#{0}\" not handled.", tag));
                    break;
            }

            return output;

        }

        /*************************
         * CUSTOM EVENT HANDLERS *
         *************************/
        /// <summary>
        /// HandleDailyChoices() is called whenever the story reach a point where the tag #waitForDailyChoiceValidation is defined in the Ink file.
        /// </summary>
        public void HandleDailyChoices() {

            // Retrieve the daily choices display that will be used to display the daily choices.
			DayPlanifier dailyChoicesManager = gameplayDisplays.dayPlanifier;

            // Initialise the daily choices display, that will make a callback when the player is done chosing for subject.
            dailyChoicesManager.Init(HandleDailyChoiceValidation);

        }

        /// <summary>
        /// HandleDailyChoiceValidation() is called whenever the player has finished chosing his daily subject trough the daily subjects choices disaply.
        /// </summary>
        public void HandleDailyChoiceValidation(James.DailyChoices dailyChoices) {

            // Ink : How ink global variables related to chosen subject are named in the .ink project.
			const string MORNING_PLACE = "MORNING_PLACE";
			const string AFTERNOON_PLACE = "AFTERNOON_PLACE";
			const string EVENING_PLACE = "EVENING_PLACE";

            // Ink : Set those global variables acording to the choices made by the player.
			story.variablesState[MORNING_PLACE] = new Path(dailyChoices.subjectChosenThisMorning.studieDivertName);
			story.variablesState[AFTERNOON_PLACE] = new Path(dailyChoices.subjectChosenThisAfternoon.studieDivertName);
			story.variablesState[EVENING_PLACE] = new Path(dailyChoices.subjectChosenThisEvening.studieDivertName);

            // Ink : Automatocly chose the blocking choice to tell Ink that the player finalised his choices.
            if(story.currentChoices.Count > 0) story.ChooseChoiceIndex(0);

            // Continue the story.
            Continue();
        }

        /*******************
         * CUSTOM HANDLERS *
         *******************/
        public void HandleActorClothesChange(object actor, object clothes) {
            throw new NotImplementedException();
        }

        public void HandleActorEnterScene(object actor, object entryType) {
            throw new NotImplementedException();
        }

        public void HandleActorExitScene(object actor, object extitType) {
            throw new NotImplementedException();
        }

        public void HandleActorFaceChange(object actor, object face) {
            throw new NotImplementedException();
        }

        public void HandleActorMovemnt(object actor, object movementType) {
            throw new NotImplementedException();
        }

        public void HandleActorPoseChange(object actor, object pause) {
            throw new NotImplementedException();
        }

        public void HandleActorSpeaker(object actor) {
            throw new NotImplementedException();
        }

        public void HandleBgmChange(object bgm) {
            throw new NotImplementedException();
        }

        public void HandleCgEventChange(object cg) {
            throw new NotImplementedException();
        }

        public void HandleCgEventEnd(object transitionType) {
            throw new NotImplementedException();
        }

        public void HandleCgEventStart(object cg, object transitionType) {
            throw new NotImplementedException();
        }

        public void HandleGfx(object gfx) {
            throw new NotImplementedException();
        }

        public void HandleSceneChange(object newScene, object transitionType) {
            throw new NotImplementedException();
        }

        public void HandleSfx(object sfx) {
            throw new NotImplementedException();
        }
    }
}