using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.Gameplay {

    /// <summary>
    /// The data class representing choices the player made trough the daily subject choice event.
    /// </summary>
    public class DailyChoices {

        /// <summary>
        /// The subject the player chosed to study in the morning.
        /// </summary>
        public SubjectInstance subjectChosenThisMorning = null;

        /// <summary>
        /// The subject the player chosed to study in the afternoon.
        /// </summary>
        public SubjectInstance subjectChosenThisAfternoon = null;

        /// <summary>
        /// Return if both subjects were chosen.
        /// </summary>
        /// <returns></returns>
        public bool IsValid() {
            return
                subjectChosenThisMorning != null &&
                subjectChosenThisAfternoon != null;
        }
    }

    /// <summary>
    /// Handler catching the choices made by the player trough the daily subjects choice event.
    /// </summary>
    /// <param name="dailyChoices">Choices the player made trough the daily subject choice event.</param>
	public delegate void DailyChoicesValidatedHandler(DailyChoices dailyChoices);

    /// <summary>
    /// The interface defining what methods the narrative manager needs to display the "daily subject lesson choices" event.
    /// </summary>
    public interface IDailyChoicesDisplay {

        /// <summary>
        /// Initialise the daily subjects choices event.
        /// </summary>
        /// <param name="dailyChoicesValidatedHandler">Handler catching the choices made by the player trough this daily subjects choice event.</param>
		void Init(DailyChoicesValidatedHandler dailyChoicesValidatedHandler);

    }
}