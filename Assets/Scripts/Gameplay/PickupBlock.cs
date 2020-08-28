using UnityEngine;

public class PickupBlock : Block
{
    #region Fields

    // Freezer and speedup sprites
    [SerializeField]
    private Sprite freezerSprite;
    [SerializeField]
    private Sprite speedupSprite;

    // Pickup effect of block
    private PickupEffect pickupEffect;

    #endregion // Fields

    #region Properties

    /// <summary>
    /// Sets pickup effect of block 
    /// </summary>
    public PickupEffect PickupEffect
    {
        set
        {
            // Set pickup effect
            pickupEffect = value;

            // Set sprite based on pickup effect
            switch (pickupEffect)
            {
                case PickupEffect.Freezer:
                    GetComponent<SpriteRenderer>().sprite = freezerSprite;
                    break;
                case PickupEffect.Speedup:
                    GetComponent<SpriteRenderer>().sprite = speedupSprite;
                    break;
                default:
                    Debug.LogWarning("Sprite not correctly applied to pickup block");
                    break;
            }
        }
    }

    #endregion // Properties

    #region Methods

    /// <summary>
    /// Retrieves and assigns the points value of a pickup block (freezer/speedup) from the configuration file
    /// </summary>
    private void AssignPointsValue()
    {
        pointsWorth = ConfigurationUtils.PickupBlockValue;
    }

    #endregion // Methods

    #region MonoBehaviour Messages

    private void Start()
    {
        AssignPointsValue();
    }

    #endregion // MonoBehaviour Messages
}
