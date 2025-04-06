using UnityEngine;

// --- Quest 8: Go to the front door ---
public class FindCrickets : Quest
{
    [Header("Objective Settings")]
    [Tooltip("The tag of the crickets trigger.")]
    public string cricketsTag = "Crickets";
    [Tooltip("The maximum distance the player can be from the crickets.")]
    public float requiredDistance = 5.0f;

    private Transform cricketsTransform;

    public override void Initialize(QuestManager manager)
    {
        base.Initialize(manager);
        GameObject doorObject = GameObject.FindGameObjectWithTag(cricketsTag);
        if (doorObject != null)
        {
            cricketsTransform = doorObject.transform;
        }
        else
        {
            Debug.LogError($"'{questName}': Could not find GameObject with tag '{cricketsTag}'. Make sure it exists and is tagged correctly.", this);
        }
    }

    public override bool CheckCompletion(Transform playerTransform)
    {
        if (isComplete || !isActive || cricketsTransform == null || playerTransform == null)
        {
            return isComplete;
        }

        float distance = Vector3.Distance(playerTransform.position, cricketsTransform.position);
        if (distance <= requiredDistance)
        {
            CompleteQuest();
            return true;
        }
        return false;
    }
}