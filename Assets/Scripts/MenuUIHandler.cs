using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] public Text bestScoreText;
    [SerializeField] public InputField nameField;

    private void Start()
    {
        nameField.Select();
        nameField.ActivateInputField();
    }
    
    private void OnGUI()
    {
        nameField.text = HighScoreManager.Instance.PlayerName;
        bestScoreText.text = "Best Score : " + HighScoreManager.Instance.ChampionName + " : " + HighScoreManager.Instance.ChampionScore;
    }

    public void SetActivePlayer()
    {
        HighScoreManager.Instance.PlayerName = nameField.text;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

}
