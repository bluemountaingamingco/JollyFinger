using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : CleanTimer
{
    private TextMeshProUGUI countDownText;

    protected override void Update()
    {
        if (Timer > 0)
        {
            DisplayCountdown();

            Timer -= Time.deltaTime;
        }
        else
        {
            transform.parent.gameObject.SetActive(false);
        }
    }

    public void DisplayCountdown()
    {
        float seconds = Mathf.FloorToInt(Timer % 60);

        countDownText.text = seconds.ToString();
    }

    private void OnEnable()
    {
        countDownText = GetComponent<TextMeshProUGUI>();

        Timer = 3;
    }

    private void OnDisable()
    {
        GameManager.Instance.EnableTimer(true);

        GameManager.Instance.SetCleanGOCollider(true);
    }
}
