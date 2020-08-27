﻿using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    #region Fields

    // Prefab of ball that is to be spawned
    [SerializeField]
    private Ball prefabBall;

    // Opposite corners of ball spawn location to check for potential collision
    private Vector2 ballSpawnLocationBottomLeftCorner;
    private Vector2 ballSpawnLocationTopRightCorner;

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
    /// Calculates bottom-left and top-right points of square area where balls spawn
    /// </summary>
    private void CalculateBallSpawnLocationCorners()
    {
        CircleCollider2D ballCollider = prefabBall.GetComponent<CircleCollider2D>();
        ballSpawnLocationBottomLeftCorner.x = ballCollider.transform.position.x - ballCollider.radius;
        ballSpawnLocationBottomLeftCorner.y = ballCollider.transform.position.y - ballCollider.radius;
        ballSpawnLocationTopRightCorner.x = ballCollider.transform.position.x + ballCollider.radius;
        ballSpawnLocationTopRightCorner.y = ballCollider.transform.position.y + ballCollider.radius;
    }

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
        // Check if timer has finished and that there is no object already present in spawn location
        if (randomSpawnTimer.Finished 
            && Physics2D.OverlapArea(ballSpawnLocationBottomLeftCorner, ballSpawnLocationTopRightCorner) == null)
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
    /// </summary>
    private void ProcessSpawnFlag()
    {
        // Check if a ball is to be spawned, the wait timer has finished, and that there is no object already present in spawn location
        if (isBallToBeSpawned 
            && waitTimer.Finished 
            && Physics2D.OverlapArea(ballSpawnLocationBottomLeftCorner, ballSpawnLocationTopRightCorner) == null)
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
        CalculateBallSpawnLocationCorners();
    }

    private void Update()
    {
        MonitorRandomSpawnTimer();
        ProcessSpawnFlag();
    }

    #endregion // MonoBehaviour Messages
}