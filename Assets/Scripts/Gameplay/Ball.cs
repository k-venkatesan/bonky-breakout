﻿using UnityEngine;

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

    #endregion // Fields

    #region Components

    /// <summary>
    /// Angle of impulse force (in radians) with which ball is initialized
    /// </summary>
    private float ForceAngleInRadians => ForceAngleInDegrees * Mathf.Deg2Rad;

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
    /// Retrieves necessary values from and references to components
    /// </summary>
    private void RetrieveValuesAndReferences()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    #endregion // Methods

    #region MonoBehaviour Messages

    private void Start()
    {
        RetrieveValuesAndReferences();
        ApplyImpulseForce();
    }

    #endregion // MonoBehaviour Messages
}