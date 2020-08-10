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

    #endregion // Fields

    #region Properties

    /// <summary>
    /// Maximum angle (in radians) on either side of vertical that ball can rebound off at
    /// </summary>
    private float MaxReboundHalfAngleInRadians => MaxReboundHalfAngleInDegrees * Mathf.Deg2Rad;

    #endregion // Properties

    #region Methods

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
    /// Moves paddle in appropriate direction based on input received
    /// </summary>
    private void ProcessMotionInput()
    {
        // Check if input to move paddle is received
        float motionInput = Input.GetAxis("Horizontal");
        if (motionInput != 0)
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
        // Check if game object colliding with paddle is a ball
        if (collision.gameObject.CompareTag(TagManager.Ball))
        {
            // Calculate offset between point of contact of ball and center of paddle as a fraction of the half-width of the paddle
            float offsetBetweenContactAndPaddleCenter = collision.gameObject.transform.position.x - transform.position.x;
            float normalizedOffsetOnPaddle = offsetBetweenContactAndPaddleCenter / colliderHalfWidth;

            // Calculate rebound angle in proportion to normalized offset
            float reboundAngleFromVerticalInRadians = normalizedOffsetOnPaddle * MaxReboundHalfAngleInRadians;

            // Calculate rebound direction
            float reboundAngleFromHorizontalInRadians = Mathf.PI / 2 - reboundAngleFromVerticalInRadians;
            Vector2 reboundDirection = new Vector2(Mathf.Cos(reboundAngleFromHorizontalInRadians), Mathf.Sin(reboundAngleFromHorizontalInRadians));

            // Change direction of ball to rebound direction
            collision.gameObject.GetComponent<Ball>().ChangeDirection(reboundDirection);
        }
    }

    /// <summary>
    /// Retrieves necessary values from and references to components
    /// </summary>
    private void RetrieveValuesAndReferences()
    {
        rb2D = GetComponent<Rigidbody2D>();
        colliderHalfWidth = GetComponent<BoxCollider2D>().size[0] / 2;
    }

    #endregion // Methods

    #region MonoBehaviour Messages
    
    private void Start()
    {
        RetrieveValuesAndReferences();
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
