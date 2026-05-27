using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DynamicDialogueTrigger : DialogueTrigger
{
    [Header("Diálogos según estado")]
    [SerializeField] private Dialogue dialogo0; // Nunca hablado, 0 coleccionables
    [SerializeField] private Dialogue dialogo1; // Hablado, 0 coleccionables
    [SerializeField] private Dialogue dialogo2; // Hablado, algunos (no todos)
    [SerializeField] private Dialogue dialogo3; // Hablado, todos
    [SerializeField] private Dialogue dialogo4; // Nunca hablado, algunos
    [SerializeField] private Dialogue dialogo5; // Nunca hablado, todos

    [Header("Referencias")]
    [SerializeField] private Player player;

    [Header("Cambio de escena")]
    [SerializeField] private string nombreEscenaDestino;

    private int totalCollectibles;
    private DialogueManager dialogueManager;

    private string PlayerPrefsKey => $"NPCTalked_{gameObject.name}";
    private bool HasTalkedBefore
    {
        get => PlayerPrefs.GetInt(PlayerPrefsKey, 0) == 1;
        set => PlayerPrefs.SetInt(PlayerPrefsKey, value ? 1 : 0);
    }

    private void Start()
    {
        if (player == null)
            player = FindObjectOfType<Player>();

        // Contar coleccionables por tag
        GameObject[] collectibles = GameObject.FindGameObjectsWithTag("Collectibles");
        totalCollectibles = collectibles.Length;

        if (totalCollectibles == 0)
            Debug.LogWarning("No hay objetos con tag 'Collectible' en la escena.");

        dialogueManager = FindObjectOfType<DialogueManager>();
        if (dialogueManager == null)
            Debug.LogError("No se encontró DialogueManager en la escena.");
    }

    public override void TriggerDialogue(NPCtalk npc)
    {
        // Seleccionar el diálogo adecuado según el estado actual
        dialogue = GetAppropriateDialogue();
        
        // Iniciar el diálogo usando el método original
        base.TriggerDialogue(npc);
        
        // Marcar que ya se habló con este NPC
        HasTalkedBefore = true;

        // Si el diálogo seleccionado es el de "todos los coleccionables", iniciar la corrutina para cambiar de escena
        if (dialogue == dialogo3 || dialogue == dialogo5)
        {
            Debug.Log("Diálogo de colección completa detectado. Se cambiará de escena al cerrar el diálogo.");
            StartCoroutine(WaitForDialogueEndAndChangeScene());
        }
    }

    private IEnumerator WaitForDialogueEndAndChangeScene()
    {
        // Esperar mientras el diálogo esté activo
        while (dialogueManager != null && dialogueManager.interacting)
        {
            yield return null;
        }

        // Pequeña pausa para que termine la animación de cierre
        yield return new WaitForSeconds(0.2f);

        Debug.Log($"Cambiando a la escena: {nombreEscenaDestino}");
        SceneManager.LoadScene(nombreEscenaDestino);
    }

    private Dialogue GetAppropriateDialogue()
    {
        int collected = player != null ? player.collectionable : 0;
        bool hasAll = collected >= totalCollectibles;
        bool hasAny = collected > 0;
        bool talkedBefore = HasTalkedBefore;

        Debug.Log($"Coleccionables: {collected}/{totalCollectibles} | Todos: {hasAll} | Alguno: {hasAny} | Hablado antes: {talkedBefore}");

        if (!talkedBefore)
        {
            if (hasAll) return dialogo5;
            if (hasAny) return dialogo4;
            return dialogo0;
        }
        else
        {
            if (hasAll) return dialogo3;
            if (hasAny) return dialogo2;
            return dialogo1;
        }
    }
}