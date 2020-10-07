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

    /// <summary>
    /// Gets the number of points a standard block is worth
    /// </summary>
    /// <value>Number of points a standard block is worth</value>
    public static int StandardBlockValue => configurationData.StandardBlockValue;

    /// <summary>
    /// Gets the number of points a bonus block is worth
    /// </summary>
    /// <value>Number of points a bonus block is worth</value>
    public static int BonusBlockValue => configurationData.BonusBlockValue;

    /// <summary>
    /// Gets the number of points a pickup block (freezer/speedup) is worth
    /// </summary>
    /// <value>Number of points a pickup block (freezer/speedup) is worth</value>
    public static int PickupBlockValue => configurationData.PickupBlockValue;

    /// <summary>
    /// Gets the percentage of standard blocks in the game
    /// </summary>
    /// <value>Percentage of standard blocks in the game</value>
    public static float StandardBlockPercentage => configurationData.StandardBlockPercentage;

    /// <summary>
    /// Gets the percentage of bonus blocks in the game
    /// </summary>
    /// <value>Percentage of bonus blocks in the game</value>
    public static float BonusBlockPercentage => configurationData.BonusBlockPercentage;

    /// <summary>
    /// Gets the percentage of freezer pickup blocks in the game
    /// </summary>
    /// <value>Percentage of freezer pickup blocks in the game</value>
    public static float FreezerBlockPercentage => configurationData.FreezerBlockPercentage;

    /// <summary>
    /// Gets the percentage of speedup pickup blocks in the game
    /// </summary>
    /// <value>Percentage of speedup pickup blocks in the game</value>
    public static float SpeedupBlockPercentage => configurationData.SpeedupBlockPercentage;

    /// <summary>
    /// Gets the total number balls provided per game
    /// </summary>
    /// <value>Total number balls provided per game</value>
    public static int TotalBallsPerGame => configurationData.TotalBallsPerGame;

    /// <summary>
    /// Gets the duration (in seconds) for which the freezer effect lasts
    /// </summary>
    /// <value>Duration for which freezer effect lasts (in seconds)</value>
    public static float FreezerEffectDurationInSeconds => configurationData.FreezerEffectDurationInSeconds;

    /// <summary>
    /// Gets the duration (in seconds) for which the speedup effect lasts
    /// </summary>
    /// <value>Duration for which speedup effect lasts (in seconds)</value>
    public static float SpeedupEffectDurationInSeconds => configurationData.SpeedupEffectDurationInSeconds;

    /// <summary>
    /// Gets the factor by which the ball is speeded-up by when the speedup effect is active
    /// </summary>
    /// <value>Factor by which ball is speeded-up during speedup effect</value>
    public static float SpeedupFactor => configurationData.SpeedupFactor;

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
