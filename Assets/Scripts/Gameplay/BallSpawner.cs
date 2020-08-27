using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    #region Fields

    // Prefab of ball that is to be spawned
    [SerializeField]
    private Ball prefabBall;

    // Timer that allows for a pause before spawning ball
    private Timer waitTimer;

    // Timer that allows for spawning balls at random intervals (within configured range)
    private Timer randomSpawnTimer;

    // Flag that instructs whether to spawn ball or not
    private bool isBallToBeSpawned = false;

    #endregion // Fields

    #region Properties
    #endregion // Properties

    #region Methods

    /// <summary>
    /// Initializes timers that control spawning of balls
    /// </summary>
    private void InitializeTimers()
    {
        // Initialize wait timer
        waitTimer = gameObject.AddComponent<Timer>();
        waitTimer.Duration = ConfigurationUtils.BallSpawnWaitDurationInSeconds;

        // Initialize random spawn timer
        randomSpawnTimer = gameObject.AddComponent<Timer>();
        randomSpawnTimer.Duration = RandomNumberGenerator.RandomNumberInRange(ConfigurationUtils.RandomBallSpawnMinDurationInSeconds, ConfigurationUtils.RandomBallSpawnMaxDurationInSeconds);
        randomSpawnTimer.Run();
    }

    /// <summary>
    /// Monitors timer that controls random spawning of balls
    /// </summary>
    private void MonitorRandomSpawnTimer()
    {
        if (randomSpawnTimer.Finished)
        {
            // Spawn ball
            Instantiate(prefabBall);

            // Reset timer
            randomSpawnTimer.Duration = RandomNumberGenerator.RandomNumberInRange(ConfigurationUtils.RandomBallSpawnMinDurationInSeconds, ConfigurationUtils.RandomBallSpawnMaxDurationInSeconds);
            randomSpawnTimer.Run();
        }
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
        InitializeTimers();
    }

    private void Update()
    {
        MonitorRandomSpawnTimer();
        ProcessSpawnFlag();
    }

    #endregion // MonoBehaviour Messages
}
