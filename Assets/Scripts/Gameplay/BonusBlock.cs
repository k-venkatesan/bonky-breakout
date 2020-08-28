public class BonusBlock : Block
{
    #region Fields
    #endregion // Fields

    #region Properties
    #endregion // Properties

    #region Methods

    /// <summary>
    /// Retrieves and assigns the points value of a bonus block from the configuration file
    /// </summary>
    private void AssignPointsValue()
    {
        pointsWorth = ConfigurationUtils.BonusBlockValue;
    }

    #endregion // Methods

    #region MonoBehaviour Messages

    private void Start()
    {
        AssignPointsValue();
    }

    #endregion // MonoBehaviour Messages
}
