using UnityEngine;
// --- Quest 2 & 5: Brush your teeth ---
public class BrushTeethQuest : Quest
{
    [Header("Objective Settings")]
    [Tooltip("The tags of objects the player needs to approach (e.g., Sink, Toothbrush).")]
    public string[] targetTags = { "Sink", "Toothbrush" };
    [Tooltip("The maximum distance the player can be from the target object.")]
    public float requiredDistance = 1.5f;

    // No need to cache transforms here, as we check multiple tags
    // Could be optimized if performance becomes an issue

    public override bool CheckCompletion(Transform playerTransform)
    {
        if (isComplete || !isActive || playerTransform == null)
        {
            return isComplete;
        }

        foreach (string tag in targetTags)
        {
            GameObject targetObject = GameObject.FindGameObjectWithTag(tag);
            if (targetObject != null)
            {
                float distance = Vector3.Distance(playerTransform.position, targetObject.transform.position);
                if (distance <= requiredDistance)
                {
                    CompleteQuest();
                    return true; // Found a valid object close enough
                }
            }
            // else: Object with this tag not found, continue checking other tags
        }

        // If loop finishes without finding a close enough tagged object
        return false;
    }
}

