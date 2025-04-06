using UnityEngine;
public class GoToSwingsQuest : Quest
{
    [Header("Objective Settings")]
    [Tooltip("The tag of the swings trigger.")]
    public string swingsTag = "Swings";
    [Tooltip("The maximum distance the player can be from the swings.")]
    public float requiredDistance = 5.0f;

    private Transform swingsTransform;

    public override void Initialize(QuestManager manager)
    {
        base.Initialize(manager);
        GameObject swingObject = GameObject.FindGameObjectWithTag(swingsTag);
        if (swingObject != null)
        {
            swingsTransform = swingObject.transform;
        }
        else
        {
            Debug.LogError($"'{questName}': Could not find GameObject with tag '{swingsTag}'. Make sure it exists and is tagged correctly.", this);
        }
    }

    public override bool CheckCompletion(Transform playerTransform)
    {
        if (isComplete || !isActive || swingsTransform == null || playerTransform == null)
        {
            return isComplete;
        }

        float distance = Vector3.Distance(playerTransform.position, swingsTransform.position);
        if (distance <= requiredDistance)
        {
            CompleteQuest();
            return true;
        }
        return false;
    }
}