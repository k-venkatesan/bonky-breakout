using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ball that rebounds off surfaces
/// </summary>
public class Ball : MonoBehaviour
{
    #region Fields
    #endregion // Fields

    #region Components
    #endregion // Components

    #region Methods

    /// <summary>
    /// Apply impulse force to ball
    /// </summary>
    private void ApplyImpulseForce()
    {
        // Get force magnitude set in configuation file
        float forceMagnitude = ConfigurationUtils.BallImpulseForce;

        // Set force direction
        const float ForceAngleInDegrees = 20;
        float forceAngleInRadians = ForceAngleInDegrees * Mathf.Deg2Rad;
        Vector2 forceDirection = new Vector2(Mathf.Cos(forceAngleInRadians), Mathf.Sin(forceAngleInRadians));

        // Apply impulse force
        GetComponent<Rigidbody2D>().AddForce(forceMagnitude * forceDirection, ForceMode2D.Impulse);
    }

    #endregion // Methods

    #region MonoBehaviour Messages

    private void Start()
    {
        ApplyImpulseForce();
    }

    #endregion // MonoBehaviour Messages
}
