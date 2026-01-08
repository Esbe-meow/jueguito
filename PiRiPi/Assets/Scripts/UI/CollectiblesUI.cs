using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectiblesUI : MonoBehaviour
{
    public Player player;
    [SerializeField] private TextMeshProUGUI yarnText;
    [SerializeField] private TextMeshProUGUI collectionableText;
    [SerializeField] private GameObject ui;
    [SerializeField] private float timer;
    [SerializeField] private int prevYarn;
    [SerializeField] private int prevCollectionable;


    void Start()
    {
        ui.SetActive(false);
    }

    void Update()
    {
        UpdateValues(player.yarn, ref prevYarn);
        UpdateValues(player.collectionable, ref prevCollectionable);
        yarnText.text = player.yarn.ToString();
        collectionableText.text = player.collectionable.ToString();
    }

    private void UpdateValues(int num, ref int prev)
    {
        if (prev == num)
        {
            ui.SetActive(true);
            timer += Time.deltaTime;

            if (timer >= 3)
            {
                timer = 0;
                ui.SetActive(false);
                prev = num + 1;
            }
        }
        else if (prev < num)
        {
            timer = 0;
            prev = num;
        }
    }
}
