using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Feature.Gameplay 
{
	public class DayManager : MonoBehaviour
	{
		public delegate void DayEventType(); 
		public static event DayEventType OnMorning;
		public static event DayEventType OnAfternoon;

		public SchoolSubjects selectionM;
		public SchoolSubjects selectionA;

		public DayNumber currentDay = new DayNumber();
		public DayTime currentTime = new DayTime();

		public DayPlanifier planifier;

		void Start()
		{
			planifier = FindObjectOfType<DayPlanifier>();
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
			planifier.selection1 = SchoolSubjects.None;
			planifier.button1.color = Color.white;
			planifier.selection2 = SchoolSubjects.None;
			if(planifier.button2 != null)
			{
				planifier.button2.color = Color.white;
			}
		} 
	}
}