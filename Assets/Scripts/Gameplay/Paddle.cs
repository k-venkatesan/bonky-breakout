using System;
using UnityEngine;

/// <summary>
/// Paddle used to deflect balls
/// </summary>
public class Paddle : MonoBehaviour
{
    #region Fields

    // Kinematic Rigidbody2D component attached to paddle
    private Rigidbody2D rb2D;

    #endregion // Fields

    #region Properties
    #endregion // Properties

    #region Methods

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

            /* MovePosition() is used instead of directly modifying 'transform.position'
             * so that it moves there instead of directly appearing there - this allows
             * for proper processing of collisions */
            rb2D.MovePosition(newPosition);
        }
    }

    /// <summary>
    /// Stores necessary component references to avoid search at every usage
    /// </summary>
    private void StoreComponentReferences()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    #endregion // Methods

    #region MonoBehaviour Messages
    
    private void Start()
    {
        StoreComponentReferences();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        ProcessMotionInput();
    }

    #endregion // MonoBehaviour Messages
}
