using UnityEngine;

public class Player : MonoBehaviour {

    public static int score;

    DataHandler data;
    PushData push = new PushData();

    double heartrate;
    float xLean, xPosition, xDelta;
    string extraText;

    void Start()
    {

        data = DataHandler.Instance;
        data.StartSession();

    }
	
	void Update()
    {

        xLean = (float)-data.GetMetric("lean", "current") / 5;
        xPosition = transform.position.x;
        xDelta = (float)1.55 - Mathf.Abs(xPosition);
        if (Mathf.Abs(xLean) > 0.015 && Mathf.Abs(xPosition) <= 1.55)
        {
            transform.Translate(xLean, 0, 0);
        }

        // Bounce user off edge of track to clearly demonstrate the barrier
        // Continued leaning against the edge 'shakes' the camera, like rumble strips
        else if (xPosition > 1.55)
        {
            transform.Translate(xDelta, 0, 0);
        }
        else if (xPosition < -1.55)
        {
            transform.Translate(-xDelta, 0, 0);
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

    }
}