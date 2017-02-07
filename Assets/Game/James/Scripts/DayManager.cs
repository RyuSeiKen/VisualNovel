using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace James
{
	public class DayManager : MonoBehaviour
	{
		public delegate void DayEventType(); 
		public static event DayEventType OnMorning;
		public static event DayEventType OnAfternoon;
		public static event DayEventType OnEvening;

		public DayNumber currentDay = new DayNumber();
		public DayTime currentTime = new DayTime();

		public DayPlanifier planifier;

		void Start()
		{
			planifier = FindObjectOfType<DayPlanifier>();
		}

		public void RemoveChoice()
		{
			DayManager.OnMorning = null;
			DayManager.OnAfternoon = null;
			DayManager.OnEvening = null;

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
		}


		public void LaunchDay() 
		{
			if(OnMorning != null) 
			{ 
				OnMorning(); 
				OnMorning = null;
			}

			if(OnAfternoon != null)
			{
				OnAfternoon(); 
				OnAfternoon = null;
			} 

			if(OnEvening != null)
			{
				OnEvening(); 
				OnEvening = null;
			} 

			planifier.morningChosen = false;
			planifier.afternoonChosen = false;
			planifier.eveningChosen = false;

			planifier.button1.color = Color.black;
			if(planifier.button2 != null)
			{
				planifier.button2.color = Color.black;
			}
			if(planifier.button3 != null)
			{
				planifier.button3.color = Color.black;
			}
		} 
	}
}