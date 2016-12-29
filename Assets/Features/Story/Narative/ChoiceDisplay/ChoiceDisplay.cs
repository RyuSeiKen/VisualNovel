using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

namespace Feature.Narative {

    public delegate void OnChooseChoiceHandler(Choice choice);

    public interface IChoiceDisplay {

        /// <summary>
        /// Display a choice to be selected on screen.
        /// </summary>
        /// <param name="choice">The choice to be displayed.</param>
        /// <param name="handler">The handler to call when this choice is selected.</param>
		void DisplayChoice(Choice choice, OnChooseChoiceHandler handler);

        /// <summary>
        /// Instantly remove all displayed choices from screen.
        /// </summary>
        void ClearChoices();

    }
}