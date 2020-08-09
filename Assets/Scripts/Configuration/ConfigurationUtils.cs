﻿/// <summary>
/// Provides access to configuration data
/// </summary>
public static class ConfigurationUtils
{
    #region Fields
    #endregion / /Fields

    #region Properties

    /// <summary>
    /// Gets the paddle move units per second
    /// </summary>
    /// <value>Paddle move units per second</value>
    public static float PaddleMoveUnitsPerSecond
    {
        get { return 10; }
    }

    /// <summary>
    /// Gets the ball impulse force
    /// </summary>
    /// <value>Paddle move units per second</value>
    public static float BallImpulseForce
    {
        get { return 10; }
    }

    #endregion // Properties

    #region Methods

    /// <summary>
    /// Initializes the configuration utils
    /// </summary>
    public static void Initialize()
    {

    }

    #endregion // Methods
}
