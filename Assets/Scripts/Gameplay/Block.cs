using UnityEditorInternal;
using UnityEngine;

/// <summary>
/// Block that player attempts to break
/// </summary>
public class Block : MonoBehaviour
{
    #region Fields
    #endregion // Fields

    #region Components
    #endregion // Components

    #region Methods

    /// <summary>
    /// Checks for ball collisions with block and breaks block if so
    /// </summary>
    /// <param name="collision">Object containing information about collision</param>
    private void ProcessCollision(Collision2D collision)
    {
        // Check if impacting object is ball
        if (collision.gameObject.CompareTag(TagManager.Ball))
        {
            Destroy(gameObject);
        }
    }

    #endregion // Methods

    #region MonoBehaviour Messages

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProcessCollision(collision);
    }

    #endregion // MonoBehaviour Messages
}
