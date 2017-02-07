using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TextBox : MonoBehaviour , Feature.Narrative.ITextDisplayAnimator
{
	public bool typing;
	public float pause = 0.001f;
	public AudioClip dialogue;
	public AudioSource source;
	public int i = 0;
	Text text;
	string textToDisplay;

	private void Awake(){
		text = GetComponent<Text>();
	}

	public IEnumerator TypeLetters (string currentMessage) 
	{
		typing = true;
		foreach (char letter in currentMessage.ToCharArray()) 
		{
			GetComponent<Text>().text += letter; 
			if(letter != " "[0] && letter != "."[0] && letter != "?"[0] && letter != "!"[0])
			{
				//source.PlayOneShot(dialogue,1);
			}
			yield return new WaitForSeconds(pause);
		}
		typing = false;
	}

	#region ITextDisplayAnimator implementation

	void Feature.Narrative.ITextDisplayAnimator.SetTextToDisplay (string textToDisplay)
	{
		StopAllCoroutines();
		text.text = string.Empty;
		StartCoroutine(TypeLetters(this.textToDisplay = textToDisplay));
	}

	void Feature.Narrative.ITextDisplayAnimator.DisplayAllTextNow ()
	{
		StopAllCoroutines();
		typing = false;
		text.text = textToDisplay; 
	}

	bool Feature.Narrative.ITextDisplayAnimator.HasAnimationReachedLastCharacter ()
	{
		return !typing;
	}

	#endregion
}