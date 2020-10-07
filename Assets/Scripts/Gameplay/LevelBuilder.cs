using UnityEngine;

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
    /// Adds random block to level based on configured probability distribution
    /// </summary>
    /// <param name="position">Position in grid where block is to be added</param>
    private void AddRandomBlock(Vector2 position)
    {
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
            Instantiate(prefabStandardBlock, position, Quaternion.identity);
        }
        else if (randomSelection <= (randomNumberUpperLimitForBlock += ConfigurationUtils.BonusBlockPercentage))
        {
            Instantiate(prefabBonusBlock, position, Quaternion.identity);
        }
        else if (randomSelection <= (randomNumberUpperLimitForBlock += ConfigurationUtils.FreezerBlockPercentage))
        {
            prefabPickupBlock.gameObject.SetActive(false);
            PickupBlock block = Instantiate(prefabPickupBlock, position, Quaternion.identity);
            block.PickupEffect = PickupEffect.Freezer;
            block.gameObject.SetActive(true);
        }
        else if (randomSelection <= (randomNumberUpperLimitForBlock += ConfigurationUtils.SpeedupBlockPercentage))
        {
            prefabPickupBlock.gameObject.SetActive(false);
            PickupBlock block = Instantiate(prefabPickupBlock, position, Quaternion.identity);
            block.PickupEffect = PickupEffect.Speedup;
            block.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Error in percentage distribution of blocks.");
            Instantiate(prefabStandardBlock, position, Quaternion.identity);
        }
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
                AddRandomBlock(blockPosition);

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
