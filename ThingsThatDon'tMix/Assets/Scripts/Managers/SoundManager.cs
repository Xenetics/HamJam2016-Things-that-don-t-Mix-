using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance = null;
    public static SoundManager Instance { get { return instance; } }

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }
    /// <summary> Types of sound that can be called </summary>
    public enum SoundType { Music, Ambient, SFX, Voice, System }
	/// <summary> States the music can be in </summary>
	public enum MusicState { Menu, Game, GameFast }
	/// <summary> Current state of the music </summary>
	private MusicState m_CurMusicState = MusicState.Menu;
	/// <summary> Next state of the music </summary>
	private MusicState m_NextMusicState = MusicState.Menu;
    //[Header("Sound & Music Lists")]
	///// <summary> These will contain all level specific tracks </summary>
	//public List<Theme> LevelThemes = new List<Theme>();
	///// <summary> The current musical theme </summary>
	//[HideInInspector]
	//public Theme theme;
    /// <summary> List of musics in the game </summary>
    [SerializeField]
    private List<AudioClip> m_Music_List = new List<AudioClip>();
	/// <summary> list of ambient sounds in the game </summary>
	[SerializeField]
	private List<AudioClip> m_Ambient_List = new List<AudioClip>();
    /// <summary> list of Sound FX in the game </summary>
    [SerializeField]
    private List<AudioClip> m_SFX_List = new List<AudioClip>();
    /// <summary> list of voice clips for the game </summary>
    [SerializeField]
    private List<AudioClip> m_Voice_List = new List<AudioClip>();
    /// <summary> list of system sounds </summary>
    [SerializeField]
    private List<AudioClip> m_System_List = new List<AudioClip>();
    [Header("Audio Sources")]
    /// <summary> Music audio source </summary>
	public AudioSource Music_Source;
	/// <summary> Ambient audio source </summary>
	public AudioSource Ambient_Source;
    /// <summary> SFX audio source </summary>
	public AudioSource SFX_Source;
    /// <summary> Voice audio source </summary>
	public AudioSource Voice_Source;
    /// <summary> System audio source </summary>
	public AudioSource System_Source;
    /// <summary> Mute Boolean </summary>
    [HideInInspector]
	public bool Mute;
    /// <summary> Master volume amount </summary>
    private float m_MusicVolume;
    /// <summary> Time it takes for music to fad in and out </summary>
    [SerializeField]
	private float m_FadeTime = 0.5f;
	/// <summary> The Timer used to account for the fade </summary>
	private float m_Timer;
	/// <summary> Gate used for transitioning in music </summary>
	private bool m_TransitionIn = false;
	/// <summary> Gate used for transitioning out music </summary>
	private bool m_TransitionOut = false;
    /// <summary> is music speeding up </summary>
    private bool m_SpeedUp = false;
    /// <summary> MAx pitch of music below the line </summary>
    [SerializeField]
    private float m_MaxPitch = 1.5f;
    /// <summary> Speed at which the music speeds up </summary>
    [SerializeField]
    private float m_SpeedUpSpeed = 0.1f;

    void Update()
	{
        HandleTransition();

        HandleMusicState();
    }

    /// <summary> Finds sound and plays it based on type and string identifier </summary>
    /// <param name="type"> Type of soundmixer to use </param>
    /// <param name="sound"> Name of sound </param>
    public void PlaySound (SoundType type, string sound)
    {
        switch (type)
        {
        case SoundType.Music:
            Music_Source.clip = FindSound(m_Music_List, sound);
			if (!Mute)
			{
				Music_Source.Play();
			}
			break;
		case SoundType.Ambient:
			Ambient_Source.clip = FindSound(m_Ambient_List, sound);
			if (!Mute)
			{
				Ambient_Source.Play();
			}
			break;
        case SoundType.SFX:
			if (!Mute)
			{
				SFX_Source.PlayOneShot(FindSound(m_SFX_List, sound));
			}	
			break;
        case SoundType.Voice:
			if (!Mute)
			{
				Voice_Source.PlayOneShot(FindSound(m_Voice_List, sound));
			}
			break;
        case SoundType.System:
			if (!Mute)
			{
            	System_Source.PlayOneShot(FindSound(m_System_List, sound));
			}
            break;
        }
	}

	/// <summary> Finds sound and plays it based on type and give sound file </summary>
	/// <param name="type"> Type of soundmixer to use </param>
	/// <param name="sound"> an audio clip to play </param>
	public void PlaySound (SoundType type, AudioClip sound)
	{
		switch (type)
		{
		case SoundType.Music:
			Music_Source.clip = sound;
			if (!Mute)
			{
				Music_Source.Play();
			}
			break;
		case SoundType.Ambient:
            Ambient_Source.clip = sound;
			if (!Mute)
			{
				Ambient_Source.Play();
			}
            break;
		case SoundType.SFX:
			if (!Mute)
			{
				SFX_Source.PlayOneShot(sound);
			}
			break;
		case SoundType.Voice:
			if (!Mute)
			{
				Voice_Source.PlayOneShot(sound);
			}
			break;
		case SoundType.System:
			if (!Mute)
			{
				System_Source.PlayOneShot(sound);
			}
			break;
		}
	}

    /// <summary> Finds sound and plays it based on type and string identifier </summary>
    /// <param name="type"> Type of soundmixer to use </param>
    /// <param name="sound"> Name of sound </param>
    /// <param name="pos"> Position of sound </param>>
    public void PlaySound(SoundType type, string sound, float volume)
    {
        switch (type)
        {
            case SoundType.Music:
                Music_Source.clip = FindSound(m_Music_List, sound);
                if (!Mute)
                {
                    Music_Source.Play();
                }
                break;
            case SoundType.Ambient:
                Ambient_Source.clip = FindSound(m_Ambient_List, sound);
                if (!Mute)
                {
                    Ambient_Source.Play();
                }
                break;
            case SoundType.SFX:
                if (!Mute)
                {
                    SFX_Source.PlayOneShot(FindSound(m_SFX_List, sound), volume);
                }
                break;
            case SoundType.Voice:
                if (!Mute)
                {
                    Voice_Source.PlayOneShot(FindSound(m_Voice_List, sound));
                }
                break;
            case SoundType.System:
                if (!Mute)
                {
                    System_Source.PlayOneShot(FindSound(m_System_List, sound));
                }
                break;
        }
    }

    /// <summary> Finds all sounds strting with the string and plays a random one </summary>
    /// <param name="type"> Type of sound we are playing </param>
    /// <param name="sound"> String that we will find all of </param>
    public void PlaySoundRandom (SoundType type, string sound)
	{
		if (!Mute)
		{
			List<AudioClip> clips = new List<AudioClip>();
			int rando;
			switch (type)
			{
			case SoundType.SFX:
				foreach(AudioClip clip in m_SFX_List)
				{
					bool match = true;
					if(!clip.name.Contains(sound))
					{
						match = false;
					}
					
					if(match)
					{
						clips.Add (clip);
					}
				}
				rando = Random.Range(0, clips.Count);
				SFX_Source.PlayOneShot(clips[rando]);
				break;
			case SoundType.Voice:
				foreach(AudioClip clip in m_Voice_List)
				{
					bool match = true;
					if(!clip.name.Contains(sound))
					{
						match = false;
					}
					
					if(match)
					{
						clips.Add (clip);
					}
				}
				rando = Random.Range(0, clips.Count);
				System_Source.PlayOneShot(clips[rando]);
				break;
			case SoundType.System:
				foreach(AudioClip clip in m_System_List)
				{
					bool match = true;
					if(!clip.name.Contains(sound))
					{
						match = false;
					}

					if(match)
					{
						clips.Add (clip);
					}
				}
				rando = Random.Range(0, clips.Count);
				System_Source.PlayOneShot(clips[rando]);
				break;
			}
		}
	}

    /// <summary> Searches the lsits for a specific sound </summary>
    /// <param name="list"> List we are searching </param>
    /// <param name="sound"> Sound we are searching for </param>
    /// <returns>  </returns>
    private AudioClip FindSound(List<AudioClip> list, string sound)
    {
        AudioClip clip = new AudioClip();
        foreach(AudioClip aClip in list)
        {
			if(aClip.name == sound)
            {
				clip = aClip;
                break;
            }
        }
        return clip;
    }

	/// <summary> Set the state of the Music </summary>
	/// <param name="newState"> Accepts a Music State and changes current state to match </param>
	public void SetMusicState(MusicState newState)
	{
		m_NextMusicState = newState;
		OnMusicStateExit();
	}

	/// <summary> Transitions the current music into the new one </summary>
	/// <param name="newState">New state.</param>
	private void TransitionMusicState()
	{
		m_CurMusicState = m_NextMusicState;
		OnMusicStateEnter();
	}
	
	/// <summary> Is called every frame and handles per state calls </summary>
	private void HandleMusicState()
	{
		switch (m_CurMusicState)
		{
		    case MusicState.Menu:
			
			    break;
		    case MusicState.Game:

                break;
			    // More down here
		}
	}
	
	/// <summary> Is called when state is entered </summary>
	private void OnMusicStateEnter()
	{
		switch (m_CurMusicState)
		{
		    case MusicState.Menu:

			    break;
		    case MusicState.Game:
			    
			    break;
		    case MusicState.GameFast:
                m_SpeedUp = true;

			    break;
			    // More down here
		}
	}
	
	/// <summary> Is called when state is exited </summary>
	private void OnMusicStateExit()
	{
		switch (m_CurMusicState)
		{
		    case MusicState.Menu:

			    break;
            case MusicState.Game:
                m_TransitionOut = true;
                break;
            case MusicState.GameFast:
                m_SpeedUp = false;
                Music_Source.pitch = 1;
                m_TransitionOut = true;
			    break;
			    // More down here
		}
	}

    /// <summary> handles the transition between musics </summary>
    private void HandleTransition()
    {
        if (m_TransitionOut)
        {
            m_Timer -= Time.unscaledDeltaTime;
            //Music_Source.volume = m_MusicVolume * (m_Timer / m_FadeTime);
            if (m_Timer <= 0)
            {
                m_TransitionOut = false;
                m_Timer = m_FadeTime;
                TransitionMusicState();
            }
        }

        if (m_TransitionIn)
        {
            m_Timer -= Time.unscaledDeltaTime;
            Music_Source.volume = m_MusicVolume - (m_MusicVolume * (m_Timer / m_FadeTime));
            if (m_Timer <= 0)
            {
                m_TransitionIn = false;
                m_Timer = m_FadeTime;
            }
        }
    }

    /// <summary> Stops the music for when you toggle mute </summary>
    public void MuteSound()
	{
		Mute = !Mute;
		if(Mute)
		{
			Music_Source.Pause();
			Ambient_Source.Pause();
		}
		else
		{
			Music_Source.Play();
			Ambient_Source.Play();
		}
	}

	///// <summary> Saves the sound settings </summary>
	//public void SaveSoundOptions()
	//{
	//	GameManager.saveData.muteSound = Mute;
	//	GameManager.saveData.MusicVolume = Music_Source.volume;
	//	GameManager.saveData.SFXVolume = SFX_Source.volume;
	//	GameSaveSystem.Instance.SaveGame();
	//}

  //  /// <summary> Sets the Sound to proper amount from savedata </summary>
  //  public void LoadData()
  //  {
  //      Mute = GameManager.saveData.muteSound;
  //      Music_Source.volume = GameManager.saveData.MusicVolume;
		//Ambient_Source.volume = GameManager.saveData.MusicVolume;
  //      SFX_Source.volume = GameManager.saveData.SFXVolume;
  //      Voice_Source.volume = GameManager.saveData.SFXVolume;
  //      System_Source.volume = GameManager.saveData.SFXVolume;
		//if(Mute)
		//{
		//	Music_Source.Pause();
		//	Ambient_Source.Pause();
		//}
		//else
		//{
		//	if(!Music_Source.isPlaying)
		//	{
		//		Music_Source.Play();
		//		Ambient_Source.Play();
		//	}
		//}
  //  }
}
