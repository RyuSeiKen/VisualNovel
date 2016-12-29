using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.Narative {
    public interface ITextDisplayAnimator {

        /// <summary>
        /// The text that this animator will start to display from now on.
        /// </summary>
        void SetTextToDisplay(string textToDisplay);

        /// <summary>
        /// Instantly finish the display animation so all the text is displayed.
        /// </summary>
        void DisplayAllTextNow();

        /// <summary>
        /// Returns if the animation has reached the last character to display.
        /// </summary>
        bool HasAnimationReachedLastCharacter();

    }
}