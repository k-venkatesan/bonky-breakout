/// <summary>
/// Provides access to configuration data
/// </summary>
public static class ConfigurationUtils
{
    #region Fields

    // Object that reads configuration data
    private static ConfigurationData configurationData;

    #endregion // Fields

    #region Properties

    /// <summary>
    /// Gets the paddle move units per second
    /// </summary>
    /// <value>Paddle move units per second</value>
    public static float PaddleMoveUnitsPerSecond => configurationData.PaddleMoveUnitsPerSecond;

    /// <summary>
    /// Gets the ball impulse force
    /// </summary>
    /// <value>Impulse force</value>
    public static float BallImpulseForce => configurationData.BallImpulseForce;

    /// <summary>
    /// Gets the total lifetime of balls (in seconds)
    /// </summary>
    /// <value>Ball lifetime in seconds</value>
    public static float BallLifetimeInSeconds => configurationData.BallLifetimeInSeconds;

    /// <summary>
    /// Gets the total lifetime (in seconds) of balls
    /// </summary>
    /// <value>Wait duration before ball spawn (in seconds)</value>
    public static float BallSpawnWaitDurationInSeconds => configurationData.BallSpawnWaitDurationInSeconds;

    /// <summary>
    /// Gets the minimum duration (in seconds) between random ball spawns
    /// </summary>
    /// <value>Minimum duration between random ball spawns (in seconds)</value>
    public static int RandomBallSpawnMinDurationInSeconds => configurationData.RandomBallSpawnMinDurationInSeconds;

    /// <summary>
    /// Gets the maximum duration (in seconds) between random ball spawns
    /// </summary>
    /// <value>Maximum duration between random ball spawns (in seconds)</value>
    public static int RandomBallSpawnMaxDurationInSeconds => configurationData.RandomBallSpawnMaxDurationInSeconds;

    #endregion // Properties

    #region Methods

    /// <summary>
    /// Initializes the configuration utils
    /// </summary>
    public static void Initialize()
    {
        configurationData = new ConfigurationData();
    }

    #endregion // Methods
}
