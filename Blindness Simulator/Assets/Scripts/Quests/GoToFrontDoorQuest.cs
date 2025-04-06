using UnityEngine;

// --- Quest 8: Go to the front door ---
public class GoToFrontDoorQuest : Quest
{
    [Header("Objective Settings")]
    [Tooltip("The tag of the front door object.")]
    public string frontDoorTag = "FrontDoor";
    [Tooltip("The maximum distance the player can be from the front door.")]
    public float requiredDistance = 2.0f;

    private Transform frontDoorTransform;

     public override void Initialize(QuestManager manager)
    {
        base.Initialize(manager);
        GameObject doorObject = GameObject.FindGameObjectWithTag(frontDoorTag);
        if (doorObject != null)
        {
            frontDoorTransform = doorObject.transform;
        }
        else
        {
            Debug.LogError($"'{questName}': Could not find GameObject with tag '{frontDoorTag}'. Make sure it exists and is tagged correctly.", this);
        }
    }

    public override bool CheckCompletion(Transform playerTransform)
    {
        if (isComplete || !isActive || frontDoorTransform == null || playerTransform == null)
        {
            return isComplete;
        }

        float distance = Vector3.Distance(playerTransform.position, frontDoorTransform.position);
        if (distance <= requiredDistance)
        {
            CompleteQuest();
            return true;
        }
        return false;
    }
}