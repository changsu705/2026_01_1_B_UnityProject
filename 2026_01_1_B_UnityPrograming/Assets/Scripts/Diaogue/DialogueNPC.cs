using UnityEngine;

public class DialogueNPC : MonoBehaviour
{
    public DialogueDataSO myDialogue;
    private DialogueManager dialogueManager;

    void Start()
    {
        dialogueManager = FindAnyObjectByType<DialogueManager>();

        if (dialogueManager != null)
        {
            Debug.Log("DialogueManager found in the scene.");
        }

        }

    private void OnMouseDown()
    {
        if (dialogueManager == null) return;

        if (dialogueManager.IsDialogueActive()) return;

        if (myDialogue != null) return;

        dialogueManager.StartDialogue(myDialogue);
    }

    void Update()
    {
        
    }
}
