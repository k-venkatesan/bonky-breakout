using System;
using UnityEngine;

/// <summary>
/// Paddle used to deflect balls
/// </summary>
public class Paddle : MonoBehaviour
{
    #region Fields

    // Kinematic Rigidbody2D component
    private Rigidbody2D rb2D;

    // Half-width of BoxCollider2D component
    private float colliderHalfWidth;

    // Maximum angle (in degrees) on either side of vertical that ball can rebound off at
    private const float MaxReboundHalfAngleInDegrees = 60;

    // State of paddle
    private bool isFrozen;
    private Timer freezeTimer;

    #endregion // Fields

    #region Properties

    /// <summary>
    /// Maximum angle (in radians) on either side of vertical that ball can rebound off at
    /// </summary>
    private float MaxReboundHalfAngleInRadians => MaxReboundHalfAngleInDegrees * Mathf.Deg2Rad;

    #endregion // Properties

    #region Methods

    /// <summary>
    /// Adds listener to freezer effect activation event
    /// </summary>
    private void AddFreezerEffectListener()
    {
        EventManager.AddFreezerEffectListener(Freeze);
    }

    /// <summary>
    /// Calculates valid position on X-axis to move paddle to based on attempted position and screen boundaries
    /// </summary>
    /// <param name="attemptedPositionX">Position on X-axis that centre of paddle attempts to move to</param>
    /// <returns>Attempted position if both ends of paddle will remain within screen boundaries; Position at edge of boundary if not</returns>
    private float CalculateClampedX(float attemptedPositionX)
    {
        // Check if each end of paddle is attempting to move past corresponding edge of screen
        if (attemptedPositionX < ScreenUtils.ScreenLeft + colliderHalfWidth)
        {
            return ScreenUtils.ScreenLeft + colliderHalfWidth;
        }
        else if (attemptedPositionX > ScreenUtils.ScreenRight - colliderHalfWidth)
        {
            return ScreenUtils.ScreenRight - colliderHalfWidth;
        }
        else
        {
            return attemptedPositionX;
        }
    }

    /// <summary>
    /// Freezes paddle in place for given duration.
    /// If paddle is already frozen, the duration is extended by given amount.
    /// </summary>
    /// <param name="freezerEffectDuration">Freezer effect duration (in seconds)</param>
    private void Freeze(float freezerEffectDuration)
    {
        if (isFrozen)
        {
            freezeTimer.AddTime(freezerEffectDuration);
        }
        else
        {
            isFrozen = true;
            freezeTimer.Duration = freezerEffectDuration;
            freezeTimer.Run();
        }
    }

    /// <summary>
    /// Initializes fields with values and references
    /// </summary>
    private void InitializeFields()
    {
        rb2D = GetComponent<Rigidbody2D>();
        colliderHalfWidth = GetComponent<BoxCollider2D>().size[0] / 2;
        freezeTimer = gameObject.AddComponent<Timer>();
        isFrozen = false;
    }

    /// <summary>
    /// Monitors freeze status of paddle and updates it when freeze timer finishes
    /// </summary>
    private void MonitorFreezeStatus()
    {
        if (isFrozen && freezeTimer.Finished)
        {
            isFrozen = false;
        }
    }

    /// <summary>
    /// Moves paddle in appropriate direction based on input received
    /// </summary>
    private void ProcessMotionInput()
    {
        // Check if input to move paddle is received and process only if paddle is not required to be frozen
        float motionInput = Input.GetAxis("Horizontal");
        if (motionInput != 0 && !isFrozen)
        {
            /* Determine position to move to based on input direction and paddle speed
             * set in configuration file - 'newPosition.y' is not explicitly set to
             * 'transform.position.y' since the Rigidbody2D component is configured to
             * lock movement in y-direction already */
            Vector2 newPosition = new Vector2();
            newPosition.x = transform.position.x + (Math.Sign(motionInput) * ConfigurationUtils.PaddleMoveUnitsPerSecond * Time.fixedDeltaTime);

            // Recalculate position to adjust for potential trespass of screen boundaries
            newPosition.x = CalculateClampedX(newPosition.x);

            /* MovePosition() is used instead of directly modifying 'transform.position'
             * so that it moves there instead of directly appearing there - this allows
             * for proper processing of collisions */
            rb2D.MovePosition(newPosition);
        }
    }

    /// <summary>
    /// Checks for ball collisions with paddle and rebounds it off at an angle if so
    /// </summary>
    /// <param name="collision">Object containing information about collision</param>
    private void ProcessCollision(Collision2D collision)
    {
        // Continue processing collision only if game object colliding with paddle is a ball
        if (!collision.gameObject.CompareTag(TagManager.Ball))
        {
            return;
        }

        // Get X coordinate values of geometric centers of paddle and ball
        float paddleCenterX = transform.position.x;
        float ballCenterX = collision.gameObject.transform.position.x;

        // Continue processing collision only if impact is within top surface of paddle (as opposed to edges or sides)
        if (ballCenterX < paddleCenterX - colliderHalfWidth
            || ballCenterX > paddleCenterX + colliderHalfWidth)
        {
            return;
        }

        // Calculate offset between ball and paddle centers as a fraction of the half-width of the paddle
        float offsetBetweenContactAndPaddleCenter = ballCenterX - paddleCenterX;
        float normalizedOffsetOnPaddle = offsetBetweenContactAndPaddleCenter / colliderHalfWidth;

        // Calculate rebound angle in proportion to normalized offset
        float reboundAngleFromVerticalInRadians = normalizedOffsetOnPaddle * MaxReboundHalfAngleInRadians;

        // Calculate rebound direction
        float reboundAngleFromHorizontalInRadians = Mathf.PI / 2 - reboundAngleFromVerticalInRadians;
        Vector2 reboundDirection = new Vector2(Mathf.Cos(reboundAngleFromHorizontalInRadians), Mathf.Sin(reboundAngleFromHorizontalInRadians));

        // Change direction of ball to rebound direction
        collision.gameObject.GetComponent<Ball>().ChangeDirection(reboundDirection);
        
    }

    #endregion // Methods

    #region MonoBehaviour Messages
    
    private void Start()
    {
        InitializeFields();
        AddFreezerEffectListener();
    }

    private void Update()
    {
        MonitorFreezeStatus();
    }

    private void FixedUpdate()
    {
        ProcessMotionInput();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProcessCollision(collision);
    }

    #endregion // MonoBehaviour Messages
}
