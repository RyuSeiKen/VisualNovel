﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace James 
{


	/// <summary>
	/// The data class representing choices the player made trough the daily subject choice event.
	/// </summary>
	public class DailyChoices {

		/// <summary>
		/// The subject the player chosed to study in the morning.
		/// </summary>
		public Feature.Gameplay.Data.Subject subjectChosenThisMorning = null;

		/// <summary>
		/// The subject the player chosed to study in the afternoon.
		/// </summary>
		public Feature.Gameplay.Data.Subject subjectChosenThisAfternoon = null;

		/// <summary>
		/// The subject the player chosed to study in the evening.
		/// </summary>
		public Feature.Gameplay.Data.Subject subjectChosenThisEvening = null;

		/// <summary>
		/// Return if both subjects were chosen.
		/// </summary>
		/// <returns></returns>
		public bool IsValid() 
		{
			return
				subjectChosenThisMorning != null &&
				subjectChosenThisAfternoon != null &&
				subjectChosenThisEvening != null;
		}
	}


	/// <summary>
	/// Handler catching the choices made by the player trough the daily subjects choice event.
	/// </summary>
	/// <param name="dailyChoices">Choices the player made trough the daily subject choice event.</param>
	public delegate void DailyChoicesValidatedHandler(DailyChoices dailyChoices);

	public class DayPlanifier : MonoBehaviour
	{
		public Image button1;
		public Image button2;
		public Image button3;

		public bool morningChosen;
		public bool afternoonChosen;
		public bool eveningChosen;

		StatManager sManager;
		DayManager dManager;

		DailyChoices dailyChoicesMade;

		public GameObject container;
		public Button validate;

		private void Awake(){
			container.SetActive(false);
		}

		void Start()
		{
			sManager = FindObjectOfType<StatManager>();
			dManager = FindObjectOfType<DayManager>();

			SubjectChoiceButton.eSubjectChoiceButtonWasClicked += OnButtonClicked;
		}

		void OnDestroy()
		{
			SubjectChoiceButton.eSubjectChoiceButtonWasClicked -= OnButtonClicked;
		}

		void Update()
		{
			if(Input.GetKeyDown(KeyCode.Backspace))
			{
				dManager.RemoveChoice();
			}
		}

		public void OnButtonClicked(SubjectChoiceButton subject) 
		{ 
			switch(subject.time)
			{
			case DayTime.Morning:
				dailyChoicesMade.subjectChosenThisMorning = subject.subjectData;
				break;
			case DayTime.Afternoon:
				dailyChoicesMade.subjectChosenThisAfternoon = subject.subjectData;
				break;
			case DayTime.Evening:
				dailyChoicesMade.subjectChosenThisEvening = subject.subjectData;
				break;
			}

			if(subject.time == DayTime.Morning)
			{
				if(button1 != null)
				{
					button1.color = Color.black;
				}
				morningChosen = true;
				button1 = subject.transform.GetComponent<Image>();
				button1.color = Color.red;
			}
			else if(subject.time == DayTime.Afternoon)
			{
				if(button2 != null)
				{
					button2.color = Color.black;
				}
				afternoonChosen = true;
				button2 = subject.transform.GetComponent<Image>();
				button2.color = Color.blue;
			}
			else if(subject.time == DayTime.Evening)
			{
				if(button3 != null)
				{
					button3.color = Color.black;
				}
				eveningChosen = true;
				button3 = subject.transform.GetComponent<Image>();
				button3.color = Color.green;
			}

			Debug.Log(dailyChoicesMade.IsValid());
			if(dailyChoicesMade.IsValid())
			{
				Debug.Log("lol");
				validate.interactable = true;
			}
				
		}

//		void RemoveChoice()
//		{
//			DayManager.OnMorning = null;
//			DayManager.OnAfternoon = null;
//
//			if(selection2 != SchoolSubjects.None)
//			{
//				if(selection1 == selection2)
//				{
//					button2.color = Color.red;
//				}
//				else
//				{
//					button2.color = Color.white;
//				}
//				DayManager.OnAfternoon -= ImproveSubject;
//				selection2 = SchoolSubjects.None;
//				return;
//			}
//			else if(selection1 != SchoolSubjects.None)
//			{
//				button1.color = Color.white;
//				DayManager.OnMorning -= ImproveSubject;
//				selection1 = SchoolSubjects.None;
//				return;
//			}
//			else
//			{
//				Debug.Log("You have not yet made a single choice!");
		//			}
//		}

		//#region IDailyChoicesDisplay implementation

		James.DailyChoicesValidatedHandler eDailyChoicesValidatedHandler;


		public void Init (James.DailyChoicesValidatedHandler dailyChoicesValidatedHandler)
		{
			this.eDailyChoicesValidatedHandler = dailyChoicesValidatedHandler;
			dailyChoicesMade = new DailyChoices();

			validate.interactable = false;
			container.SetActive(true);
		}

		public void Done()
		{
			eDailyChoicesValidatedHandler(dailyChoicesMade);

			morningChosen = false;
			afternoonChosen = false;
			eveningChosen = false;

			button1.color = Color.black;
			if(button2 != null)
			{
				button2.color = Color.black;
			}
			if(button3 != null)
			{
				button3.color = Color.black;
			}

			container.SetActive(false);
		}

		//#endregion
	}
}