using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    #region Fields

    // Prefab of ball that is to be spawned
    [SerializeField]
    private Ball prefabBall;

    // Timer that allows for a pause before spawning ball
    private Timer waitTimer;

    // Flag that instructs whether to spawn ball or not
    private bool isBallToBeSpawned = false;

    #endregion // Fields

    #region Properties
    #endregion // Properties

    #region Methods

    /// <summary>
    /// Initializes ball spawn timer with duration provided in configuration file
    /// </summary>
    private void InitializeTimer()
    {
        waitTimer = gameObject.AddComponent<Timer>();
        waitTimer.Duration = ConfigurationUtils.BallSpawnWaitDurationInSeconds;
    }

    /// <summary>
    /// Processes flag that determines if ball is to be spawned or not
    /// and performs spawning if wait duration is complete
    /// </summary>
    private void ProcessSpawnFlag()
    {
        if (isBallToBeSpawned && waitTimer.Finished)
        {
            Instantiate(prefabBall);
            isBallToBeSpawned = false;
        }
    }

    /// <summary>
    /// Requests a ball that gets spawned after a wait duration set in configuration file
    /// </summary>
    public void RequestNewBall()
    {
        isBallToBeSpawned = true;
        waitTimer.Run();
    }

    #endregion // Methods

    #region MonoBehaviour Messages

    private void Start()
    {
        InitializeTimer();
    }

    private void Update()
    {
        ProcessSpawnFlag();
    }

    #endregion // MonoBehaviour Messages
}
