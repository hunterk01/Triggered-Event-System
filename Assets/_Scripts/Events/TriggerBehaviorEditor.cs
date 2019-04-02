using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TriggerBehavior))]
public class TriggerBehaviorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        TriggerBehavior triggerBehavior = (TriggerBehavior)target;

        GUILayout.Space(10);
        EditorGUILayout.LabelField("Trigger Settings", EditorStyles.boldLabel);
        triggerBehavior.showTriggerInEditor = EditorGUILayout.Toggle("Show Trigger In Editor", triggerBehavior.showTriggerInEditor);
        PlayerTriggerCheck(triggerBehavior);
    }

    private void PlayerTriggerCheck(TriggerBehavior triggerBehavior)
    {
        // A toggle for if the event is triggered by the player
        triggerBehavior.triggeredByPlayer = EditorGUILayout.Toggle("Triggered By Player", triggerBehavior.triggeredByPlayer);

        // If it's not triggered by the player, then show an object field to drag the triggering object to
        if (!triggerBehavior.triggeredByPlayer)
        {
            triggerBehavior.triggeredByObject = EditorGUILayout.ObjectField("Object that triggers", triggerBehavior.triggeredByObject, typeof(GameObject), true) as GameObject;
        }
    }
}
