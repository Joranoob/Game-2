using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float startingTime = 60f;
    private float currentTime;

    [SerializeField] private Text countdownText;
    [SerializeField] private Player player;

    private void Start()
    {
        currentTime = startingTime;
        UpdateCountdownText();
    }

    public void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            currentTime = Mathf.Clamp(currentTime, 0, startingTime);
            UpdateCountdownText();

            if (currentTime <= 0)
            {
                player.Die();
            }
        }
    }

    private void UpdateCountdownText()
    {
        countdownText.text = "Time Left: " + currentTime.ToString("0");
    }
}
