using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    #region Fields

    // Parameters to be displayed in HUD
    private static int score;
    private static int ballsLeft;

    // Text objects that display required parameters
    private static Text scoreText;
    private static Text ballsLeftText;

    #endregion // Fields

    #region Properties

    /// <summary>
    /// Contents of text object displaying score
    /// </summary>
    private static string ScoreTextContents
    {
        get { return "Score: " + score.ToString(); }
    }

    /// <summary>
    /// Contents of text object displaying balls left
    /// </summary>
    private static string BallsLeftTextContents
    {
        get { return "Balls Left: " + ballsLeft.ToString(); }
    }

    #endregion // Properties

    #region Methods

    /// <summary>
    /// Adds listeners for events pertaining to HUD
    /// </summary>
    private void AddEventListeners()
    {
        EventManager.AddPointsAddedListener(IncreaseScore);
    }

    /// <summary>
    /// Decreases number of balls left and updates display
    /// </summary>
    public static void DecreaseBallsLeftByOne()
    {
        ballsLeft -= 1;
        ballsLeftText.text = BallsLeftTextContents;
    }

    /// <summary>
    /// Increases score by given points and updates display
    /// </summary>
    /// <param name="pointsToIncreaseScoreBy">Points to increase total score by</param>
    private void IncreaseScore(int pointsToIncreaseScoreBy)
    {
        score += pointsToIncreaseScoreBy;
        scoreText.text = ScoreTextContents;
    }

    /// <summary>
    /// Initializes parameters and correspondings texts on HUD
    /// </summary>
    private void InitializeTexts()
    {
        // Initialize score and corresponding display text
        score = 0;
        scoreText = GameObject.FindWithTag(TagManager.ScoreDisplay).GetComponent<Text>();
        scoreText.text = ScoreTextContents;

        // Initialize balls left and corresponding display text
        ballsLeft = ConfigurationUtils.TotalBallsPerGame;
        ballsLeftText = GameObject.FindWithTag(TagManager.BallsLeftDisplay).GetComponent<Text>();
        ballsLeftText.text = BallsLeftTextContents;
    }

    #endregion // Methods

    #region MonoBehaviour Messages

    private void Start()
    {
        InitializeTexts();
        AddEventListeners();
    }

    #endregion MonoBehaviour Messages
}
