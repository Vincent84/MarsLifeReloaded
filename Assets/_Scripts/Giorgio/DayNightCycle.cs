using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour {

    public float time;
    public TimeSpan currentTime;
    public Transform SunTransform;
    public Light Sun;
    public Text timeText;
    public int Days;

    public float intensity;
    public Color fogDay = Color.grey;
    public Color fogNight = Color.black;

 
    
    public float Speed;

	void Update () {
        ChangeTime();
	}

    public void ChangeTime()
    {
        time += Time.deltaTime + Speed;
        if (time > 86400)
        {
            Days += 1;
            time = 0;
        }
        currentTime = TimeSpan.FromSeconds(time);

        SunTransform.rotation = Quaternion.Euler(new Vector3((time-21600)/86400*360,0,0));

        if (time < 43200)
            intensity = 1 - (43200 - time) / 43200;
        else
            intensity = 1 - ((43200 - time) / 43200 * -1);
        if (intensity < 0.3)
            intensity = 0;

        RenderSettings.fogColor = Color.Lerp(fogNight, fogDay, intensity * intensity);

        Sun.intensity = intensity;
    }


}
