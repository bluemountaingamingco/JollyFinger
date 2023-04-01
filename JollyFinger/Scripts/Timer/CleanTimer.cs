using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanTimer : MonoBehaviour
{
    private float timer;

    public float Timer
    {
        get { return timer; }
        set { timer = value; }
    }

    protected virtual void Update()
    {
        timer += Time.deltaTime;
    }

    private void OnEnable()
    {
        timer = 0;
    }
}
