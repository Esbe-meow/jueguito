using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VideoSettings : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;
    [SerializeField] TextMeshProUGUI screenText;
    [SerializeField] bool state;

    private void Awake() 
    {
        dropdown.value = QualitySettings.GetQualityLevel();
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        Debug.Log("Full screen: " + isFullScreen);
    }

    public void onClickFullScreen()
    {
        if(state)
        {
            state = false;
            SetFullScreen(state);
            screenText.text = "Full Screen";
        }
        else
        {
            state = true;
            SetFullScreen(state);
            screenText.text = "Windowed";
        }
    }
}
