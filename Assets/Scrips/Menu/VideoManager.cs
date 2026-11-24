using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Scrips.Menu
{
	public class VideoManager : MonoBehaviour
	{
		public VideoPlayer videoPlayer;
		//[SerializeField] private VideoPlayer videoPlayer2;
		public List<VideoClip> videoClips;

		[SerializeField] private GameObject videoScreen;
		[SerializeField] private AudioSource gameMusic;

		public void Awake()
		{
			videoScreen.SetActive(false);
		}

		public void PlayVideo()
		{
			Time.timeScale = 0.0f;
			gameMusic.Pause();

			videoPlayer.clip = videoClips[0];
			videoPlayer.isLooping = false;

			videoScreen.SetActive(true);
			videoPlayer.Play();
		}
	}
}