using UnityEngine;

/// <summary>
/// Initializes and monitors the game
/// </summary>
public class GameInitializer : MonoBehaviour 
{
    #region Fields
    #endregion // Fields

    #region Components
    #endregion // Components

    #region Methods
    
    /// <summary>
    /// Adds listener for last ball used eve
    /// </summary>
    private void AddLastBallUsedListener()
    {
        EventManager.AddLastBallUsageListener(EndGame);
    }

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

    /// <summary>
    /// Ends the game and loads the Game Over menu
    /// </summary>
    private void EndGame()
    {
        // Pause all physics behaviours and load Game Over menu
        Time.timeScale = 0;
        Instantiate(Resources.Load("GameOverCanvas"));

        // Hide HUD
        FindObjectOfType<HUD>().gameObject.SetActive(false);

        // Destroy balls
        foreach (Ball ball in FindObjectsOfType<Ball>())
        {
            Destroy(ball.gameObject);
        }

        // Stop checking for pause menu opening by disabling Update() loop
        enabled = false;
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

    private void Start()
    {
        AddLastBallUsedListener();
    }

    private void Update()
    {
        CheckForPauseMenuOpening();
    }

    #endregion // MonoBehaviour Messages
}
