using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Main menu of game
/// </summary>
public class MainMenu : MonoBehaviour
{
    #region Fields
    #endregion // Fields

    #region Properties
    #endregion // Properties

    #region Methods

    /// <summary>
    /// Handles response to 'Help' button being clicked
    /// </summary>
    public void HandleHelpButtonClick()
    {
        gameObject.SetActive(false);
        Instantiate(Resources.Load("HelpCanvas"));
    }

    /// <summary>
    /// Handles response to 'Play' button being clicked
    /// </summary>
    public void HandlePlayButtonClick()
    {
        SceneManager.LoadSceneAsync("GamePlay");
    }

    /// <summary>
    /// Handles response to 'Quit' button being clicked
    /// </summary>
    public void HandleQuitButtonClick()
    {
        Application.Quit();
    }

    #endregion // Methods

    #region MonoBehaviour Messages
    #endregion // MonoBehaviour Messages
}
