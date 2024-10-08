using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Arcade.Gameplay
{
	public class ArcAudioManager : MonoBehaviour
	{
		public static ArcAudioManager Instance { get; private set; }
		private void Awake()
		{
			Instance = this;
		}

		public AudioSource Source;
		public AudioMixerGroup PitchShiftMixerGroup;
		public AudioMixerGroup DoublePitchShiftMixerGroup;
		public Text PlayBackSpeedButtonText;

		public float Timing
		{
			get
			{
				return Source.time;
			}
			set
			{
				Source.time = value;
			}
		}
		public AudioClip Clip
		{
			get
			{
				return Source.clip;
			}
			set
			{
				Source.clip = value;
			}
		}

		private float playBackSpeed = 1;

		public float PlayBackSpeed
		{
			get
			{
				return playBackSpeed;
			}
			private set
			{
				Source.pitch = value;
				if (value == 1)
				{
					Source.outputAudioMixerGroup = null;
				}
				else if (value >= 0.5f)
				{
					Source.outputAudioMixerGroup = PitchShiftMixerGroup;
					PitchShiftMixerGroup.audioMixer.SetFloat("pitchShift", 1f / value);
				}
				else
				{
					Source.outputAudioMixerGroup = DoublePitchShiftMixerGroup;
					PitchShiftMixerGroup.audioMixer.SetFloat("pitchShift", 2f);
					PitchShiftMixerGroup.audioMixer.SetFloat("pitchShift2", 0.5f / value);
				}
				playBackSpeed = value;
			}
		}

		public void Load(AudioClip clip)
		{
			Clip = clip;
			Clip.LoadAudioData();
		}
		public void Play()
		{
			Source.Play();
		}
		public void Pause()
		{
			Source.Pause();
		}

		public void NextPlaybackSpeed()
		{
			if (PlayBackSpeedButtonText.text == "100%")
			{
				PlayBackSpeed = 0.75f;
				PlayBackSpeedButtonText.text = "75%";
			}
			else if (PlayBackSpeedButtonText.text == "75%")
			{
				PlayBackSpeed = 0.5f;
				PlayBackSpeedButtonText.text = "50%";
			}
			else if (PlayBackSpeedButtonText.text == "50%")
			{
				PlayBackSpeed = 0.25f;
				PlayBackSpeedButtonText.text = "25%";
			}
			else
			{
				PlayBackSpeed = 1f;
				PlayBackSpeedButtonText.text = "100%";
			}
		}
	}
}
