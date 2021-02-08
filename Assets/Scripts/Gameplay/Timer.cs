using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A timer
/// </summary>
public class Timer : MonoBehaviour
{
	#region Fields
	
	// Timer duration
	private float totalSeconds = 0;

	// Timer execution
	private float elapsedSeconds = 0;
	private bool running = false;
	
	// Support for Finished property
	bool started = false;

	// Timer completion event
	TimerFinished timerFinished;

    #endregion // Fields

    #region Properties

    /// <summary>
    /// Sets the duration of the timer.
    /// The duration can only be set if the timer isn't currently running
    /// </summary>
    /// <value>duration</value>
    public float Duration
    {
		set
        {
			if (!running)
            {
				totalSeconds = value;
			}
		}
	}
	
	/// <summary>
	/// Gets whether or not the timer is currently running
	/// </summary>
	/// <value>true if running; otherwise, false.</value>
	public bool Running
    {
		get { return running; }
	}

	#endregion // Properties

	#region Methods

	/// <summary>
	/// Add listener for timer completion
	/// </summary>
	/// <param name="listener"></param>
	public void AddTimerCompletionListener(UnityAction listener)
    {
		timerFinished.AddListener(listener);
    }

	/// <summary>
	/// Adds time on top of existing duration
	/// </summary>
	/// <param name="seconds">Number of seconds to add onto timer</param>
	public void AddTime(float seconds)
    {
		totalSeconds += seconds;
    }

	/// <summary>
	/// Initializes events pertaining to the timer
	/// </summary>
	private void InitializeEvents()
    {
		timerFinished = new TimerFinished();
	}

	/// <summary>
	/// Runs the timer.
	/// Because a timer of 0 duration doesn't really make sense,
	/// the timer only runs if the total seconds is larger than 0.
	/// This also makes sure the consumer of the class has actually 
	/// set the duration to something higher than 0.
	/// </summary>
	public void Run()
	{
		// Only run with valid duration
		if (totalSeconds > 0)
		{
			started = true;
			running = true;
			elapsedSeconds = 0;
		}
	}

	/// <summary>
	/// Updates timer if running and checks if finished
	/// </summary>
	private void UpdateStatus()
    {
		if (running)
		{
			elapsedSeconds += Time.deltaTime;
			if (elapsedSeconds >= totalSeconds)
			{
				running = false;
				timerFinished.Invoke();
			}
		}
	}

    #endregion // Methods

    #region MonoBehaviour Messages

    private void Awake()
    {
		InitializeEvents();
    }

    private void Update()
    {
		UpdateStatus();		
	}
	
	#endregion // MonoBehaviour Messages
}
