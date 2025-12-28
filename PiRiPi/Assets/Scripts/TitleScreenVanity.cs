using UnityEngine;
using UnityEngine.UI;

public class TitleScreenVanity : MonoBehaviour
{
    [SerializeField] RectTransform rt;
    [SerializeField] RectTransform canvasRT;
    [SerializeField] Image img;
    [SerializeField] Sprite[] images;
    [Range(200f, 400f)]
    [SerializeField] float movingSpeed;
    [SerializeField] int ID;
    [SerializeField] bool movingRight;


    void Start()
    {
        canvasRT = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        movingSpeed = Random.Range(200f, 400f);
    }

    void Update()
    {
        //Debug.Log(Vector2.left);
        //Debug.Log(Vector2.right);
        moverImagen();
    }

    public void moverImagen()
    {
        float limiteIzq = -1030;
        float limiteDer =  1030;

        //Debug.Log(limiteDer +" "+ limiteIzq);
        //Debug.Log(rt.anchoredPosition.x);

        if(movingRight)
        {
            rt.anchoredPosition += Vector2.right * movingSpeed * Time.deltaTime;
            //Debug.Log("voy a la derecha" + ID);
            if (rt.anchoredPosition.x > limiteDer)
            {
                movingRight = false;
                img.sprite = images[Random.Range(0,2)];
                movingSpeed = Random.Range(200f, 400f);
                float yRandom = Random.Range(-440f, 440f);
                rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, yRandom);
            }
        }
        else 
        {
            rt.anchoredPosition += Vector2.left * movingSpeed * Time.deltaTime;
            //Debug.Log("deberia ir a la izquierda" + ID);
            if (rt.anchoredPosition.x < limiteIzq)
            {
                movingRight = true;
                img.sprite = images[Random.Range(0,2)];
                movingSpeed = Random.Range(200f, 400f);
                float yRandom = Random.Range(-440f, 440f);
                rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, yRandom);
            }
        }
    }
}
