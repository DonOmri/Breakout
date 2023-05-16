using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class SettingsGetDifficulty : MonoBehaviour
{
    public CrossSceneData crossSceneData;
    private TextMeshProUGUI _scoreText;

    private void Start()
    {
        _scoreText = GetComponent<TextMeshProUGUI>();
        UpdateDifficulty();
    }

    public void UpdateDifficulty()
    {
        _scoreText.text = crossSceneData.GetDifficulty().ToString(CultureInfo.DefaultThreadCurrentCulture);
    }
}
