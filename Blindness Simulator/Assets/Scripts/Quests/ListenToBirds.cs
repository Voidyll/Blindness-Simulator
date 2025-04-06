using UnityEngine;

// --- Quest 8: Go to the front door ---
public class ListenToBirds : Quest
{
    [Header("Objective Settings")]
    [Tooltip("The tag of the birds trigger.")]
    public string birdsTag = "Birds";
    [Tooltip("The maximum distance the player can be from the birds.")]
    public float requiredDistance = 5.0f;

    private Transform birdsTransform;

    public override void Initialize(QuestManager manager)
    {
        base.Initialize(manager);
        GameObject doorObject = GameObject.FindGameObjectWithTag(birdsTag);
        if (doorObject != null)
        {
            birdsTransform = doorObject.transform;
        }
        else
        {
            Debug.LogError($"'{questName}': Could not find GameObject with tag '{birdsTag}'. Make sure it exists and is tagged correctly.", this);
        }
    }

    public override bool CheckCompletion(Transform playerTransform)
    {
        if (isComplete || !isActive || birdsTransform == null || playerTransform == null)
        {
            return isComplete;
        }

        float distance = Vector3.Distance(playerTransform.position, birdsTransform.position);
        if (distance <= requiredDistance)
        {
            CompleteQuest();
            return true;
        }
        return false;
    }
}