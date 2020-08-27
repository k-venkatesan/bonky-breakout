using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    #region Fields

    // Parameters to be displayed in HUD
    private static int score = 0;
    private static int ballsLeft = 0;

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
    /// Increases score by given points and updates display
    /// </summary>
    /// <param name="pointsToIncreaseScoreBy">Points to increase total score by</param>
    public static void IncreaseScore(int pointsToIncreaseScoreBy)
    {
        score += pointsToIncreaseScoreBy;
        scoreText.text = ScoreTextContents;
    }

    /// <summary>
    /// Initializes text on HUD
    /// </summary>
    private void InitializeText()
    {
        // Initialize text displaying score
        scoreText = GameObject.FindWithTag(TagManager.ScoreDisplay).GetComponent<Text>();
        scoreText.text = ScoreTextContents;

        // Initialize text displaying balls left
        ballsLeftText = GameObject.FindWithTag(TagManager.BallsLeftDisplay).GetComponent<Text>();
        ballsLeftText.text = BallsLeftTextContents;
    }

    #endregion // Methods

    #region MonoBehaviour Messages

    private void Start()
    {
        InitializeText();
    }

    #endregion MonoBehaviour Messages
}
