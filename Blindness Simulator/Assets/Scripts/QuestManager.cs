using UnityEngine;
using UnityEngine.UI; // Required for UI elements like Text
using System.Collections;
using System.Collections.Generic; // Required for List
using TMPro;

/// <summary>
/// Manages the sequence of quests, tracks progress, handles day breaks,
/// and updates the UI.
/// </summary>
public class QuestManager : MonoBehaviour
{
    [Header("Quest Configuration")]
    [Tooltip("Assign all quest GameObjects here in the desired order.")]
    public List<Quest> questList = new List<Quest>(); // Assign Quest components (or prefabs) in the Inspector

    [Header("Player Reference")]
    [Tooltip("Assign the player's Transform here.")]
    public Transform playerTransform;

    [Header("Day Break Settings")]
    [Tooltip("The index in the questList AFTER which the day break occurs (e.g., index 2 for after the 3rd quest).")]
    public int dayBreakQuestIndex = 2; // After quest index 2 (Sit on Sofa)
    [Tooltip("Duration of the day break delay in seconds.")]
    public float dayBreakDuration = 7.0f; // 5-10 seconds delay

    [Header("UI (Optional)")]
    [Tooltip("Assign a UI Text element to display the current quest.")]
    public TMP_Text questDisplay; // Assign in Inspector

    // --- Private State ---
    private int currentQuestIndex = -1;
    private Quest currentQuest = null;
    private bool isDayBreaking = false; // Flag to prevent checks during day break
    private bool allQuestsComplete = false;

    void Start()
    {
        if (playerTransform == null)
        {
            Debug.LogError("QuestManager: Player Transform is not assigned!", this);
            this.enabled = false; // Disable manager if player is missing
            return;
        }

        if (questList.Count == 0)
        {
             Debug.LogWarning("QuestManager: No quests assigned to the quest list.", this);
             this.enabled = false;
             return;
        }

        // Initialize all quests
        for (int i = 0; i < questList.Count; i++)
        {
            if (questList[i] != null)
            {
                questList[i].Initialize(this);
            }
            else
            {
                Debug.LogError($"QuestManager: Quest at index {i} is null!", this);
                // Optionally remove null entries or handle differently
            }
        }

        // Start the first quest
        StartNextQuest();
    }

    void Update()
    {
        // Only check for completion if there's an active quest,
        // not during day break, and not all quests are done.
        if (currentQuest != null && currentQuest.isActive && !isDayBreaking && !allQuestsComplete)
        {
            // Ask the current quest to check its completion status
            currentQuest.CheckCompletion(playerTransform);
            // Note: Completion is handled by the quest itself calling CompleteQuest(),
            // which then calls OnQuestCompleted() here.
        }
    }

    /// <summary>
    /// Called by a Quest when it is completed.
    /// </summary>
    /// <param name="completedQuest">The quest that was just finished.</param>
    public void OnQuestCompleted(Quest completedQuest)
    {
        if (completedQuest != currentQuest)
        {
            Debug.LogWarning($"QuestManager: Received completion signal from non-active quest '{completedQuest.questName}'. Ignoring.", this);
            return;
        }

        Debug.Log($"QuestManager: Detected completion of '{completedQuest.questName}'.");

        // Check if this is the quest that triggers the day break
        if (currentQuestIndex == dayBreakQuestIndex)
        {
            StartCoroutine(DayBreakCoroutine());
        }
        else
        {
            // Immediately proceed to the next quest
            StartNextQuest();
        }
    }

    /// <summary>
    /// Starts the next quest in the sequence.
    /// </summary>
    private void StartNextQuest()
    {
        currentQuestIndex++;

        if (currentQuestIndex < questList.Count)
        {
            currentQuest = questList[currentQuestIndex];
            if (currentQuest != null)
            {
                currentQuest.StartQuest(); // This also updates the UI via the quest itself
            }
            else
            {
                 Debug.LogError($"QuestManager: Cannot start quest at index {currentQuestIndex} because it is null. Skipping.", this);
                 StartNextQuest(); // Try the next one
            }
        }
        else
        {
            // All quests are finished
            currentQuest = null;
            allQuestsComplete = true;
            Debug.Log("QuestManager: All quests completed!");
            if (questDisplay != null)
            {
                questDisplay.text = "All tasks complete!";
            }
            // Optional: Trigger game end state, final cutscene, etc.
        }
    }

    /// <summary>
    /// Coroutine to handle the delay between Day 1 and Day 2.
    /// </summary>
    private IEnumerator DayBreakCoroutine()
    {
        isDayBreaking = true; // Pause quest checking
        Debug.Log("QuestManager: Day 1 finished. Starting day break...");
        if (questDisplay != null)
        {
            // You might want a specific message during the break
            questDisplay.text = "End of Day 1...";
        }

        // Wait for the specified duration
        yield return new WaitForSeconds(dayBreakDuration);

        Debug.Log("QuestManager: Day break finished. Starting Day 2 quests...");
        isDayBreaking = false; // Resume quest checking

        // Start the next quest (which will be the first quest of Day 2)
        StartNextQuest();
    }

    /// <summary>
    /// Updates the optional UI Text element with the current quest info.
    /// Called by the Quest when it starts.
    /// </summary>
    public void UpdateQuestDisplay()
    {
        if (questDisplay != null && currentQuest != null)
        {
            questDisplay.text = $"Current Task: {currentQuest.questName}\n{currentQuest.description}";
        }
        else if (questDisplay != null && allQuestsComplete)
        {
             questDisplay.text = "All tasks complete!";
        }
         else if (questDisplay != null)
        {
            questDisplay.text = ""; // Clear display if no quest or UI element
        }
    }
}
