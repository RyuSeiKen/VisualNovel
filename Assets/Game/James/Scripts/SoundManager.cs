using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour 
{
	[Header("Ink Config")]
	public string introMusicName;
	public string choiceMusicName;
	public string sewersMusicName;
	public string forestMusicName;
	public string desertMusicName;
	public string volcanoMusicName;
	public string blacksmithMusicName;
	public string tavernMusicName;
	public string churchMusicName;
	public string battleMusicName;
	public string victoryMusicName;
	public string levelUpMusicName;
	public string bossMusicName;
	public string dragonMusicName;


	[Space(32)]

	public FMODUnity.StudioEventEmitter fmodEmiter_Intro;
	public FMODUnity.StudioEventEmitter fmodEmiter_Choice;
	public FMODUnity.StudioEventEmitter fmodEmiter_Sewers;
	public FMODUnity.StudioEventEmitter fmodEmiter_Forest;
	public FMODUnity.StudioEventEmitter fmodEmiter_Desert;
	public FMODUnity.StudioEventEmitter fmodEmiter_Volcano;
	public FMODUnity.StudioEventEmitter fmodEmiter_Blacksmith;
	public FMODUnity.StudioEventEmitter fmodEmiter_Tavern;
	public FMODUnity.StudioEventEmitter fmodEmiter_Church;
	public FMODUnity.StudioEventEmitter fmodEmiter_Battle;
	public FMODUnity.StudioEventEmitter fmodEmiter_Victory;
	public FMODUnity.StudioEventEmitter fmodEmiter_LevelUp;
	public FMODUnity.StudioEventEmitter fmodEmiter_Boss;
	public FMODUnity.StudioEventEmitter fmodEmiter_Dragon;
	private FMODUnity.StudioEventEmitter currentMusic;

	private void Start () 
	{
		James.NarrativeManager.eSwitchMusic += SwitchMusic;
		PlayMusic(fmodEmiter_Intro);
	}

	private void OnDestroy () 
	{
		James.NarrativeManager.eSwitchMusic -= SwitchMusic;
	}
	
	private void SwitchMusic(string musicName)
	{
		if(musicName == choiceMusicName)
		{
			PlayMusic(fmodEmiter_Choice);
		}
		if(musicName == sewersMusicName)
		{
			PlayMusic(fmodEmiter_Sewers);
		}
		if(musicName == forestMusicName)
		{
			PlayMusic(fmodEmiter_Forest);
		}
		if(musicName == desertMusicName)
		{
			PlayMusic(fmodEmiter_Desert);
		}
		if(musicName == volcanoMusicName)
		{
			PlayMusic(fmodEmiter_Volcano);
		}
		if(musicName == blacksmithMusicName)
		{
			PlayMusic(fmodEmiter_Blacksmith);
		}
		if(musicName == tavernMusicName)
		{
			PlayMusic(fmodEmiter_Tavern);
		}
		if(musicName == churchMusicName)
		{
			PlayMusic(fmodEmiter_Church);
		}
		if(musicName == battleMusicName)
		{
			PlayMusic(fmodEmiter_Battle);
		}
		if(musicName == victoryMusicName)
		{
			PlayMusic(fmodEmiter_Victory);
		}
		if(musicName == levelUpMusicName)
		{
			PlayMusic(fmodEmiter_LevelUp);
		}
		if(musicName == bossMusicName)
		{
			PlayMusic(fmodEmiter_Boss);
		}
		if(musicName == dragonMusicName)
		{
			PlayMusic(fmodEmiter_Dragon);
		}

	}

	public void PlayMusic(FMODUnity.StudioEventEmitter fmodMusicEmitter){
		if(currentMusic != null){
			currentMusic.Stop();
		}
		fmodMusicEmitter.Play();
		currentMusic = fmodMusicEmitter;
	}
}