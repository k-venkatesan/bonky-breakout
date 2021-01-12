using UnityEngine;

/// <summary>
/// Initializes the game
/// </summary>
public class GameInitializer : MonoBehaviour 
{
    #region Fields
    #endregion // Fields

    #region Components
    #endregion // Components

    #region Methods

    /// <summary>
    /// Checks for pressing of 'Esc' key and opens Pause Menu when so
    /// </summary>
    private void CheckForPauseMenuOpening()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            Instantiate(Resources.Load("PauseMenuCanvas"));
        }
    }

    #endregion // Methods

    #region MonoBehaviour Messages

    private void Awake()
    {
        ScreenUtils.Initialize();
        ConfigurationUtils.Initialize();
        RandomNumberGenerator.Initialize();
        EffectUtils.Initialize();
    }

    private void Update()
    {
        CheckForPauseMenuOpening();
    }

    #endregion // MonoBehaviour Messages
}
