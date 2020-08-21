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
    /// <value>Paddle move units per second</value>
    public static float BallImpulseForce => configurationData.BallImpulseForce;

    /// <summary>
    /// Gets the total lifetime of balls (in seconds)
    /// </summary>
    /// <value>Paddle move units per second</value>
    public static float BallLifetimeInSeconds => configurationData.BallLifetimeInSeconds;

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
