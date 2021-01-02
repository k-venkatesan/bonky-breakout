using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Help canvas opened from main menu
/// </summary>
public class HelpCanvas : MonoBehaviour
{
    #region Fields
    #endregion // Fields

    #region Properties
    #endregion // Properties

    #region Methods

    /// <summary>
    /// Handles response to 'Back' button being clicked
    /// </summary>
    public void HandleBackButtonClick()
    {
        SceneManager.LoadSceneAsync("MainMenu");
        Destroy(gameObject);
    }

    #endregion // Methods

    #region MonoBehaviour Messages
    #endregion // MonoBehaviour Messages
}
