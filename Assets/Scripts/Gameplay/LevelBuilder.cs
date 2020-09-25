using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelBuilder : MonoBehaviour
{
    #region Fields

    // References to level object prefabs
    [SerializeField]
    private Paddle prefabPaddle;
    [SerializeField]
    private StandardBlock prefabStandardBlock;
    [SerializeField]
    private BonusBlock prefabBonusBlock;
    [SerializeField]
    private PickupBlock prefabPickupBlock;

    // Block layout dimensions
    private const float horizontalSpacing = 1.1f;
    private const float verticalSpacing = 0.7f;
    private const int blocksPerRow = 15;
    private const int totalRows = 5;

    #endregion // Fields

    #region Components

    /// <summary>
    /// Gets prefab of random block based on percentage distribution
    /// </summary>
    private Block RandomBlockPrefab
    {
        get
        {
            // Block prefab to be returned
            Block prefabBlock;

            /* If percentage distributions are 60, 20, 10 and 10 for example, then block
             * type is assigned based on random number selection as below:
             * * Standard block if less than or equal to 60
             * * Bonus block if between 61 and 80 (both inclusive)
             * * Freezer block if between 81 and 90 (both inclusive)
             * * Speedup block if between 91 and 100 (both inclusive) */
            int randomSelection = RandomNumberGenerator.RandomNumberInRange(1, 100);
            float randomNumberUpperLimitForBlock = 0;
            if (randomSelection <= (randomNumberUpperLimitForBlock += ConfigurationUtils.StandardBlockPercentage))
            {
                prefabBlock = prefabStandardBlock;
            }
            else if (randomSelection <= (randomNumberUpperLimitForBlock += ConfigurationUtils.BonusBlockPercentage))
            {
                prefabBlock = prefabBonusBlock;
            }
            else if (randomSelection <= (randomNumberUpperLimitForBlock += ConfigurationUtils.FreezerBlockPercentage))
            {
                prefabBlock = prefabPickupBlock;
                (prefabBlock as PickupBlock).PickupEffect = PickupEffect.Freezer;
            }
            else if (randomSelection <= (randomNumberUpperLimitForBlock += ConfigurationUtils.SpeedupBlockPercentage))
            {
                prefabBlock = prefabPickupBlock;
                (prefabBlock as PickupBlock).PickupEffect = PickupEffect.Speedup;
            }
            else
            {
                Debug.LogWarning("Error in percentage distribution of blocks.");
                prefabBlock = prefabStandardBlock;
            }
            
            return prefabBlock;
        }
    }

    #endregion // Components

    #region Methods

    /// <summary>
    /// Adds paddle to level
    /// </summary>
    private void AddPaddle()
    {
        Instantiate(prefabPaddle);
    }

    /// <summary>
    /// Builds grid with blocks
    /// </summary>
    private void BuildGrid()
    {
        // Position of first block to be added
        Vector2 blockPosition = new Vector2(-(blocksPerRow / 2) * horizontalSpacing, 0);
                
        for (int i = 0; i < totalRows; ++i)
        {
            for (int j = 0; j < blocksPerRow; ++j)
            {
                // Add block
                Instantiate(RandomBlockPrefab, blockPosition, Quaternion.identity);

                // Update position for next block in same row
                blockPosition.x += horizontalSpacing;
            }

            // Update position for first block of next row
            blockPosition.y += verticalSpacing;
            blockPosition.x = -(blocksPerRow / 2) * horizontalSpacing;
        }
    }

    /// <summary>
    /// Verifies that serialized fields are filled and logs warnings where they are not
    /// </summary>
    private void VerifySerializedFields()
    {
        if (prefabPaddle == null || prefabStandardBlock == null || prefabBonusBlock == null || prefabPickupBlock == null)
        {
            Debug.LogWarning("All prefabs were not found. Please populate fields in Inspector window.");
        }
    }

    #endregion // Methods

    #region MonoBehaviour Messages

    private void Awake()
    {
        VerifySerializedFields();
    }

    private void Start()
    {
        BuildGrid();
        AddPaddle();
    }

    #endregion // MonoBehaviour Messages
}
