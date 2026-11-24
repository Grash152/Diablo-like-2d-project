using System.Linq;
using Scrips.Menu;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scrips.Characters
{
	public class DeathEffect : MonoBehaviour
	{
		[SerializeField] private Volume globalVolume;
		[SerializeField] private GameObject deathOverlay;
		[SerializeField] private VideoManager videoManager;
		[SerializeField] private movment playerPosition;

		private ColorAdjustments _colorAdjustments;
		private Image _deathOverlayChild;

		private float _monoColorAdjustSpeed;
		private float _alphaAdjustSpeed;
		private float _alphaValue;
		private float _timer;
		private bool _overlayDone;
		private bool _isFadeOut;
		private bool _deathStarted;
		private bool _videoStarted;

		private void Awake()
		{
			_overlayDone = false;
			_isFadeOut = false;
			_deathStarted = false;
			_videoStarted = false;
			_timer = 3.5f;
			
			globalVolume.profile.TryGet(out _colorAdjustments);

			_colorAdjustments.saturation.value = 0.0f;

			_alphaAdjustSpeed = 25.0f;
			_monoColorAdjustSpeed = 50.0f;

			_deathOverlayChild = deathOverlay.GetComponentInChildren<Image>();

			_alphaValue = 0.0f;

			Color color = _deathOverlayChild.color;
			color.a = _alphaValue;
			_deathOverlayChild.color = color;

			deathOverlay.SetActive(false);
		}

		private void Update()
		{
			transform.position = playerPosition.transform.position;
			if (!_deathStarted)
			{
				return;
			}

			MakeMonochrome();
			DeathOverlay();
		}
		
		public void StartDeathEffect()
		{
			_deathStarted = true;

			deathOverlay.SetActive(true);
			videoManager.videoPlayer.Play();
		}

		private void MakeMonochrome()
		{
			_colorAdjustments.saturation.value = Mathf.MoveTowards(_colorAdjustments.saturation.value,
					-100.0f, _monoColorAdjustSpeed * Time.unscaledDeltaTime);
		}

		public void RestoreColor()
		{
			_colorAdjustments.saturation.value = 0f;
		}

		private void DeathOverlay()
		{
			deathOverlay.SetActive(true);
			
			if (_colorAdjustments.saturation.value <= -50.0f && !_isFadeOut)
			{
				_alphaValue = Mathf.MoveTowards(_alphaValue, 1.0f, _alphaAdjustSpeed * Time.unscaledDeltaTime);

				if (_alphaValue >= 1.0f)
				{
					_isFadeOut = true;
				}
			}

			if (_isFadeOut)
			{
				_timer -= Time.unscaledDeltaTime;
				
				if (_timer <= 0.0f)
				{
					_alphaValue = Mathf.MoveTowards(_alphaValue, 0.0f, _alphaAdjustSpeed * Time.unscaledDeltaTime);
					
					if (_alphaValue <= 0.05f)
					{
						_overlayDone = true;
					}
				}
			}
			
			if (_overlayDone && !_videoStarted)
			{
				_videoStarted = true;
				videoManager.PlayVideo();
			}
			
			//Debug.Log("Overlay Done" + _overlayDone);
			//Debug.Log("Alpha Value" + _alphaValue);
			
			Color color = _deathOverlayChild.color;
			color.a = _alphaValue;
			_deathOverlayChild.color = color;
		}
	}
}