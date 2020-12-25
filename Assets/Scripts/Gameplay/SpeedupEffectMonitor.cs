using UnityEngine;

/// <summary>
/// Monitors status of speedup effect
/// </summary>
public class SpeedupEffectMonitor : MonoBehaviour
{
    #region Fields

    // Status of speedup effect
    private bool isSpeedupEffectActive;

    #endregion // Fields

    #region Properties

    /// <summary>
    /// Status of speedup effect
    /// </summary>
    public bool IsSpeedupEffectActive
    {
        get { return isSpeedupEffectActive; }
        private set 
        {
            isSpeedupEffectActive = value;
            HasSpeedupEffectStatusChanged = true;
        }
    }

    /// <summary>
    /// Whether speedup effect has changed since last update
    /// </summary>
    public bool HasSpeedupEffectStatusChanged { get; set; }

    /// <summary>
    /// Current speedup factor
    /// </summary>
    public float SpeedupFactor { get; private set; }

    /// <summary>
    /// Remaining duration of speedup effect
    /// </summary>
    public float SpeedupEffectDuration { get; private set; }

    #endregion // Properties

    #region Methods

    /// <summary>
    /// Activates the speedup effect
    /// </summary>
    /// <param name="speedupFactor">Factor by which to speedup ball</param>
    /// <param name="speedupEffectDuration">Duration for which speedup effect is to be applied</param>
    private void ActivateSpeedupEffect(float speedupFactor, float speedupEffectDuration)
    {
        if (isSpeedupEffectActive == false)
        {
            IsSpeedupEffectActive = true;
            this.SpeedupFactor = speedupFactor;
            this.SpeedupEffectDuration = speedupEffectDuration;
        }
        else
        {
            this.SpeedupEffectDuration += speedupEffectDuration;
        }
    }

    /// <summary>
    /// Deactivates the speedup effect
    /// </summary>
    private void DeactivateSpeedupEffect()
    {
        IsSpeedupEffectActive = false;
        SpeedupEffectDuration = 0;
    }

    /// <summary>
    /// Initializes fields and adds event listener
    /// </summary>
    private void Initialize()
    {
        isSpeedupEffectActive = false;
        HasSpeedupEffectStatusChanged = false;
        SpeedupFactor = 1;
        SpeedupEffectDuration = 0;
        EventManager.AddSpeedupEffectListener(ActivateSpeedupEffect);
    }

    /// <summary>
    /// Updates timer that keeps speedup effect running
    /// </summary>
    private void UpdateTimer()
    {
        if (isSpeedupEffectActive)
        {
            SpeedupEffectDuration -= Time.deltaTime;
            if (SpeedupEffectDuration <= 0)
            {
                DeactivateSpeedupEffect();
            }
        }
    }

    #endregion // Methods

    #region MonoBehaviour Messages

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    #endregion // MonoBehaviour Messages
}
