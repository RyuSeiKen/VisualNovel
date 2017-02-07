using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Feature.Gameplay 
{
	public class DayPlanifier : MonoBehaviour 
	{
		public SchoolSubjects selection1;
		public Image button1;
		public SchoolSubjects selection2;
		public Image button2;

		public bool morningChosen;
		public bool afternoonChosen;

		StatManager sManager;
		DayManager dManager;

		void Start()
		{
			sManager = FindObjectOfType<StatManager>();
			dManager = FindObjectOfType<DayManager>();

			SubjectChoiceButton.eSubjectChoiceButtonWasClicked += OnButtonClicked;
		}

		void ImproveSubject(SubjectChoiceButton subject)
		{
			sManager.FindKnownSubject(subject.subject).subjectLevel += 1;
		}

		void Update()
		{
			if(Input.GetKeyDown(KeyCode.Backspace))
			{
//				RemoveChoice();
			}
		}

		public void OnButtonClicked(SubjectChoiceButton subject) 
		{ 
			if(sManager.FindKnownSubject(subject.subject).IsMaxed())
			{
				Debug.Log("Maxed");
				return;
			}
			else if(selection1 == SchoolSubjects.None)
			{
				DayManager.OnMorning += delegate {
					ImproveSubject(subject);
				};
				dManager.selectionM = subject.subject;
//				morningChosen = true;
				selection1 = subject.subject;
				button1 = subject.transform.GetComponent<Image>();
				button1.color = Color.red;
				return;
			}
			else if(sManager.FindKnownSubject(subject.subject).IsNearlyMaxed() && subject.subject == selection1)
			{
				Debug.Log("Nearly Maxed");
				return;
			}
			else if(selection2 == SchoolSubjects.None)
			{
				DayManager.OnAfternoon += delegate {
					ImproveSubject(subject);
				};
//				afternoonChosen = true;
				selection2 = subject.subject;
				button2 = subject.transform.GetComponent<Image>();
				if(selection1 == selection2)
				{
					button2.color = Color.magenta;
				}
				else
				{
					button2.color = Color.blue;
				}
				return;
			}
			else
			{
				Debug.Log("You may only choose two activities for today!");
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
	}
}