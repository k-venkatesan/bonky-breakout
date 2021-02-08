using UnityEngine;
using UnityEngine.Events;

public class PickupBlock : Block
{
    #region Fields

    // Freezer and speedup sprites
    [SerializeField]
    private Sprite freezerSprite;
    [SerializeField]
    private Sprite speedupSprite;

    // Pickup effect type, duration and factor
    private PickupEffect pickupEffect;
    private float pickupEffectDuration;
    private float speedupFactor;

    // Pickup effect events
    private FreezerEffectActivated freezerEffectActivated;
    private SpeedupEffectActivated speedupEffectActivated;

    #endregion // Fields

    #region Properties

    /// <summary>
    /// Sets pickup effect of block 
    /// </summary>
    public PickupEffect PickupEffect
    {
        set => pickupEffect = value;
    }

    #endregion // Properties

    #region Methods

    /// <summary>
    /// Adds listener to freezer effect activation event
    /// </summary>
    /// <param name="listener">Listener to freezer effect activation event</param>
    public void AddFreezerEffectListener(UnityAction<float> listener)
    {
        freezerEffectActivated?.AddListener(listener);
    }

    /// <summary>
    /// Adds listener to speedup effect activation event
    /// </summary>
    /// <param name="listener">Listener to speedup effect activation event</param>
    public void AddSpeedupEffectListener(UnityAction<float, float> listener)
    {
        speedupEffectActivated?.AddListener(listener);
    }

    /// <summary>
    /// Applies sprite, duration and event invoker for pickup effect
    /// </summary>
    private void ApplyPickupEffectFeatures()
    {
        // Apply pickup effect features
        switch (pickupEffect)
        {
            case PickupEffect.Freezer:
                GetComponent<SpriteRenderer>().sprite = freezerSprite;
                pickupEffectDuration = ConfigurationUtils.FreezerEffectDurationInSeconds;
                freezerEffectActivated = new FreezerEffectActivated();
                break;

            case PickupEffect.Speedup:
                GetComponent<SpriteRenderer>().sprite = speedupSprite;
                pickupEffectDuration = ConfigurationUtils.SpeedupEffectDurationInSeconds;
                speedupFactor = ConfigurationUtils.SpeedupFactor;
                speedupEffectActivated = new SpeedupEffectActivated();
                break;

            default:
                Debug.LogWarning("Effect not correctly assigned to pickup block");
                break;
        }

        // Add self as pickup effect invoker
        EventManager.AddPickupEffectInvoker(this);
    }

    /// <summary>
    /// Retrieves and assigns the points value of a pickup block (freezer/speedup) from the configuration file
    /// </summary>
    private void AssignPointsValue()
    {
        pointsWorth = ConfigurationUtils.PickupBlockValue;
    }

    /// <summary>
    /// Invokes pickup effect activation and processes other standard Block collision effects
    /// </summary>
    /// <param name="collision">Object containing information about collision</param>
    private void ProcessCollision(Collision2D collision)
    {
        // Invoke pickup effect activation
        switch (pickupEffect)
        {
            case PickupEffect.Freezer:
                freezerEffectActivated.Invoke(pickupEffectDuration);
                break;

            case PickupEffect.Speedup:
                speedupEffectActivated.Invoke(pickupEffectDuration, speedupFactor);
                break;
        }

        // Continue with rest of collision processing
        base.OnCollisionEnter2D(collision);
    }

    /// <summary>
    /// Verifies that serialized fields are filled and logs warnings where they are not
    /// </summary>
    private void VerifySerializedFields()
    {
        if (freezerSprite == null || speedupSprite == null)
        {
            Debug.LogWarning("One or both sprites are missing. Please populate fields in Inspector window.");
        }
    }

    #endregion // Methods

    #region MonoBehaviour Messages

    private void Awake()
    {
        VerifySerializedFields();
    }

    protected override void Start()
    {
        base.Start();

        AssignPointsValue();
        ApplyPickupEffectFeatures();
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        ProcessCollision(collision);
    }

    #endregion // MonoBehaviour Messages
}
