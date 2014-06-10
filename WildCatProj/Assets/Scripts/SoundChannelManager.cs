using UnityEngine;
using System.Collections;

public class SoundChannelManager {

	private static SoundChannelManager instance = null;
	private AudioSource[] audioSources;
	private const int channelNbr = 8;
		
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

	public void playMusic(bool loop = true) {
		throw new System.NotImplementedException ();
	}

	public void setMusicVolume (float s) {
		throw new System.NotImplementedException ();
	}
}