using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class catMeme : MonoBehaviour
{
    [SerializeField] RectTransform[] whiteRT;
    [SerializeField] RectTransform[] eyesRT;
    [SerializeField] RectTransform catPos;
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
        Vector2 catScreenPos = RectTransformUtility.WorldToScreenPoint(null, catPos.position);
        Vector2 centeredMousePos = mousePos - catScreenPos;
    
        moveEyes(0, centeredMousePos);
        moveEyes(1, centeredMousePos);
        
    }
    
    public void moveEyes(int a, Vector2 pos)
    {
        eyesRT[a].anchoredPosition = pos;
        if(eyesRT[a].anchoredPosition.y > limitesUpDown[0]) eyesRT[a].anchoredPosition = new Vector2 (eyesRT[a].anchoredPosition.x, limitesUpDown[0]);
        if(eyesRT[a].anchoredPosition.y < limitesUpDown[1]) eyesRT[a].anchoredPosition = new Vector2 (eyesRT[a].anchoredPosition.x, limitesUpDown[1]);
        if(eyesRT[a].anchoredPosition.x > limitesDer[a]) eyesRT[a].anchoredPosition = new Vector2 (limitesDer[a], eyesRT[a].anchoredPosition.y);
        if(eyesRT[a].anchoredPosition.x < limitesIzq[a]) eyesRT[a].anchoredPosition = new Vector2 (limitesIzq[a], eyesRT[a].anchoredPosition.y);
    }
    //9
    public void getLimits()
    {
        limitesUpDown[0] =  whiteRT[0].anchoredPosition.y + whiteRT[0].rect.height/2; //limite de arriba
        limitesUpDown[1] =  whiteRT[0].anchoredPosition.y - whiteRT[0].rect.height/2; //limite abajo
        limitesDer[0] = whiteRT[0].anchoredPosition.x + whiteRT[0].rect.width/2;      //limite derecho del ojo izquierdo
        limitesIzq[0] = whiteRT[0].anchoredPosition.x - whiteRT[0].rect.width/2;      //limite izquierdo del ojo izquierdo
        limitesDer[1] = whiteRT[1].anchoredPosition.x + whiteRT[1].rect.width/2;      //limite derecho del ojo derecho
        limitesIzq[1] = whiteRT[1].anchoredPosition.x - whiteRT[1].rect.width/2;      //limite izquierdo del ojo derecho
    }
}
