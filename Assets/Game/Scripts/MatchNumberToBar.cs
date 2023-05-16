using System;
using TMPro;
using UnityEngine;

public class MatchNumberToBar : MonoBehaviour
{
    public UnityEngine.UI.Slider slider;
    public string dataType;
    [SerializeField] private CrossSceneData settingsData;
    private TextMeshProUGUI _sliderText;

    private void Start()
    {
        _sliderText = GetComponent<TextMeshProUGUI>();
        slider.value = GetSpecificDataFactory(dataType);
        NumberUpdate();
    }

    public void NumberUpdate()
    {
        int currentValue = (int) slider.value;
        _sliderText.text = "(" + currentValue + ")";
        SetSpecificDataFactory(dataType, currentValue);
    }

    private void SetSpecificDataFactory(string type, int currentValue)
    {
        switch (type)
        {
            case "PaddleSize":
            {
                settingsData.PaddleSizeValue = currentValue;
                break;   
            }
            case "PaddleSpeed":
            {
                settingsData.PaddleSpeedValue = currentValue;
                break;   
            }
            case "BallSize":
            {
                settingsData.BallSizeValue = currentValue;
                break;   
            }
            case "BallSpeed":
            {
                settingsData.BallSpeedValue = currentValue;
                break;   
            }
            default: throw new IndexOutOfRangeException("type must be either one of:\"PaddleSize\", " +
                                                        "\"PaddleSpeed\", \"BallSize\", \"BallSpeed\",");
        }
    }
    
    private float GetSpecificDataFactory(string type)
    {
        switch (type)
        {
            case "PaddleSize": return settingsData.PaddleSizeValue;
            case "PaddleSpeed": return settingsData.PaddleSpeedValue;
            case "BallSize": return settingsData.BallSizeValue;
            case "BallSpeed": return settingsData.BallSpeedValue;
            default: throw new IndexOutOfRangeException("type must be either one of:\"PaddleSize\", " +
                                                        "\"PaddleSpeed\", \"BallSize\", \"BallSpeed\",");
        }
    }
}
