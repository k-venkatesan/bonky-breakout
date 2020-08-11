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
    #endregion // Methods

    #region MonoBehaviour Messages

    private void Awake()
    {
        ScreenUtils.Initialize();
        ConfigurationUtils.Initialize();
    }

    #endregion // MonoBehaviour Messages
}
