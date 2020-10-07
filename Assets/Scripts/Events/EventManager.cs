﻿using UnityEngine.Events;

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
        else if (speedupEffectListener != null)
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

    #endregion // Methods
}