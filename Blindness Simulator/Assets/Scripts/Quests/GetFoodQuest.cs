using UnityEngine;

// --- Quest 6: Get food ---
public class GetFoodQuest : Quest
{
    [Header("Objective Settings")]
    [Tooltip("The tags of objects the player needs to approach (e.g., Fridge, Table).")]
    public string[] targetTags = { "Fridge", "Table" };
    [Tooltip("The maximum distance the player can be from the target object.")]
    public float requiredDistance = 2.0f;

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
                    return true;
                }
            }
        }
        return false;
    }
}