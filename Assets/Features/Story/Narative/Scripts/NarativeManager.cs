using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Ink.Runtime;

namespace Feature.Narative {
    
    public class NarativeManager : MonoBehaviour, INarativeManagerMock {

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
        /// The Ink story represent a complete Ink narative, and manage the evaluation and state of it.
        /// </summary>
        private Story story;


        /***********
         * DISPLAY *
         ***********/

        // Naration
        /// <summary>
        /// Contains everythign related to narative display. Such as though, dialogs and choices.
        /// </summary>
        [System.Serializable]
        public class NarativeDisplays {

            [SerializeField]
            private ShaderBasedTextDisplayAnimator textAnimator;
            [SerializeField]
            private ChoiceDisplayer choiceDisplay;

            /// <summary>
            /// The display system used to animate the display of regular sentences and dialogs.
            /// </summary>
            public ITextDisplayAnimator text { get { return textAnimator; } }
            
            /// <summary>
            /// The display system used to display choices.
            /// </summary>
            public IChoiceDisplay choice { get { return choiceDisplay; } }
        }

        /// <summary>
        /// Contains everythign related to narative display. Such as though, dialogs and choices.
        /// </summary>
        [Header("Display Setup")]
        [SerializeField]
        public NarativeDisplays narativeDisplays;

        // Gameplay
        /// <summary>
        /// Contains every display related to this game's specific gameplay. Such as daily subject choices.
        /// </summary>
        [System.Serializable]
        public class GameplayDisplays {

            [SerializeField]
            private Gameplay.DailyChoicesManager dailyChoicesDisplay;

            /// <summary>
            /// The display system used to display the daily subject choices event.
            /// </summary>
            public Gameplay.IDailyChoicesDisplay dailyChoices { get { return dailyChoicesDisplay; } }
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

            if (Input.GetMouseButtonDown(0) || Input.GetKey(KeyCode.LeftControl)) {

                Continue();

            }

        }
			
        /*********
         * LOGIC *
         *********/
        /// <summary>
        /// Initialise this narative manager. Please refrain from calling it more than once.
        /// </summary>
        public void Init() {
            story = inkSetup.Setup();
        }

        /// <summary>
        /// This is the core method of the narative manager. It advance in the narative.
        /// </summary>
        public void Continue() {

            // Compleatly display text if was in the process of displaying it
            if (!narativeDisplays.text.HasAnimationReachedLastCharacter()) {

                narativeDisplays.text.DisplayAllTextNow();

            }
            else
            // Continue storry
            {

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
                        narativeDisplays.text.SetTextToDisplay(text);

                    } else {

                        // Display nothing if a tag asked to skip text display
                        narativeDisplays.text.SetTextToDisplay("");

                    }

                    // Display choices
                    // Skip choice display if requested by a tag
                    if ((tagHandingOutput & TagHandingOutput.SKIP_CHOICE) == 0) {
                        if (story.currentChoices.Count > 0) {

                            narativeDisplays.choice.ClearChoices();
                            foreach (Choice c in story.currentChoices) {

                                narativeDisplays.choice.DisplayChoice(c, HandleChoiceChosed);

                            }

                        }
                    }

                } else if (story.currentChoices.Count <= 0) {

                    // Reset story if it both can't continue and there is no choices left to do.
                    story.ResetState();

                    // Debug : Display the main input used to play.
                    narativeDisplays.text.SetTextToDisplay("Start (Left click)");

                }

            }
        }




        /*****************************
         * INK CONVENTIONAL HANDLERS *
         *****************************/

        /// <summary>
        /// Called whenever the player select a basic choice.
        /// </summary>
        /// <param name="choice"></param>
        public void HandleChoiceChosed(Choice choice) {
            story.ChooseChoiceIndex(choice.index);
            Continue();
        }

        
        // Tag handling

        /// <summary>
        /// Describe all effect a tag can have on the narative Continue() method.
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
                case "waitForDailyChoiceValidation":
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
            Gameplay.IDailyChoicesDisplay dailyChoicesManager = gameplayDisplays.dailyChoices;

            // DEBUG : "Manualy" retrieve the daily choices display if not found
            if (dailyChoicesManager == null) {
                // Retrieve the game object charged of displaying the daily subjects choices event.
                const string dailyChoicesGoName = "DailyChoices";
                GameObject dailyChoicesGo = GameObject.Find(dailyChoicesGoName);

                // Retrieve the daily choices display component.
                dailyChoicesManager = dailyChoicesGo.GetComponent<Gameplay.DailyChoicesManager>();
            }
            
            // Initialise the daily choices display, that will make a callback when the player is done chosing for subject.
            dailyChoicesManager.Init(HandleDailyChoiceValidation);

        }

        /// <summary>
        /// HandleDailyChoiceValidation() is called whenever the player has finiched chosing his daily subject trough the daily subjects choices disaply.
        /// </summary>
        public void HandleDailyChoiceValidation(Gameplay.DailyChoices dailyChoices) {

            // Ink : How ink global variables related to chosen subject are named.
            const string SUBJECT_MORNING    = "SUBJECT_MORNING";
            const string SUBJECT_AFTERNOON  = "SUBJECT_AFTERNOON";
            // Ink : Set those global variables acording to the choices made by the player.
            story.variablesState[SUBJECT_MORNING] = dailyChoices.subjectChosenThisMorning.inkGlobalVaraibleName;
            story.variablesState[SUBJECT_AFTERNOON] = dailyChoices.subjectChosenThisAfternoon.inkGlobalVaraibleName;
            
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