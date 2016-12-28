using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayManager : MonoBehaviour 
{
	public delegate void DayEventType(StatManager manager); 
	public static event DayEventType OnMorning;
	public static event DayEventType OnAfternoon;

	public DayNumber currentDay = new DayNumber();
	public DayTime currentTime = new DayTime();

	public StatManager statManager;
	public DayPlanifier planifier;

	void Start()
	{
		statManager = FindObjectOfType<StatManager>();
		planifier = FindObjectOfType<DayPlanifier>();
	}

	public void LaunchDay() 
	{
		if(OnMorning != null) 
		{ 
			OnMorning(statManager); 
			OnMorning = null;
		}

		if(OnAfternoon != null)
		{
			OnAfternoon(statManager); 
			OnAfternoon = null;
		} 
		planifier.selection1 = SchoolSubjects.None;
		planifier.button1.color = Color.white;
		planifier.selection2 = SchoolSubjects.None;
		planifier.button2.color = Color.white;
	} 
}