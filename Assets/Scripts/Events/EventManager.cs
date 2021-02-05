using UnityEngine.Events;

/// <summary>
/// Manages invokers and listeners of events
/// </summary>
public static class EventManager
{
    #region Fields

    // Invoker and listeners for pickup effect activation events
    private static PickupBlock pickupEffectInvoker;
    private static UnityAction<float> freezerEffectListener;
    private static UnityAction<float, float> speedupEffectListener;

    // Invoker and listener for points addition event
    private static Block pointsAddedInvoker;
    private static UnityAction<int> pointsAddedListener;

    // Invoker and listener for ball removal event
    private static BallSpawner ballRemovedInvoker;
    private static UnityAction ballRemovedListener;

    #endregion // Fields

    #region // Properties
    #endregion // Properties

    #region // Methods

    /// <summary>
    /// Adds invoker of pickup effect activation events
    /// </summary>
    /// <param name="invoker">Invoker of pickup effect activation events</param>
    public static void AddPickupEffectInvoker(PickupBlock invoker)
    {
        pickupEffectInvoker = invoker;
        if (freezerEffectListener != null)
        {
            pickupEffectInvoker.AddFreezerEffectListener(freezerEffectListener);
        }
        if (speedupEffectListener != null)
        {
            pickupEffectInvoker.AddSpeedupEffectListener(speedupEffectListener);
        }
    }

    /// <summary>
    /// Adds listener to freezer activation event
    /// </summary>
    /// <param name="listener">Listener to freezer activation event</param>
    public static void AddFreezerEffectListener(UnityAction<float> listener)
    {
        freezerEffectListener = listener;
        if (pickupEffectInvoker != null)
        {
            pickupEffectInvoker.AddFreezerEffectListener(freezerEffectListener);
        }
    }

    /// <summary>
    /// Adds listener to speedup activation event
    /// </summary>
    /// <param name="listener">Listener to speedup activation event</param>
    public static void AddSpeedupEffectListener(UnityAction<float, float> listener)
    {
        speedupEffectListener = listener;
        if (pickupEffectInvoker != null)
        {
            pickupEffectInvoker.AddSpeedupEffectListener(speedupEffectListener);
        }
    }

    /// <summary>
    /// Adds invoker of points addition event
    /// </summary>
    /// <param name="invoker">Invoker of points addition event</param>
    public static void AddPointsAddedInvoker(Block invoker)
    {
        pointsAddedInvoker = invoker;
        if (pointsAddedListener != null)
        {
            pointsAddedInvoker.AddPointsAdditionListener(pointsAddedListener);
        }
    }

    /// <summary>
    /// Adds listener for points addition event
    /// </summary>
    /// <param name="listener">Listener for points addition event</param>
    public static void AddPointsAddedListener(UnityAction<int> listener)
    {
        pointsAddedListener = listener;
        if (pointsAddedInvoker != null)
        {
            pointsAddedInvoker.AddPointsAdditionListener(pointsAddedListener);
        }
    }

    /// <summary>
    /// Adds invoker of ball removal event
    /// </summary>
    /// <param name="invoker">Invoker of ball removal event</param>
    public static void AddBallRemovedInvoker(BallSpawner invoker)
    {
        ballRemovedInvoker = invoker;
        if (ballRemovedListener != null)
        {
            ballRemovedInvoker.AddBallRemovalListener(ballRemovedListener);
        }
    }

    /// <summary>
    /// Adds listener for ball removal event
    /// </summary>
    /// <param name="listener">Listener for ball removal event</param>
    public static void AddBallRemovedListener(UnityAction listener)
    {
        ballRemovedListener = listener;
        if (ballRemovedInvoker != null)
        {
            ballRemovedInvoker.AddBallRemovalListener(ballRemovedListener);
        }
    }

    #endregion // Methods
}
