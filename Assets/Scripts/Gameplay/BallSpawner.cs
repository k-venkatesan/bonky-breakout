using UnityEngine;
using UnityEngine.Events;

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

    // Ball removal event
    private BallRemoved ballRemoved;

    #endregion // Fields

    #region Properties
    #endregion // Properties

    #region Methods

    /// <summary>
    /// Adds listener for ball removal event
    /// </summary>
    /// <param name="listener">Listener for ball removal event</param>
    public void AddBallRemovalListener(UnityAction listener)
    {
        ballRemoved.AddListener(listener);
    }

    /// <summary>
    /// Adds listener for ball disappearance event
    /// </summary>
    private void AddBallDisappearedListener()
    {
        EventManager.AddBallDisappearedListener(RequestNewBall);
    }

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
    /// Checks if new ball is required and spawns one if so
    /// </summary>
    private void CheckIfNewBallIsRequired()
    {
        // Check if that there is no object already present in spawn location
        if (Physics2D.OverlapArea(ballSpawnLocationBottomLeftCorner, ballSpawnLocationTopRightCorner) == null)
        {
            // Spawn ball
            SpawnNewBall();

            // Reset timer
            randomSpawnTimer.Duration = RandomNumberGenerator.RandomNumberInRange(ConfigurationUtils.RandomBallSpawnMinDurationInSeconds, ConfigurationUtils.RandomBallSpawnMaxDurationInSeconds);
            randomSpawnTimer.Run();
        }
    }

    /// <summary>
    /// Initializes events pertaining to ball spawner
    /// </summary>
    private void InitializeEvents()
    {
        ballRemoved = new BallRemoved();
        EventManager.AddBallRemovedInvoker(this);
    }

    /// <summary>
    /// Initializes timers that control spawning of balls
    /// </summary>
    private void InitializeTimers()
    {
        // Initialize wait timer
        waitTimer = gameObject.AddComponent<Timer>();
        waitTimer.Duration = ConfigurationUtils.BallSpawnWaitDurationInSeconds;
        waitTimer.AddTimerCompletionListener(ProcessSpawnFlag);

        // Initialize random spawn timer
        randomSpawnTimer = gameObject.AddComponent<Timer>();
        randomSpawnTimer.Duration = RandomNumberGenerator.RandomNumberInRange(ConfigurationUtils.RandomBallSpawnMinDurationInSeconds, ConfigurationUtils.RandomBallSpawnMaxDurationInSeconds);
        randomSpawnTimer.Run();
        randomSpawnTimer.AddTimerCompletionListener(CheckIfNewBallIsRequired);
    }

    /// <summary>
    /// Processes flag that determines if ball is to be spawned or not
    /// </summary>
    private void ProcessSpawnFlag()
    {
        // Check if a ball is to be spawned and that there is no object already present in spawn location
        if (isBallToBeSpawned 
            && Physics2D.OverlapArea(ballSpawnLocationBottomLeftCorner, ballSpawnLocationTopRightCorner) == null)
        {
            SpawnNewBall();
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

    /// <summary>
    /// Spawns a new ball and updates count of balls left
    /// </summary>
    private void SpawnNewBall()
    {
        Instantiate(prefabBall);
        ballRemoved.Invoke();
    }

    /// <summary>
    /// Verifies that serialized fields are filled and logs warnings where they are not
    /// </summary>
    private void VerifySerializedFields()
    {
        if (prefabBall == null)
        {
            Debug.LogWarning("Ball prefab not found. Please populate field in Inspector window.");
        }
    }

    #endregion // Methods

    #region MonoBehaviour Messages

    private void Awake()
    {
        VerifySerializedFields();
        InitializeEvents();
    }

    private void Start()
    {
        InitializeTimers();
        CalculateBallSpawnLocationCorners();
        AddBallDisappearedListener();
        RequestNewBall();
    }

    #endregion // MonoBehaviour Messages
}
