using UnityEngine;

[RequireComponent(typeof(TriggerBehavior))]
public class TriggeredEvent : MonoBehaviour
{
	private TriggerBehavior triggerBehavior;
	
	// To set the triggering object
	public bool TriggeredByPlayer = true;
	private GameObject player;
	public GameObject triggeredByObject;
	public bool disableEventOnUse = true;

	// Enum for event type selection and controlling editor GUI drawing
	public enum EventType { Activate, Animation, Audio, Destroy, Music, Particle, EventDisabled};
	public EventType eventType;

	// For triggering object activation
	public bool willActivate = true;
	public GameObject activationObject;
	public float activationDelay = 0;
	private bool doActivation = false;

	// For triggering an animator
	public Animator anim;
	public string animTrigger;

	// For triggering audio clips
	public enum AudioType { Simple, Randomized }
	public AudioType audioType;
	public AudioClip audioClip;
	public AudioClip[] audioClips;
	public float clipCount = 1;
	public float audioVolume = 100, audioPitch = 1;
	public float audioVolumeMin = 0, audioVolumeMax = 100;
	public float audioPitchMin = 0, audioPitchMax = 2;

	// For triggering music clips
	public AudioClip musicClip;
	public bool loopMusic = false;
	public float fadeMin = 0, fadeMax = 5;
	public float musicFadeInTime = 0;
	public float musicFadeOutTime = 0;
	public float musicVolume = 100, musicPitch = 1;
	public float musicVolumeMin = 0, musicVolumeMax = 100;
	public float musicPitchMin = 0, musicPitchMax = 2;


	// For triggering the destruction of an object
	public GameObject DestructableObject;
	public float DestructionDelay = 0;

	// For triggering a particle system
	public ParticleSystem particle;

	private void Start ()
	{
		triggerBehavior = GetComponent<TriggerBehavior>();
	}

	private void Update()
	{
		if (triggerBehavior.triggerActivated)
			TriggeredEventHandler();

		if (doActivation)
			ActivationTriggered();
	}

	private void TriggeredEventHandler()
	{
		switch(eventType)
		{
			case EventType.Activate:
				doActivation = true;
				break;
			case EventType.Animation:
				AnimationTriggered();
				break;
			case EventType.Audio:
				AudioTriggered();
				break;
			case EventType.Music:
				MusicTriggered();
				break;
			case EventType.Particle:
				ParticleTriggered();
				break;
			case EventType.Destroy:
				DestroyTriggered();
				break;
			case EventType.EventDisabled:
				break;
		}
	}

	private void ActivationTriggered()
	{
		// Check if the triggered event is set to enable or disable and take the appropriate action
		if (activationDelay <= 0)
		{
			if (willActivate)
			{
				activationObject.SetActive(true);
				doActivation = false;
				DisableOnUse();  // Automatic disable on use for this triggered event
			}
			else
			{
				activationObject.SetActive(false);
				doActivation = false;
				DisableOnUse();  // Automatic disable on use for this triggered event
			}
		}
		else
		{
			activationDelay -= Time.deltaTime;
		}
	}

	private void AnimationTriggered()
	{
		anim.SetTrigger(animTrigger);

		if (disableEventOnUse)
			DisableOnUse();
	}

	private void AudioTriggered()
	{
		// TODO: Work on a better audio source selection method, so the audio source doesn't need to be on the trigger
		AudioSource aSource = this.GetComponent<AudioSource>();

		// Check which audio type is selected, set the audio source and clip, then play the clip
		switch (audioType)
		{
			case AudioType.Simple:
				aSource.volume = audioVolume / 100;
				aSource.pitch = audioPitch;
				aSource.PlayOneShot(audioClip);
				break;
			case AudioType.Randomized:
				// TODO: Randomize audioClip selection, volume and pitch
                // TODO: Display audioClip list and volume/pitch ranges in editor
				aSource.volume = audioVolume / 100;
				aSource.pitch = audioPitch;
				aSource.PlayOneShot(audioClips[0]);
				break;
		}

		if (disableEventOnUse)
			DisableOnUse();
	}

	private void DestroyTriggered()
	{
		Object.Destroy(DestructableObject, DestructionDelay);
		DisableOnUse();  // Automatic disable on use for this triggered event
	}

	private void MusicTriggered()
	{
		// TODO: Add music functionality

		if (disableEventOnUse)
			DisableOnUse();
	}

	private void ParticleTriggered()
	{
		Instantiate(particle, this.transform.position, this.transform.rotation);

		if (disableEventOnUse)
			DisableOnUse();
	}

	private void DisableOnUse()
	{
		eventType = EventType.EventDisabled;
	}
}
