using UnityEngine;

/// <summary>
/// Provides information on global status effects
/// </summary>
public static class EffectUtils
{
    // Monitors speedup effect
    private static SpeedupEffectMonitor speedupEffectMonitor;

    /// <summary>
    /// Whether the speedup effect is active or not
    /// </summary>
    public static bool IsSpeedupEffectActive => speedupEffectMonitor.IsSpeedupEffectActive;

    /// <summary>
    /// Whether the speedup effect status has changed since last update
    /// </summary>
    public static bool HasSpeedupEffectStatusChanged
    {
        get { return speedupEffectMonitor.HasSpeedupEffectStatusChanged; }
        set { speedupEffectMonitor.HasSpeedupEffectStatusChanged = value; }
    }

    /// <summary>
    /// Current speedup factor
    /// </summary>
    public static float SpeedupFactor => speedupEffectMonitor.SpeedupFactor;

    /// <summary>
    /// Initializes speeedup effect monitor
    /// </summary>
    public static void Initialize()
    {
        speedupEffectMonitor = Camera.main.GetComponent<SpeedupEffectMonitor>();
    }
}
