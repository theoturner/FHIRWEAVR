using UnityEngine;

public class Player : MonoBehaviour {

    public static int score;

    DataHandler data;
    PushData push = new PushData();

    double heartrate;
    string extraText;

    void Start()
    {

        data = DataHandler.Instance;
        data.StartSession();

    }
	
	void Update()
    {

        heartrate = data.GetMetric("heartrate", "current");
        if (heartrate < 70)
        {
            extraText = "Speed up! You need to work harder.";
        }
        else if (heartrate > 110)
        {
            extraText = "Slow down! You're working too hard.";
        }
        else
        {
            extraText = "Keep going! Your target is 90 bpm.";
        }
        data.DisplayMetric("heartrate", "current", additionalText: extraText + "\nScore: " + score);

    }
}