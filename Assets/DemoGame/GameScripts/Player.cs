using UnityEngine;

public class Player : MonoBehaviour {

    public static int score;

    DataHandler data;
    PushData push = new PushData();

    double xLean, xPosition, heartrate;
    string extraText;

    void Start()
    {

        data = DataHandler.Instance;
        data.StartSession();

    }
	
	void Update()
    {

        xLean = -data.GetMetric("lean", "current") / 5;
        xPosition = transform.position.x;
        if (Mathf.Abs((float)xLean) > 0.015 && Mathf.Abs((float)xPosition) <= 1.55)
        {
            transform.Translate((float)xLean, 0, 0);
        }

        // Bounce user off edge of track to clearly demonstrate the barrier
        // Continued leaning against the edge 'shakes' the camera, like rumble strips
        else if (xPosition > 1.55)
        {
            transform.Translate((float)(1.55 - xPosition), 0, 0);
        }
        else if (xPosition < -1.55)
        {
            transform.Translate(-(float)(1.55 + xPosition), 0, 0);
        }

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
        data.DisplayMetric("lean", "current");

    }
}