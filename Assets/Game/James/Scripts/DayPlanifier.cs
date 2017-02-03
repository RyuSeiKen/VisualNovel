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

		public StatManager manager;

		void Start()
		{
			manager = FindObjectOfType<StatManager>();
		}

		void ImproveSubject1()
		{
			manager.FindKnownSubject(selection1).subjectLevel += 1;
		}

		void ImproveSubject2()
		{
			manager.FindKnownSubject(selection2).subjectLevel += 1;
		}

		void Update()
		{
			if(Input.GetKeyDown(KeyCode.Backspace))
			{
				RemoveChoice();
			}
		}

		public void OnButtonClicked(SubjectChoice subject) 
		{ 
			if(manager.FindKnownSubject(subject.subject).IsMaxed())
			{
				Debug.Log("Maxed");
				return;
			}
			else if(selection1 == SchoolSubjects.None)
			{
				DayManager.OnMorning += ImproveSubject1;
				selection1 = subject.subject;
				button1 = subject.transform.GetComponent<Image>();
				button1.color = Color.red;
				return;
			}
			else if(manager.FindKnownSubject(subject.subject).IsNearlyMaxed() && subject.subject == selection1)
			{
				Debug.Log("Nearly Maxed");
				return;
			}
			else if(selection2 == SchoolSubjects.None)
			{
				DayManager.OnAfternoon += ImproveSubject2;
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

		void RemoveChoice()
		{
			if(selection2 != SchoolSubjects.None)
			{
				if(selection1 == selection2)
				{
					button2.color = Color.red;
				}
				else
				{
					button2.color = Color.white;
				}
				DayManager.OnAfternoon -= ImproveSubject2;
				selection2 = SchoolSubjects.None;
				return;
			}
			else if(selection1 != SchoolSubjects.None)
			{
				button1.color = Color.white;
				DayManager.OnMorning -= ImproveSubject1;
				selection1 = SchoolSubjects.None;
				return;
			}
			else
			{
				Debug.Log("You have not yet made a single choice!");
			}
		}
	}
}