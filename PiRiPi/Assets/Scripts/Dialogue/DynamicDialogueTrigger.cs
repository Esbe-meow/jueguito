using UnityEngine;
using UnityEngine.SceneManagement;


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
    
    private int totalCollectibles; // Total de coleccionables en el nivel
    private DialogueManager dialogueManager;
    private bool pendingSceneChange = false;
    private bool interactingHere = false;

    // Guarda el nombre del NPC si ya habló con el antes
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

        GameObject[] collectibles = GameObject.FindGameObjectsWithTag("Collectibles");
        totalCollectibles = collectibles.Length;

        if (totalCollectibles == 0)
            Debug.LogWarning("No hay objetos con tag 'Collectible' en la escena. Asegúrate de etiquetarlos.");
    }
    
    private void Update()
    {
        if (pendingSceneChange && !dialogueManager.interacting)
        {
            pendingSceneChange = false;
            Debug.Log($"Cambiando a la escena: {nombreEscenaDestino}");
            SceneManager.LoadScene(nombreEscenaDestino);
        }
    }

    // Este método reemplaza al original
    public override void TriggerDialogue(NPCtalk npc)
    {
        // Selecciona el diálogo según el estado actual
        dialogue = GetAppropriateDialogue();

        // Llamar al método original (DialogueTrigger) para iniciar el diálogo
        base.TriggerDialogue(npc);

        // Marcar que ya se habló con este NPC (para la próxima vez)
        HasTalkedBefore = true;
        
        // Si tiene todos los coleccionables, pasa a creditos
        if (dialogue == dialogo3 || dialogue == dialogo5)
        {
            pendingSceneChange = true;
        }
    }

    private Dialogue GetAppropriateDialogue()
    {
        int collected = player != null ? player.collectionable : 0;
        bool hasAll = collected >= totalCollectibles;
        bool hasAny = collected > 0;
        bool talkedBefore = HasTalkedBefore;

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