using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GoalZone leftGoalZone;
    public GoalZone rightGoalZone;
    public WinPanel winPanel;

    public int useLevel = 0;
    private void Start()
    {
        Application.targetFrameRate = 60;

        leftGoalZone.OnGoalReached += () => OnGoalReached("RIGHT");
        rightGoalZone.OnGoalReached += () => OnGoalReached("LEFT");

        winPanel.OnRestartClicked += OnRestartClicked;
        winPanel.Hide();
        useLevel = PlayerPrefs.GetInt("user_level", 0);
        FirebaseManager.Instance.SendLevelStart(useLevel);
    }

 
    void OnGUI()
    {
        int w = Screen.width / 2;
        int h = Screen.width / 20;
        int fontSizeOld =   GUI.skin.button.fontSize;
        GUI.skin.label.fontSize = 3*Screen.width / (20*4);
        GUI.Label(new Rect(w, 0, w, h), "level : " + useLevel);
        GUI.skin.label.fontSize = fontSizeOld;
    }

    void SetUserLevel(int newUseLevel)
    {
        useLevel = newUseLevel;
        PlayerPrefs.SetInt("user_level", useLevel);
        FirebaseManager.Instance.SetLevel(useLevel);
     
    }

    private void OnRestartClicked()
    {
        FirebaseManager.Instance.SendLevelStart(useLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnGoalReached(string winner)
    {
        FirebaseManager.Instance.SendLevelEnd(useLevel, winner);
        winPanel.SetText($"{winner} WINS! \n Level " + useLevel + " is completed");
        SetUserLevel(useLevel+1);
        winPanel.Show();
        
    }
}