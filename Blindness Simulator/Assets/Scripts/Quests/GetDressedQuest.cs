using UnityEngine;

// --- Quest 7: Get dressed ---
public class GetDressedQuest : Quest
{
    [Header("Objective Settings")]
    [Tooltip("The tag of the wardrobe object.")]
    public string wardrobeTag = "Wardrobe";
    [Tooltip("The maximum distance the player can be from the wardrobe.")]
    public float requiredDistance = 2.0f;

    private Transform wardrobeTransform;

    public override void Initialize(QuestManager manager)
    {
        base.Initialize(manager);
        GameObject wardrobeObject = GameObject.FindGameObjectWithTag(wardrobeTag);
        if (wardrobeObject != null)
        {
            wardrobeTransform = wardrobeObject.transform;
        }
        else
        {
            Debug.LogError($"'{questName}': Could not find GameObject with tag '{wardrobeTag}'. Make sure it exists and is tagged correctly.", this);
        }
    }

    public override bool CheckCompletion(Transform playerTransform)
    {
        if (isComplete || !isActive || wardrobeTransform == null || playerTransform == null)
        {
            return isComplete;
        }

        float distance = Vector3.Distance(playerTransform.position, wardrobeTransform.position);
        if (distance <= requiredDistance)
        {
            CompleteQuest();
            return true;
        }
        return false;
    }
}