using System.IO;
using UnityEngine;

/// <summary>
/// A container for the configuration data
/// </summary>
public class ConfigurationData
{
    #region Fields

    private const string ConfigurationDataFilePath = "/ConfigurationData.csv";

    // Configuration data with default values
    private static float paddleMoveUnitsPerSecond = 10;
    private static float ballImpulseForce = 10;
    private static float ballLifetimeInSeconds = 10;
    private static float ballSpawnWaitDurationInSeconds = 1;
    private static int randomBallSpawnMinDurationInSeconds = 5;
    private static int randomBallSpawnMaxDurationInSeconds = 10;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the paddle move units per second
    /// </summary>
    /// <value>Paddle move units per second</value>
    public float PaddleMoveUnitsPerSecond => paddleMoveUnitsPerSecond;

    /// <summary>
    /// Gets the impulse force to apply to move balls
    /// </summary>
    /// <value>Impulse force</value>
    public float BallImpulseForce => ballImpulseForce;

    /// <summary>
    /// Gets the total lifetime (in seconds) of balls
    /// </summary>
    /// <value>Ball lifetime in seconds</value>
    public float BallLifetimeInSeconds => ballLifetimeInSeconds;

    /// <summary>
    /// Gets the duration (in seconds) to wait between a ball being destroyed
    /// and a new one being spawned
    /// </summary>
    /// <value>Wait duration before ball spawn (in seconds)</value>
    public float BallSpawnWaitDurationInSeconds => ballSpawnWaitDurationInSeconds;

    /// <summary>
    /// Gets the minimum duration (in seconds) between random ball spawns
    /// </summary>
    /// <value>Minimum duration between random ball spawns (in seconds)</value>
    public int RandomBallSpawnMinDurationInSeconds => randomBallSpawnMinDurationInSeconds;

    /// <summary>
    /// Gets the maximum duration (in seconds) between random ball spawns
    /// </summary>
    /// <value>Maximum duration between random ball spawns (in seconds)</value>
    public int RandomBallSpawnMaxDurationInSeconds => randomBallSpawnMaxDurationInSeconds;

    #endregion

    #region Constructor

    /// <summary>
    /// Constructor
    /// Reads configuration data from a file. If the file
    /// read fails, the object contains default values for
    /// the configuration data
    /// </summary>
    public ConfigurationData()
    {
        // Initialize stream to read configuration data file
        StreamReader input = null;

        // Attempt to read configuration data file and handle exceptions if they arise
        try
        {
            // Obtain data from CSV file            
            input = File.OpenText(Application.streamingAssetsPath + ConfigurationDataFilePath);
            string csvConfigurationData = input.ReadLine();

            // Set fields using configuration data
            SetConfigurationDataFields(csvConfigurationData);
        }
        catch
        {
        }
        finally
        {
            if (input != null)
            {
                input.Close();
            }
        }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Sets configuration data fields using CSV data
    /// </summary>
    /// <param name="csvConfigurationData">Configuration values in CSV format</param>
    private void SetConfigurationDataFields(string csvConfigurationData)
    {
        // Get array of configuration values from CSV data
        string[] configurationValues = csvConfigurationData.Split(',');

        // Set fields using array of configuration values
        paddleMoveUnitsPerSecond = float.Parse(configurationValues[0]);
        ballImpulseForce = float.Parse(configurationValues[1]);
        ballLifetimeInSeconds = float.Parse(configurationValues[2]);
        randomBallSpawnMinDurationInSeconds = int.Parse(configurationValues[3]);
        randomBallSpawnMaxDurationInSeconds = int.Parse(configurationValues[4]);
    }

    #endregion // Methods
}
