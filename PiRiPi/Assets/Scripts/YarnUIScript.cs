using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YarnUIScript : MonoBehaviour
{
    public Player player;
    private TextMeshProUGUI textM;
    private Image img;
    [SerializeField] private float timer;
    [SerializeField] private int prevScore;
    [SerializeField] private int prueba;


    void Start()
    {
        textM = GetComponentInChildren<TextMeshProUGUI>();
        img = GetComponentInChildren<Image>();

        textM.enabled = false;
        img.enabled = false;
    }

    void Update()
    {
        UIManage();
        textM.text = player.yarn.ToString();
    }

    private void UIManage()
    {
        prueba = player.yarn;
        if (prevScore == prueba)
        {
            textM.enabled = true;
            img.enabled = true;
            timer += Time.deltaTime;

            if (timer >= 3)
            {
                timer = 0;
                textM.enabled = false;
                img.enabled = false;
                prevScore = prueba + 1;
            }
        }
        else if (prevScore < prueba)
        {
            timer = 0;
            prevScore = prueba;
        }
    }
}
