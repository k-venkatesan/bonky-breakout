using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Pause menu of game
/// </summary>
public class GameOverCanvas : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private Text scoreText;

    #endregion // Fields

    #region Properties
    #endregion // Properties

    #region Methods    

    /// <summary>
    /// Displays score shown on HUD
    /// </summary>
    private void DisplayScore()
    {
        scoreText.text = HUD.ScoreTextContents;
    }

    /// <summary>
    /// Handles response to 'Quit' button being clicked
    /// </summary>
    public void HandleQuitButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("MainMenu");
    }

    #endregion // Methods

    #region MonoBehaviour Messages

    private void Start()
    {
        DisplayScore();
    }

    #endregion // MonoBehaviour Messages
}
