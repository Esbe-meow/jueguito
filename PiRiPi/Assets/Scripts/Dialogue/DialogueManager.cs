using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum DialogueState
{
    None,
    Starting,
    Typing,
    Playing
}

public class DialogueManager : MonoBehaviour
{
    [SerializeField] Queue<string> sentences = new Queue<string>();

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public bool interacting;
    public DialogueState state = DialogueState.None;

    [SerializeField] Animator animator;

    public NPCtalk currentNPC;

    [Header("Typing")]
    public float typingSpeed = 0.03f;

    Coroutine typingCoroutine;
    string currentSentence;
    
    [Header("Audio")]
    [SerializeField] AudioSource AS;
    [SerializeField] AudioClip blahblah;

    void Awake()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue, NPCtalk npc)
    {
        interacting = true;
        animator.SetBool("isOpen", true);
        state = DialogueState.Starting;
        currentNPC = npc;

        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
            sentences.Enqueue(sentence);

        DisplayNextSentence();

        StartCoroutine(EnablePlayingNextFrame());
    }

    IEnumerator EnablePlayingNextFrame()
    {
        yield return null; 
        state = DialogueState.Playing;
    }

    public void DisplayNextSentence()
    {
        if (state == DialogueState.Typing)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = currentSentence;
            state = DialogueState.Playing;
            return;
        }

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        currentSentence = sentences.Dequeue();

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeSentence(currentSentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        state = DialogueState.Typing;
        dialogueText.text = "";

        foreach (char c in sentence)
        {
            dialogueText.text += c;
            AS.PlayOneShot(blahblah);
            yield return new WaitForSeconds(typingSpeed);
        }

        state = DialogueState.Playing;
    }

    void EndDialogue()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        StartCoroutine(CloseAfterAnimation());
    }

    private IEnumerator CloseAfterAnimation()
    {
        animator.SetBool("isOpen", false);
        yield return new WaitForSecondsRealtime(0.5f); 
        interacting = false;
        state = DialogueState.None;
        currentNPC = null;
    }
}
