/// <summary>
/// Manages the different menus in the game
/// </summary>
public static class MenuManager
{
    #region Fields
    #endregion // Fields

    #region Properties
    #endregion // Properties

    #region Methods

    /// <summary>
    /// Goes to the given menu
    /// </summary>
    /// <param name="menuName">Menu to go to</param>
    public static void GoToMenu(MenuName menuName)
    {
        switch (menuName)
        {
            case MenuName.MainMenu:
                break;

            default:
                break;
        }
    }

    #endregion // Methods
}
