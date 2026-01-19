using UnityEngine;
using System.Collections.Generic;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance;
    private HashSet<string> collected = new HashSet<string>();
    [SerializeField] bool reset;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Load();
        }
        else Destroy(gameObject);

        if(reset)
            ResetAllCollectibles();
    }

    public bool IsCollected(string id)
    {
        return collected.Contains(id);
    }

    public void Collect(string id)
    {
        if (!collected.Contains(id))
        {
            collected.Add(id);
            PlayerPrefs.SetString("Collected", string.Join(",", collected));
            PlayerPrefs.Save();
        }
    }

    void Load()
    {
        string data = PlayerPrefs.GetString("Collected", "");
        if (!string.IsNullOrEmpty(data))
            collected = new HashSet<string>(data.Split(','));
    }

    public void ResetAllCollectibles()
    {
        collected.Clear();
        PlayerPrefs.DeleteKey("Collected");
        PlayerPrefs.Save();
    }

}
