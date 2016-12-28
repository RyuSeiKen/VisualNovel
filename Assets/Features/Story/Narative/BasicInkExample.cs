using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;


namespace Feature.Narative {

    public class BasicInkExample : MonoBehaviour {
        [SerializeField]
        [Header("Ink Setup")]
        private TextAsset inkJSONAsset;
        private Story story;

        [SerializeField]
        private Canvas canvas;

        // UI Prefabs
        [SerializeField]
        private Text textPrefab;
        [SerializeField]
        private Button buttonPrefab;
        
        void Awake() {
            StartStory();
        }

        void StartStory() {
            story = new Story(inkJSONAsset.text);


            //RefreshView();
        }

        void RefreshView() {
            RemoveChildren();

            while (story.canContinue) {
                string text = story.Continue().Trim();
                CreateContentView(text);
            }

            if (story.currentChoices.Count > 0) {
                for (int i = 0; i < story.currentChoices.Count; i++) {
                    Choice choice = story.currentChoices[i];
                    Button button = CreateChoiceView(choice.text.Trim());
                    button.onClick.AddListener(delegate {
                        OnClickChoiceButton(choice);
                    });
                }
            } else {
                Button choice = CreateChoiceView("End of story.\nRestart?");
                choice.onClick.AddListener(delegate {
                    StartStory();
                });
            }
        }

        void OnClickChoiceButton(Choice choice) {
            story.ChooseChoiceIndex(choice.index);
            RefreshView();
        }

        void CreateContentView(string text) {
            Text storyText = Instantiate(textPrefab) as Text;
            storyText.text = text;
            storyText.transform.SetParent(canvas.transform, false);
        }

        Button CreateChoiceView(string text) {
            Button choice = Instantiate(buttonPrefab) as Button;
            choice.transform.SetParent(canvas.transform, false);

            Text choiceText = choice.GetComponentInChildren<Text>();
            choiceText.text = text;

            HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
            layoutGroup.childForceExpandHeight = false;

            return choice;
        }

        void RemoveChildren() {
            int childCount = canvas.transform.childCount;
            for (int i = childCount - 1; i >= 0; --i) {
                GameObject.Destroy(canvas.transform.GetChild(i).gameObject);
            }
        }

        [Header("Animation")]
        public TextDisplayAnimation textAnimation = new TextDisplayAnimation();
        
        public void Update() {
            // Tick animation logic
            textAnimation.Tick(Time.deltaTime);

            // Input
            if (Input.GetMouseButtonDown(0)) {
                // Compleatly display text if was in the process of displaying it
                if (!textAnimation.lastCharacterReached) {
                    textAnimation.DisplayAll();
                } else
                // Continue storry
                {
                    if (story.canContinue) {
                        // Continue story
                        string text = story.Continue();
                        text = text.TrimEnd('\r', '\n');
                        // Start displaying this line
                        textAnimation.Set(text);

                        List<string> tagList = story.currentTags;
                        foreach(var tag in tagList) {
                            if (tag.StartsWith("actor")) {
                                Debug.Log("ACTOR : #" + tag);
                            } else {
                                Debug.Log("#" + tag);
                            }
                        }
                    } else {
                        // Reset story
                        story.ResetState();
                        textAnimation.Set("Left click");
                    }
                }
            }

            // Refresh animation material
            textAnimation.RefreshMaterial();
        }

        [System.Serializable]
        public class TextDisplayAnimation {

            /// <summary>
            /// The text animation material with the tex text animation shader.
            /// </summary>
            [Header("References")]
            public Material material;

            /// <summary>
            /// The Text UI the text must be displayed on.
            /// </summary>
            public Text textUi;

            /// <summary>
            /// The Text UI rectangle, from the screen top left position.
            /// </summary>
            private Vector4 textRect;

            /// <summary>
            /// The curent animation time.
            /// </summary>
            private double time;

            public bool lastCharacterReached { get; protected set; }

            // Config
            [Header("Configuration")]
            public float pixelPerSeconds = 512;
            [Range(0, 8)]
            public float speedMultiplicator = 1;
            public float lineHight = 32;
            public float lineHightOffset = -2;
            public float trantionLengh = 64;
            [Range(-1, 1)]
            public float transitionOffset = -1;

            public void Init(Material material, Text textUi) {
                this.material = material;
                this.textUi = textUi;
            }

            public void Set(string text) {
                // Set text on Text UI
                textUi.text = text;
                // Reset animation time
                time = 0;
            }

            public void Tick(double dt) {
                // Tick time
                time += dt * pixelPerSeconds * speedMultiplicator;

                // Tick text rectable
                RectTransform transform = (RectTransform)textUi.transform;
                RectTransform canvasTransform = (RectTransform)textUi.canvas.transform;
                Bounds bounds = RectTransformUtility.CalculateRelativeRectTransformBounds(canvasTransform, transform);
                textRect = new Vector4();
                textRect.x = bounds.min.x + canvasTransform.sizeDelta.x / 2;
                textRect.y = canvasTransform.sizeDelta.y / 2 - bounds.max.y;
                textRect.z = bounds.size.x;
                textRect.w = bounds.size.y;

                // Tick state
                lastCharacterReached = HasAnimationReachedLastCharacter();
            }
            
            public void RefreshMaterial() {
                // Set shader parameters on material
                material.SetFloat("_AnimationTime", (float)time);
                material.SetVector("_Canvas", textRect);
                material.SetFloat("_LineHight", lineHight);
                material.SetFloat("_LineHightOffset", lineHightOffset);
            }

            private bool HasAnimationReachedLastCharacter() {
                // The animation has reached the last character if there is no character to display.
                if (string.IsNullOrEmpty(textUi.text)) return true;

                // Find last character and it's bottom left vertex
                RectTransform transform = (RectTransform)textUi.transform;
                var textGen = textUi.cachedTextGenerator;
                var verts = textGen.verts;
                if (verts.Count <= 0) return false;
                UIVertex lastVertex = verts[verts.Count - 1];

                // Convert the UIVertex to a position inside the text rectangle.
                Vector3 position = lastVertex.position;
                position = new Vector3(
                    position.x,
                    -position.y,
                    0
                    );
                
                // Check, using the same "getProgress()" function in the shader, if the animation has reached/displayed it.
                bool hasAnimationReachedPoint = HasAnimationReachedPoint(position);

                // Debug
#if UNITY_EDITOR
#pragma warning disable 162
                const bool debug = true;
                if (debug) {
                    Color debugDrawColor;
                    if (hasAnimationReachedPoint) {
                        debugDrawColor = Color.green;
                    } else {
                        debugDrawColor = Color.red;
                    }

                    // Reposition point for display
                    position = new Vector3(
                        position.x + textRect.x,
                        -position.y - textRect.y + Screen.height,
                        0
                        );
                    Debug.DrawLine(textUi.transform.position, position, debugDrawColor);
                }
#pragma warning restore 162
#endif

                return hasAnimationReachedPoint;
            }

            private bool HasAnimationReachedPoint(Vector2 point) {
                /* #########################################################################
                 * # Look at the shader's "getProgress()" function before modifying this one #
                 * ######################################################################### */

                // Calculate signle axis position of characters's vertex
                float position = Mathf.Floor((point.y + lineHightOffset) / lineHight);
                position *= textRect.z;
                position += point.x;
                position += trantionLengh;
                // Calculate progress with transition
                float progress = (float)time - position;
                // Return progress without transition
                return progress > 0.5f;
            }

            public void DisplayAll() {
                time = double.PositiveInfinity;
            }

        }

    }
}