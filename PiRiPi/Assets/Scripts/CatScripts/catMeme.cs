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
        
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Vector2 centeredMousePos = mousePos - screenCenter;

        moveEyes(0, centeredMousePos);
        moveEyes(1, centeredMousePos);
        
    }
    
    public void moveEyes(int a, Vector2 pos)
    {
        eyesRT[a].anchoredPosition = pos;
        if(eyesRT[a].anchoredPosition.y > limitesUpDown[0]) eyesRT[a].anchoredPosition = new Vector2 (eyesRT[a].anchoredPosition.x, 63);
        if(eyesRT[a].anchoredPosition.y < limitesUpDown[1]) eyesRT[a].anchoredPosition = new Vector2 (eyesRT[a].anchoredPosition.x, -63);
        if(eyesRT[a].anchoredPosition.x > limitesDer[a]) eyesRT[a].anchoredPosition = new Vector2 (limitesDer[a], eyesRT[a].anchoredPosition.y);
        if(eyesRT[a].anchoredPosition.x < limitesIzq[a]) eyesRT[a].anchoredPosition = new Vector2 (limitesIzq[a], eyesRT[a].anchoredPosition.y);
    }

    public void getLimits()
    {
        limitesUpDown[0] =  64;
        limitesUpDown[1] =  -64;
        limitesDer[0] = -130;
        limitesIzq[0] = -250;
        limitesDer[1] = 255;
        limitesIzq[1] = 145;
    }
}
