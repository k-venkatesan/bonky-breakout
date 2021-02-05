using System.Collections.Generic;
using UnityEngine;

public class StandardBlock : Block
{
    #region Fields

    // List of possible sprites for standard block
    [SerializeField]
    List<Sprite> sprites;

    #endregion // Fields

    #region Properties
    #endregion // Properties

    #region Methods

    /// <summary>
    /// Chooses and applies randomly from the list of sprites
    /// </summary>
    private void ApplyRandomSprite()
    {
        int numberOfSprites = sprites.Count;
        int selectedSpriteIndex = RandomNumberGenerator.RandomNumberInRange(0, numberOfSprites - 1);
        GetComponent<SpriteRenderer>().sprite = sprites[selectedSpriteIndex];
    }

    /// <summary>
    /// Retrieves and assigns the points value of a standard block from the configuration file
    /// </summary>
    private void AssignPointsValue()
    {
        pointsWorth = ConfigurationUtils.StandardBlockValue;
    }

    /// <summary>
    /// Verifies that serialized fields are filled and logs warnings where they are not
    /// </summary>
    private void VerifySerializedFields()
    {
        if (sprites == null)
        {
            Debug.LogWarning("Standard block sprites were not found. Please populate fields in Inspector window.");
        }
    }

    #endregion // Methods

    #region MonoBehaviour Messages

    private void Awake()
    {
        VerifySerializedFields();
    }

    protected override void Start()
    {
        base.Start();

        ApplyRandomSprite();
        AssignPointsValue();
    }

    #endregion // MonoBehaviour Messages
}
