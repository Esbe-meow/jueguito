using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public virtual void TriggerDialogue(NPCtalk npc)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, npc);
    }
}
