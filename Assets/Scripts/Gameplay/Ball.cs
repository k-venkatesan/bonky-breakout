using UnityEngine;

/// <summary>
/// Ball that rebounds off surfaces
/// </summary>
public class Ball : MonoBehaviour
{
    #region Fields

    // Angle of impulse force (in degrees) with which ball is initialized
    private const float ForceAngleInDegrees = -90;

    // Dynamic Rigidbody 2D component
    private Rigidbody2D rb2D;

    // Timer used to track eclipsed lifetime
    private Timer lifeTimer;

    // State of ball
    private bool isSpeedupActive;
    private Timer speedupTimer;

    #endregion // Fields

    #region Components

    /// <summary>
    /// Angle of impulse force (in radians) with which ball is initialized
    /// </summary>
    private float ForceAngleInRadians => ForceAngleInDegrees * Mathf.Deg2Rad;

    #endregion // Components

    #region Methods

    /// <summary>
    /// Adds listener to speedup effect activation event
    /// </summary>
    private void AddSpeedupEffectListener()
    {
        EventManager.AddSpeedupEffectListener(Speedup);
    }

    /// <summary>
    /// Apply impulse force to ball
    /// </summary>
    private void ApplyImpulseForce()
    {
        // Get force magnitude set in configuation file
        float forceMagnitude = ConfigurationUtils.BallImpulseForce;

        // Set force direction
        Vector2 forceDirection = new Vector2(Mathf.Cos(ForceAngleInRadians), Mathf.Sin(ForceAngleInRadians));

        // Apply impulse force
        GetComponent<Rigidbody2D>().AddForce(forceMagnitude * forceDirection, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Changes direction of motion of ball
    /// </summary>
    /// <param name="newDirection">New direction the ball is to move in</param>
    public void ChangeDirection(Vector2 newDirection)
    {
        // Set velocity of ball based on current speed and new direction
        float speed = rb2D.velocity.magnitude;
        rb2D.velocity = speed * newDirection;
    }

    /// <summary>
    /// Initializes fields with values and references
    /// </summary>
    private void InitializeFields()
    {
        rb2D = GetComponent<Rigidbody2D>();
        lifeTimer = GetComponent<Timer>();
        speedupTimer = gameObject.AddComponent<Timer>();
        isSpeedupActive = false;
    }

    /// <summary>
    /// Monitors speedup status of ball and updates it when speedup timer finishes
    /// </summary>
    private void MonitorSpeedupStatus()
    {
        if (isSpeedupActive && speedupTimer.Finished)
        {
            isSpeedupActive = false;
        }
    }

    /// <summary>
    /// Checks if reason for ball disappearance is because it has fallen below bottom edge of screen.
    /// Destroys the ball and spawns a new one if so.
    /// </summary>
    private void ProcessDisappearance()
    {
        if (transform.position.y < ScreenUtils.ScreenBottom)
        {
            Camera.main.GetComponent<BallSpawner>().RequestNewBall();
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Speeds up ball by given factor for given duration.
    /// If speed-up is already active, the duration is extended by given amount.
    /// </summary>
    /// <param name="speedupFactor">Factor by which to speed up ball</param>
    /// <param name="speedupEffectDuration">Speedup effect duration (in seconds)</param>
    private void Speedup(float speedupFactor, float speedupEffectDuration)
    {
        // Set new speed
        float newSpeed = speedupFactor * rb2D.velocity.magnitude;
        rb2D.velocity = newSpeed * rb2D.velocity.normalized;

        // Set duration of new speed
        if (isSpeedupActive)
        {
            speedupTimer.AddTime(speedupEffectDuration);
        }
        else
        {
            isSpeedupActive = true;
            speedupTimer.Duration = speedupEffectDuration;
            speedupTimer.Run();
        }
    }

    /// <summary>
    /// Starts timer to begin tracking the eclipsed lifetime of the ball
    /// </summary>
    private void StartLifeTimer()
    {
        lifeTimer.Duration = ConfigurationUtils.BallLifetimeInSeconds;
        lifeTimer.Run();
    }

    /// <summary>
    /// Checks if the eclipsed lifetime has surpassed the total lifetime.
    /// Destroys the ball and spawns a new one if so.
    /// </summary>
    private void UpdateLifeStatus()
    {
        if (lifeTimer.Finished)
        {
            Camera.main.GetComponent<BallSpawner>().RequestNewBall();
            Destroy(gameObject);
        }
    }

    #endregion // Methods

    #region MonoBehaviour Messages

    private void Start()
    {
        InitializeFields();
        AddSpeedupEffectListener();
        ApplyImpulseForce();
        StartLifeTimer();
    }

    private void Update()
    {
        MonitorSpeedupStatus();
        UpdateLifeStatus();
    }

    private void OnBecameInvisible()
    {
        ProcessDisappearance();
    }

    #endregion // MonoBehaviour Messages
}
