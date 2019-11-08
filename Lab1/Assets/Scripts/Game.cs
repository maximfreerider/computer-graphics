using UnityEngine;

public class Game : MonoBehaviour
{
    public static int Points;
    private string _livesString;
    private string _pointsString;

    public void Awake()
    {
        Player.Lives = 1;
        Points = 0;
    }

    public void Update()
    {
        _livesString = "Life: " + Player.Lives.ToString("");
        _pointsString = "Points: " + Points.ToString("");
    }

    public void OnGUI()
    {
        GUI.color = Color.black;
        GUI.skin.label.alignment = TextAnchor.UpperRight;
        GUI.Label(new Rect(0, 20, 850, 512), _livesString);
        GUI.Label(new Rect(0, 40, 850, 512), _pointsString);
    }
}