using TMPro;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public TMP_Text timer;
    int minutes;
    int seconds;


    void Start()
    {
        minutes = 0;
        seconds = 0;
    }

    void Update()
    {
        seconds = (int)Time.time % 60;
        minutes = (int)Time.time / 60;
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
