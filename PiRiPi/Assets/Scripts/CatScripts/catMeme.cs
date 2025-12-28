using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class catMeme : MonoBehaviour
{
    [SerializeField] RectTransform[] whiteRT;
    [SerializeField] RectTransform[] eyesRT;
    [SerializeField] float[] limitesUpDown;
    [SerializeField] float[] limitesDer;
    [SerializeField] float[] limitesIzq;

    private void Start() 
    {
        eyesRT[0].anchoredPosition = whiteRT[0].anchoredPosition;   
        eyesRT[1].anchoredPosition = whiteRT[1].anchoredPosition; 
        getLimits();    
    }
    void Update()
    {
        //moveEyes();
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Vector2 centeredMousePos = mousePos - screenCenter;

        eyesRT[0].anchoredPosition = centeredMousePos;
        if(eyesRT[0].anchoredPosition.y > limitesUpDown[0]) eyesRT[0].anchoredPosition = new Vector2 (eyesRT[0].anchoredPosition.x, 63);
        if(eyesRT[0].anchoredPosition.y < limitesUpDown[1]) eyesRT[0].anchoredPosition = new Vector2 (eyesRT[0].anchoredPosition.x, -63);
        if(eyesRT[0].anchoredPosition.x > limitesDer[0]) eyesRT[0].anchoredPosition = new Vector2 (-131, eyesRT[0].anchoredPosition.y);
        if(eyesRT[0].anchoredPosition.x < limitesIzq[0]) eyesRT[0].anchoredPosition = new Vector2 (-254, eyesRT[0].anchoredPosition.y);
    }

    public void getLimits()
    {
        limitesUpDown[0] =  64;
        limitesUpDown[1] =  -64;
        limitesDer[0] = -130;
        limitesIzq[0] = -250;
        limitesDer[1] = 145;
        limitesIzq[1] = 255;
    }
}
