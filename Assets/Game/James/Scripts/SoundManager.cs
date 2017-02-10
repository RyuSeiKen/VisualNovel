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
	public string cureMusicName;
	public string blessMusicName;
	public string nightMusicName;
	public string innMusicName;
	public string battleMusicName;
	public string victoryMusicName;
	public string levelUpMusicName;
	public string questMusicName;
	public string bossMusicName;
	public string triumphMusicName;
	public string accomplishedMusicName;
	public string dragonMusicName;
	public string theEndMusicName;
	public string deathMusicName;

	public string playerhitEffectName;
	public string enemyhitEffectName;


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
	public FMODUnity.StudioEventEmitter fmodEmiter_Cure;
	public FMODUnity.StudioEventEmitter fmodEmiter_Bless;
	public FMODUnity.StudioEventEmitter fmodEmiter_Night;
	public FMODUnity.StudioEventEmitter fmodEmiter_Inn;
	public FMODUnity.StudioEventEmitter fmodEmiter_Battle;
	public FMODUnity.StudioEventEmitter fmodEmiter_Victory;
	public FMODUnity.StudioEventEmitter fmodEmiter_LevelUp;
	public FMODUnity.StudioEventEmitter fmodEmiter_Quest;
	public FMODUnity.StudioEventEmitter fmodEmiter_Boss;
	public FMODUnity.StudioEventEmitter fmodEmiter_Triumph;
	public FMODUnity.StudioEventEmitter fmodEmiter_Accomplished;
	public FMODUnity.StudioEventEmitter fmodEmiter_Dragon;
	public FMODUnity.StudioEventEmitter fmodEmiter_TheEnd;
	public FMODUnity.StudioEventEmitter fmodEmiter_Death;

	public FMODUnity.StudioEventEmitter fmodEmiter_PlayerHit;
	public FMODUnity.StudioEventEmitter fmodEmiter_EnemyHit;

	private FMODUnity.StudioEventEmitter currentMusic;

	private void Start () 
	{
		James.NarrativeManager.eSwitchMusic += SwitchMusic;
		James.NarrativeManager.ePlayEffect += PlayEffect;

	}

	private void OnDestroy () 
	{
		James.NarrativeManager.eSwitchMusic -= SwitchMusic;
		James.NarrativeManager.ePlayEffect -= PlayEffect;
	}

	private void PlayEffect(string effectName)
	{
		if(effectName == playerhitEffectName)
		{
			fmodEmiter_PlayerHit.Play();
		}
		if(effectName == enemyhitEffectName)
		{
			fmodEmiter_EnemyHit.Play();
		}
	}

	private void SwitchMusic(string musicName)
	{
		if(musicName == introMusicName)
		{
			PlayMusic(fmodEmiter_Intro);
		}
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
		if(musicName == cureMusicName)
		{
			PlayMusic(fmodEmiter_Cure);
		}
		if(musicName == blessMusicName)
		{
			PlayMusic(fmodEmiter_Bless);
		}
		if(musicName == nightMusicName)
		{
			PlayMusic(fmodEmiter_Night);
		}
		if(musicName == innMusicName)
		{
			PlayMusic(fmodEmiter_Inn);
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
		if(musicName == questMusicName)
		{
			PlayMusic(fmodEmiter_Quest);
		}
		if(musicName == bossMusicName)
		{
			PlayMusic(fmodEmiter_Boss);
		}
		if(musicName == triumphMusicName)
		{
			PlayMusic(fmodEmiter_Triumph);
		}
		if(musicName == accomplishedMusicName)
		{
			PlayMusic(fmodEmiter_Accomplished);
		}
		if(musicName == dragonMusicName)
		{
			PlayMusic(fmodEmiter_Dragon);
		}
		if(musicName == theEndMusicName)
		{
			PlayMusic(fmodEmiter_TheEnd);
		}
		if(musicName == deathMusicName)
		{
			PlayMusic(fmodEmiter_Death);
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