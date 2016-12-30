using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour 
{
	public SubjectButton mathButton;
	public SubjectButton geographyButton;

	KnownSubject math;
	KnownSubject geography;

	public List<KnownSubject> subjectList = new List<KnownSubject>();

	void LoadSubjects()
	{
		foreach(SchoolSubjects subject in System.Enum.GetValues(typeof(SchoolSubjects)))
		{
			Debug.Log(subject.ToString());
		}
	}

	void Start()
	{
		math = new KnownSubject(SchoolSubjects.Math, 1, mathButton);
		subjectList.Add(math);
		geography = new KnownSubject(SchoolSubjects.Geography, 1, geographyButton);
		subjectList.Add(geography);
		LoadSubjects();
	}

	void Update()
	{
		foreach(KnownSubject ks in subjectList)
		{
			if(ks.subjectLevel >= 5)
			{
				ks.subjectButton.maxed = true;
			}
			else if(ks.subjectLevel == 4)
			{
				ks.subjectButton.nearlyMaxed = true;
			}
		}
	}

	public KnownSubject FindKnownSubject(SchoolSubjects subject)
	{
		foreach(KnownSubject ks in subjectList)
		{
			if(ks.subject == subject)
			{
				return ks;
			}
		}
		return subjectList[0];
	}
}

public class KnownSubject
{
	public SchoolSubjects subject;
	public int subjectLevel;
	public SubjectButton subjectButton;
	public KnownSubject(SchoolSubjects subject, int level, SubjectButton button)
	{
		this.subject = subject;
		subjectLevel = level;
		subjectButton = button;
	}

	public bool IsMaxed()
	{
		return subjectLevel >= 5;
	}

	public bool IsNearlyMaxed()
	{
		return subjectLevel == 4;
	}
}