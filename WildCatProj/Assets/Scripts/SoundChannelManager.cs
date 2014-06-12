using UnityEngine;
using System.Collections;

public class SoundChannelManager {

	private static SoundChannelManager instance = null;
	private AudioSource[] audioSources;
	private const int channelNbr = 8;
	private AudioSource musicPlayer;
		
	public static SoundChannelManager GetInstance() {
		if (instance == null) {
			instance = new SoundChannelManager();
		}
		return instance;
	}

	private SoundChannelManager() {
		init();
	}

	private void init() {
		audioSources = new AudioSource[channelNbr];
		for (int i = 0; i < channelNbr; i++) {
			GameObject tmp = new GameObject("AudioSource" + (i + 1));
			audioSources[i] = tmp.AddComponent("AudioSource") as AudioSource;
			GameObject.DontDestroyOnLoad(tmp);
		}
	}

	public void PlayClipAtPoint(AudioClip clip, Transform point, float volume = 1f) {
		if (!OptionManager.GetInstance().soundIsMuted){
			for (int i = 0; i < channelNbr; i++) {
				if (audioSources[i] != null && !audioSources[i].isPlaying) {
					audioSources[i].transform.position = point.position;
					audioSources[i].clip = clip;
					audioSources[i].volume = volume;
					audioSources[i].Play();
					return;
				}
			}
		}
	}

	private bool tryGetMusicPlayer() {
		if (!musicPlayer) {
			GameObject tmpObj = GameObject.Find("MusicPlayer");
			if (!tmpObj) {
				Debug.LogWarning("There is no game object Named MusicPlayer in this scene");
				return false;
			} else {
				musicPlayer = tmpObj.GetComponent<AudioSource>();
				if (!musicPlayer) {
					Debug.LogWarning("The game object Named MusicPlayer has no AudioSource");
					return false;
				} else {
					Debug.Log ("Music Player is is set.");
				}
			}
		}
		return true;
	}

	public void playMusic(bool loop = true) {
		if (tryGetMusicPlayer()) {
			musicPlayer.loop = loop;
			musicPlayer.Play();
		}
	}

	public void setMusicVolume (float s) {
		if (tryGetMusicPlayer()) {
			musicPlayer.volume = s;
		}
	}

	public void setSfxVolume (float s)
	{
		for (int i = 0; i < channelNbr; i++) {
			audioSources[i].volume = s;
		}
	}
}