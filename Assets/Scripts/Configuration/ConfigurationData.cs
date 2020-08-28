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
    private static int standardBlockValue = 1;
    private static int bonusBlockValue = 5;
    private static int pickupBlockValue = 3;
    private static int standardBlockPercentage = 60;
    private static int bonusBlockPercentage = 20;
    private static int freezerBlockPercentage = 10;
    private static int speedupBlockPercentage = 10;
    private static int totalBallsPerGame = 20;

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

    /// <summary>
    /// Gets the number of points a standard block is worth
    /// </summary>
    /// <value>Number of points a standard block is worth</value>
    public int StandardBlockValue => standardBlockValue;

    /// <summary>
    /// Gets the number of points a bonus block is worth
    /// </summary>
    /// <value>Number of points a bonus block is worth</value>
    public int BonusBlockValue => bonusBlockValue;

    /// <summary>
    /// Gets the number of points a pickup block (freezer/speedup) is worth
    /// </summary>
    /// <value>Number of points a pickup block (freezer/speedup) is worth</value>
    public int PickupBlockValue => pickupBlockValue;

    /// Gets the percentage of standard blocks in the game
    /// </summary>
    /// <value>Percentage of standard blocks in the game</value>
    public int StandardBlockPercentage => standardBlockPercentage;

    /// Gets the percentage of bonus blocks in the game
    /// </summary>
    /// <value>Percentage of bonus blocks in the game</value>
    public int BonusBlockPercentage => bonusBlockPercentage;

    /// Gets the percentage of freezer pickup blocks in the game
    /// </summary>
    /// <value>Percentage of freezer pickup blocks in the game</value>
    public int FreezerBlockPercentage => freezerBlockPercentage;

    /// Gets the percentage of speedup pickup blocks in the game
    /// </summary>
    /// <value>Percentage of speedup pickup blocks in the game</value>
    public int SpeedupBlockPercentage => speedupBlockPercentage;

    /// Gets the total number balls provided per game
    /// </summary>
    /// <value>Total number balls provided per game</value>
    public int TotalBallsPerGame => totalBallsPerGame;

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

            // Set fields using configuration data and verify that they are compatible
            SetConfigurationDataFields(csvConfigurationData);
            VerifyValueCompatibility();
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
        ballSpawnWaitDurationInSeconds = float.Parse(configurationValues[3]);
        randomBallSpawnMinDurationInSeconds = int.Parse(configurationValues[4]);
        randomBallSpawnMaxDurationInSeconds = int.Parse(configurationValues[5]);
        standardBlockValue = int.Parse(configurationValues[6]);
        bonusBlockValue = int.Parse(configurationValues[7]);
        pickupBlockValue = int.Parse(configurationValues[8]);
        standardBlockPercentage = int.Parse(configurationValues[9]);
        bonusBlockPercentage = int.Parse(configurationValues[10]);
        freezerBlockPercentage = int.Parse(configurationValues[11]);
        speedupBlockPercentage = int.Parse(configurationValues[12]);
        totalBallsPerGame = int.Parse(configurationValues[13]);
    }

    /// <summary>
    /// Verifies that there are no values that are incompatible with each other
    /// </summary>
    private void VerifyValueCompatibility()
    {
        // Verify that percentage distribution of blocks add up to exactly 100
        if (standardBlockPercentage
            + bonusBlockPercentage
            + freezerBlockPercentage 
            + speedupBlockPercentage != 100)
        {
            Debug.LogWarning("Percentage distribution of blocks do not add up to 100.");
        }
    }

    #endregion // Methods
}
