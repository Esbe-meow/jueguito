using UnityEngine;

public class NPCtalk : MonoBehaviour
{
    [SerializeField] bool nearNPC;
    [SerializeField] DialogueTrigger dialogueTrigger;
    [SerializeField] DialogueManager dialogueManager;
    [SerializeField] GameObject bubble;

    void Update()
    {
        if (nearNPC && Input.GetButtonDown("Interact") && !dialogueManager.interacting)
        {
            bubble.SetActive(true);
            dialogueTrigger.TriggerDialogue(this);
            return;
        }

        if (dialogueManager.currentNPC == this && (dialogueManager.state == DialogueState.Playing || dialogueManager.state == DialogueState.Typing) && Input.GetButtonDown("Continue"))
        {
            dialogueManager.DisplayNextSentence();
        }

        if (!dialogueManager.interacting)
        {
            bubble.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        nearNPC = true;
    }

    private void OnTriggerExit(Collider other)
    {
        nearNPC = false;
    }
}
