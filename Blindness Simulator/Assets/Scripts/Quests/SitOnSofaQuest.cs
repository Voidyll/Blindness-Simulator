using UnityEngine;

// --- Quest 3: Sit on the sofa ---
public class SitOnSofaQuest : Quest
{
    [Header("Objective Settings")]
    [Tooltip("The tag of the sofa object.")]
    public string sofaTag = "Sofa";
    [Tooltip("The maximum distance the player can be from the sofa.")]
    public float requiredDistance = 2.0f;

    private Transform sofaTransform;

     public override void Initialize(QuestManager manager)
    {
        base.Initialize(manager);
        GameObject sofaObject = GameObject.FindGameObjectWithTag(sofaTag);
        if (sofaObject != null)
        {
            sofaTransform = sofaObject.transform;
        }
        else
        {
            Debug.LogError($"'{questName}': Could not find GameObject with tag '{sofaTag}'. Make sure it exists and is tagged correctly.", this);
        }
    }

    public override bool CheckCompletion(Transform playerTransform)
    {
        if (isComplete || !isActive || sofaTransform == null || playerTransform == null)
        {
            return isComplete;
        }

        float distance = Vector3.Distance(playerTransform.position, sofaTransform.position);
        if (distance <= requiredDistance)
        {
            CompleteQuest();
            return true;
        }
        return false;
    }
}