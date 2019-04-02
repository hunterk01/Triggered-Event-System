using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TriggeredEvent))]
public class TriggeredEventEditor : Editor
{
    public Vector2 scrollPosition;

    public override void OnInspectorGUI()
    {
        TriggeredEvent triggeredEvent = (TriggeredEvent)target;

        // Get the event type selection and draw the approprate inspector GUI
        GUILayout.Space(10);
        EditorGUILayout.LabelField("Select Event Type", EditorStyles.boldLabel);
        triggeredEvent.eventType = (TriggeredEvent.EventType)EditorGUILayout.EnumPopup("Event Type", triggeredEvent.eventType);
        GUILayout.Space(10);

        switch (triggeredEvent.eventType)
        {
            case TriggeredEvent.EventType.Activate:
                ShowActivateGUI(triggeredEvent);
                break;
            case TriggeredEvent.EventType.Animation:
                ShowAnimationGUI(triggeredEvent);
                break;
            case TriggeredEvent.EventType.Audio:
                ShowAudioGUI(triggeredEvent);
                break;
            case TriggeredEvent.EventType.Destroy:
                ShowDestroyGUI(triggeredEvent);
                break;
            case TriggeredEvent.EventType.Music:
                ShowMusicGUI(triggeredEvent);
                break;
            case TriggeredEvent.EventType.Particle:
                ShowParticleGUI(triggeredEvent);
                break;
        }
    }

    private void ShowActivateGUI(TriggeredEvent triggeredEvent)
    {
        EditorGUILayout.LabelField("Activate Event Settings", EditorStyles.boldLabel);

        // A toggle for if the event activates or deactivates the selected object
        triggeredEvent.willActivate = EditorGUILayout.Toggle("Will Activate", triggeredEvent.willActivate);

        // Fields for entering the object to activate and a delay time to wait for activation
        triggeredEvent.activationObject = EditorGUILayout.ObjectField("Object To Activate", triggeredEvent.activationObject, typeof(GameObject), true) as GameObject;
        triggeredEvent.activationDelay = EditorGUILayout.FloatField("Activation Delay", triggeredEvent.activationDelay);
    }

    private void ShowAnimationGUI(TriggeredEvent triggeredEvent)
    {
        // A toggle for having the triggered event disable itself upon completion
        triggeredEvent.disableEventOnUse = EditorGUILayout.Toggle("Disable On Use", triggeredEvent.disableEventOnUse);
        GUILayout.Space(5);

        EditorGUILayout.LabelField("Animation Event Settings", EditorStyles.boldLabel);

        // Fields for entering the animator and animation trigger
        triggeredEvent.anim = EditorGUILayout.ObjectField("Animator", triggeredEvent.anim, typeof(Animator), true) as Animator;
        triggeredEvent.animTrigger = EditorGUILayout.TextField("Animation Trigger", triggeredEvent.animTrigger);
    }

    private void ShowAudioGUI(TriggeredEvent triggeredEvent)
    {
        // A menu for selecting the type of audio event that will be played
        triggeredEvent.disableEventOnUse = EditorGUILayout.Toggle("Disable On Use", triggeredEvent.disableEventOnUse);
        GUILayout.Space(5);

        EditorGUILayout.LabelField("Audio Event Settings", EditorStyles.boldLabel);

        // A toggle for having the triggered event disable itself upon completion
        triggeredEvent.audioType = (TriggeredEvent.AudioType)EditorGUILayout.EnumPopup("Audio Type", triggeredEvent.audioType);
        GUILayout.Space(10);

        // SimpleAudioEvent is an single audio clip with set pitch and volume
        // RadomizedAudioEvent is multiple audio clips with randomness built in for audio clip selection, pitch, and volume
        switch (triggeredEvent.audioType)
        {
            case TriggeredEvent.AudioType.Simple: // TODO: Update this to the new SimpleAudioSource functionality
                triggeredEvent.audioClip = EditorGUILayout.ObjectField("Audio Clip", triggeredEvent.audioClip, typeof(AudioClip), true) as AudioClip;
                triggeredEvent.audioVolume = EditorGUILayout.Slider("Volume", triggeredEvent.audioVolume, 0, 100);
                triggeredEvent.audioPitch = EditorGUILayout.Slider("Pitch", triggeredEvent.audioPitch, 0, 2);
                break;
            case TriggeredEvent.AudioType.Randomized:
                triggeredEvent.clipCount = EditorGUILayout.FloatField("Clip Count", triggeredEvent.clipCount);

                //triggeredEvent.audioClips[0] = EditorGUILayout.ObjectField(triggeredEvent.audioClips[0], typeof(AudioClip), true) as AudioClip;

                // TODO: Setup random ranges for volume and pitch
                triggeredEvent.audioVolume = EditorGUILayout.Slider("Volume", triggeredEvent.audioVolume, 0, 100);
                triggeredEvent.audioPitch = EditorGUILayout.Slider("Pitch", triggeredEvent.audioPitch, 0, 2);
                break;
        }
    }

    private void ShowDestroyGUI(TriggeredEvent triggeredEvent)
    {
        EditorGUILayout.LabelField("Destroy Event Settings", EditorStyles.boldLabel);

        // Fields for entering the object to be destroyed and a destruction delay time
        triggeredEvent.DestructableObject = EditorGUILayout.ObjectField("Destructable Object", triggeredEvent.DestructableObject, typeof(GameObject), true) as GameObject;
        triggeredEvent.DestructionDelay = EditorGUILayout.FloatField("Destruction Delay", triggeredEvent.DestructionDelay);
    }

    private void ShowMusicGUI(TriggeredEvent triggeredEvent)
    {
        // A toggle for having the triggered event disable itself upon completion
        triggeredEvent.disableEventOnUse = EditorGUILayout.Toggle("Disable On Use", triggeredEvent.disableEventOnUse);
        GUILayout.Space(5);

        EditorGUILayout.LabelField("Music Event Settings", EditorStyles.boldLabel);
        triggeredEvent.loopMusic = EditorGUILayout.Toggle("Loop Music", triggeredEvent.loopMusic);

        // Fields for entering a music clip, fade in time, fade out time, and volume
        triggeredEvent.musicClip = EditorGUILayout.ObjectField("Music Clip", triggeredEvent.musicClip, typeof(AudioClip), true) as AudioClip;
        triggeredEvent.musicFadeInTime = EditorGUILayout.Slider("Fade In Time", triggeredEvent.musicFadeInTime, triggeredEvent.fadeMin, triggeredEvent.fadeMax);
        triggeredEvent.musicFadeOutTime = EditorGUILayout.Slider("Fade Out Time", triggeredEvent.musicFadeOutTime, triggeredEvent.fadeMin, triggeredEvent.fadeMax);
        triggeredEvent.audioVolume = EditorGUILayout.Slider("Music Volume", triggeredEvent.audioVolume, triggeredEvent.musicVolumeMin, triggeredEvent.musicVolumeMin);
    }

    private void ShowParticleGUI(TriggeredEvent triggeredEvent)
    {
        // A toggle for having the triggered event disable itself upon completion
        triggeredEvent.disableEventOnUse = EditorGUILayout.Toggle("Disable On Use", triggeredEvent.disableEventOnUse);
        GUILayout.Space(5);

        EditorGUILayout.LabelField("Particle Event Settings", EditorStyles.boldLabel);

        // Field for entering a particle system
        triggeredEvent.particle = EditorGUILayout.ObjectField("Particle System", triggeredEvent.particle, typeof(ParticleSystem), true) as ParticleSystem;
    }
}
