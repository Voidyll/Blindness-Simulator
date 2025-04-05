using UnityEngine;

/// <summary>
/// Abstract base class for all quests in the game.
/// </summary>
public abstract class Quest : MonoBehaviour
{
    [Header("Quest Info")]
    [Tooltip("The name of the quest displayed to the player.")]
    public string questName = "New Quest";
    [TextArea]
    [Tooltip("A brief description of the quest objective.")]
    public string description = "Quest Description";

    [Header("Quest State")]
    [Tooltip("Is this quest currently active?")]
    public bool isActive = false;
    [Tooltip("Has this quest been completed?")]
    public bool isComplete = false;

    // Reference to the Quest Manager
    protected QuestManager questManager;

    /// <summary>
    /// Initializes the quest with a reference to the QuestManager.
    /// Called by QuestManager when setting up quests.
    /// </summary>
    /// <param name="manager">The QuestManager controlling this quest.</param>
    public virtual void Initialize(QuestManager manager)
    {
        questManager = manager;
        // Ensure the quest component itself is disabled initially.
        // The QuestManager will enable the active quest's component.
        this.enabled = false;
    }

    /// <summary>
    /// Called when the quest becomes the active quest.
    /// </summary>
    public virtual void StartQuest()
    {
        isActive = true;
        isComplete = false;
        this.enabled = true; // Enable the component to run Update checks if needed
        Debug.Log($"Quest Started: {questName}");
        // Optional: Update UI or trigger events
        if (questManager != null)
        {
            questManager.UpdateQuestDisplay();
        }
    }

    /// <summary>
    /// Called when the quest objective is met.
    /// </summary>
    public virtual void CompleteQuest()
    {
        if (!isComplete) // Prevent completing multiple times
        {
            isActive = false;
            isComplete = true;
            this.enabled = false; // Disable component when complete
            Debug.Log($"Quest Completed: {questName}");
            // Notify the QuestManager
            if (questManager != null)
            {
                questManager.OnQuestCompleted(this);
            }
            // Optional: Give rewards, trigger events
        }
    }

    /// <summary>
    /// Abstract method to be implemented by derived quest classes.
    /// Checks if the conditions for completing this quest have been met.
    /// </summary>
    /// <param name="playerTransform">The transform of the player character.</param>
    /// <returns>True if the quest is completed, false otherwise.</returns>
    public abstract bool CheckCompletion(Transform playerTransform);

    // Optional: Add an Update method in derived classes if continuous checking is needed.
    // protected virtual void Update() {
    //     if (isActive && !isComplete) {
    //         // Continuous checks can happen here, but often checks are better triggered by events
    //         // or called directly by the QuestManager's Update.
    //         // For simplicity, we'll have QuestManager call CheckCompletion.
    //     }
    // }
}
