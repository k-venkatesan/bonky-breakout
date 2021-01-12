using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Pause menu of game
/// </summary>
public class PauseMenu : MonoBehaviour
{
    #region Fields
    #endregion // Fields

    #region Properties
    #endregion // Properties

    #region Methods    

    /// <summary>
    /// Handles response to 'Quit' button being clicked
    /// </summary>
    public void HandleQuitButtonClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync("MainMenu");
    }

    /// <summary>
    /// Handles response to 'Resume' button being clicked
    /// </summary>
    public void HandleResumeButtonClick()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
    }

    #endregion // Methods

    #region MonoBehaviour Messages
    #endregion // MonoBehaviour Messages
}
