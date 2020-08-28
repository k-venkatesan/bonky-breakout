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

    // Block layout dimensions
    private const float horizontalSpacing = 1.1f;
    private const float verticalSpacing = 0.7f;
    private const int blocksPerRow = 15;
    private const int totalStandardBlockRows = 3;

    #endregion // Fields

    #region Components
    #endregion // Componenets

    #region Methods

    /// <summary>
    /// Adds paddle to level
    /// </summary>
    private void AddPaddle()
    {
        Instantiate(prefabPaddle);
    }

    /// <summary>
    /// Builds rows of standard blocks
    /// </summary>
    private void BuildStandardBlockRows()
    {
        // Position of first block to be added
        Vector2 blockPosition = new Vector2(-(blocksPerRow / 2) * horizontalSpacing, 0);

        for (int i = 0; i < totalStandardBlockRows; ++i)
        {
            for (int j = 0; j < blocksPerRow; ++j)
            {
                // Add block
                Instantiate(prefabStandardBlock, blockPosition, Quaternion.identity);

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
        if (prefabPaddle == null || prefabStandardBlock == null)
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
        AddPaddle();
        BuildStandardBlockRows();
    }

    #endregion // MonoBehaviour Messages
}
