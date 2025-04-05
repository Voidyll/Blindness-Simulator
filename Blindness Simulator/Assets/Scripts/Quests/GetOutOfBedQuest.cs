using UnityEngine;

// --- Quest 1 & 4: Get out of bed ---
public class GetOutOfBedQuest : Quest
{
    [Header("Objective Settings")]
    [Tooltip("The tag of the bed object.")]
    public string bedTag = "Bed";
    [Tooltip("The minimum distance the player must be from the bed.")]
    public float requiredDistance = 3.0f;

    private Transform bedTransform;

    public override void Initialize(QuestManager manager)
    {
        base.Initialize(manager);
        // Find the bed object once during initialization for efficiency
        GameObject bedObject = GameObject.FindGameObjectWithTag(bedTag);
        if (bedObject != null)
        {
            bedTransform = bedObject.transform;
        }
        else
        {
            Debug.LogError($"'{questName}': Could not find GameObject with tag '{bedTag}'. Make sure it exists and is tagged correctly.", this);
        }
    }

    public override bool CheckCompletion(Transform playerTransform)
    {
        if (isComplete || !isActive || bedTransform == null || playerTransform == null)
        {
            return isComplete; // Return true if already complete, false if prerequisites not met
        }

        float distance = Vector3.Distance(playerTransform.position, bedTransform.position);
        if (distance > requiredDistance)
        {
            CompleteQuest();
            return true;
        }
        return false;
    }
}

