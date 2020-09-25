using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    #endregion // Fields

    #region // Properties
    #endregion // Properties

    #region // Methods

    /// <summary>
    /// Adds invoker of freezer activation event
    /// </summary>
    /// <param name="invoker">Invoker of freezer activation event</param>
    public static void AddFreezerEffectInvoker(PickupBlock invoker)
    {
        pickupEffectInvoker = invoker;
        if (freezerEffectListener != null)
        {
            pickupEffectInvoker.AddFreezerEffectListener(freezerEffectListener);
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

    #endregion // Methods
}
