using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Block that player attempts to break
/// </summary>
public class Block : MonoBehaviour
{
    #region Fields

    // Points added to score when block is broken
    protected int pointsWorth;

    // Block breaking event
    private BlockBroken blockBroken;

    // Points addition event
    private PointsAdded pointsAdded;

    #endregion // Fields

    #region Components
    #endregion // Components

    #region Methods

    /// <summary>
    /// Adds listener for block breaking event
    /// </summary>
    /// <param name="listener">Listener for block breaking event</param>
    public void AddBlockBreakingListener(UnityAction listener)
    {
        blockBroken.AddListener(listener);
    }

    /// <summary>
    /// Adds listener for points addition event
    /// </summary>
    /// <param name="listener">Listener for points addition event</param>
    public void AddPointsAdditionListener(UnityAction<int> listener)
    {
        pointsAdded?.AddListener(listener);
    }

    /// <summary>
    /// Initializes events pertaining to block
    /// </summary>
    private void InitializeEvents()
    {
        blockBroken = new BlockBroken();
        EventManager.AddBlockBreakingInvoker(this);

        pointsAdded = new PointsAdded();
        EventManager.AddPointsAddedInvoker(this);
    }

    /// <summary>
    /// Checks for ball collisions with block and breaks block if so
    /// </summary>
    /// <param name="collision">Object containing information about collision</param>
    private void ProcessCollision(Collision2D collision)
    {
        // Check if impacting object is ball
        if (collision.gameObject.CompareTag(TagManager.Ball))
        {
            // Add points before destroying block
            blockBroken.Invoke();
            pointsAdded.Invoke(pointsWorth);
            Destroy(gameObject);
        }
    }

    #endregion // Methods

    #region MonoBehaviour Messages

    protected virtual void Start()
    {
        InitializeEvents();
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        ProcessCollision(collision);
    }

    #endregion // MonoBehaviour Messages
}
