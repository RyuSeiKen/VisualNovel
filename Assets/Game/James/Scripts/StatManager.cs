using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour 
{
	public List<KnownSubject> subjectList = new List<KnownSubject>();

	void LoadSubjects()
	{
		foreach(SchoolSubjects subject in System.Enum.GetValues(typeof(SchoolSubjects)))
		{
			if(subject.ToString() != "None")
			{
				KnownSubject s = new KnownSubject(subject, 1);
				subjectList.Add(s);
			}
		}	
	}

	void Start()
	{
		LoadSubjects();
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

[System.Serializable]
public class KnownSubject
{
	public SchoolSubjects subject;
	public int subjectLevel;

	public KnownSubject(SchoolSubjects subject, int level)
	{
		this.subject = subject;
		subjectLevel = level;
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