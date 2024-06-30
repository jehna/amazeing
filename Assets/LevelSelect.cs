using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class LevelSelect : MonoBehaviour
{



    void Start()
    {
        UIDocument doc = GetComponent<UIDocument>();
        doc.rootVisualElement.Q<Button>("next-level").clicked += () =>
        {
            int currentLevel = PlayerPrefs.GetInt("level", 0);
            PlayerPrefs.SetInt("level", Math.Min(currentLevel + 1, MazeGenerator.sizes.Length - 1));
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        };
        doc.rootVisualElement.Q<Button>("previous-level").clicked += () =>
        {
            int currentLevel = PlayerPrefs.GetInt("level", 0);
            PlayerPrefs.SetInt("level", Math.Max(0, currentLevel - 1));
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        };
    }

}
