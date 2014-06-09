using UnityEngine;
using System.Collections;

public class OptionManager
{
	private static OptionManager instance = null;
	private bool soundMuted = false;
	private bool musicMuted = false;

	public bool musicIsMuted {
		set { 
			musicMuted = value;
			if (musicMuted)
				SoundChannelManager.GetInstance().setMusicVolume(0f);
		}
		get { return musicMuted; }
	}
	public bool soundIsMuted { 
		get { return soundMuted; }
		set { soundMuted = value; }
	}

	public static OptionManager GetInstance() {
		if (instance == null) {
			instance = new OptionManager();
		}
		return instance;
	}

	private OptionManager() {
		init();
	}

	private void init() {

	}
}