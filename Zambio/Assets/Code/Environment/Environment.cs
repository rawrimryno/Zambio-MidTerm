using UnityEngine;
using System.Collections;
using System;

public class Environment : MonoBehaviour
{

    private Light Sun;
    public int DayLength = 120; // Seconds per Day
    public int DawnStart = 6;
    public int DayStart = 9;
    public int DuskStart = 15;
    public int NightStart = 18;

    private static float staticIntensity = 0.6f;
    public float nightFogDens = 0.04f;
    public float dayFogDens = 0.01f;
    public float midFogDens = 0.03f;

    public float initDawnIntensity = staticIntensity;  // FinalNightIntensity
    public float initDayIntensity = 0.8f;  // FinalDawnIntensity
    public float initDuskIntensity = staticIntensity;  // Final Day Intensity
    public float initNightIntensity = 0.4f; // FinalDuskIntensity

    public Color initDawnColor;// = new Color(160, 73, 126);
    public Color initDayColor;// = new Color(135, 206, 235);
    public Color initDuskColor;// = new Color(144, 96, 144);
    public Color initNightColor;// = new Color (90,60,90);

    public Color initDawnFog;
    public Color initDayFog;
    public Color initDuskFog;
    public Color initNightFog;

    public float SunAngle = 0;

    private float initTime = 6;  // This is what time it is when our game starts
                                 //  This shouldn't change, but make it public to tweak

    //private int numDays = 100;
    private float dt;
    private Quaternion initSunRotation/*, finalSunRotation*/;
    private Vector3 SunRotationAxis;
    float SunSpeed;
    float normInitTime;
    public float dtInHours; // Public for Observational Purposes from inside Unity
    float percentDay;
    public FogMode OurFogMode = FogMode.Exponential;



    // Use this for initialization
    void Start()
    {
        initSunRotation = Quaternion.Euler(0f, 60f, 0f);
        //finalSunRotation = Quaternion.Euler(new Vector3(359.9f, 60f, 0f));

        SunRotationAxis = initSunRotation * Vector3.right;
        Sun = GetComponentInChildren<Light>();
        Sun.intensity = initDawnIntensity;

        RenderSettings.fog = true;
        RenderSettings.fogMode = OurFogMode;


        if (DayLength == 0)
        {
            DayLength = 120;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ( RenderSettings.fogMode != OurFogMode)
        {
            RenderSettings.fogMode = OurFogMode;
        }
        if (Sun != null)
        {
            // This controls the Rotation of the Light Source,
            // Gives the appearance of rising and sitting
            dt = Time.time % DayLength ;        // 0 -> dayLength
            SunSpeed = 360f / DayLength;       // 1 Cycle/dayLength
            percentDay = dt / DayLength;       // (0 -> DayLength)/DayLength
            normInitTime = initTime / 24f * DayLength;  // initTime is in hours, make it percent apply to dayDuration
                                                        //Debug.Log("<b>dtInHours : " + dtInHours + "</b>");
                                                        // This rotates our sun
            dtInHours = percentDay * 24f;      // (0 -> 1) * 24 should resemble hours now
            Sun.transform.rotation = Quaternion.AngleAxis(SunSpeed * (dt - normInitTime), SunRotationAxis);

            if (dtInHours < DawnStart)
            {
                // This if makes sure we don't do anything until 6am, sound like my life
                // Considered FSM, but this more of a cycle than a varying statemachine
                // Open for suggestions.
                Night(dtInHours);
            }
            else if (dtInHours < DayStart)
            {
                Dawn(dtInHours);
            }
            else if (dtInHours < DuskStart)
            {
                Day(dtInHours);
            }
            else if (dtInHours < NightStart)
            {
                Dusk(dtInHours);
            }
            else
            {
                Night(dtInHours);
            }
        }
    }

    // These handle aesthetics of Time Periods
    void Dawn(float time)
    {
        changeIntensity(time, DawnStart, DayStart, initDawnIntensity, initDayIntensity);
        changeColor(time, DawnStart, DayStart, initDawnColor, initDayColor);
        changeFog(time, DawnStart, DayStart, initDawnFog, initDayFog, midFogDens, dayFogDens);

        //changeColorofSomethingToResemble(DawnColorsInWorldView);
        // Debug.Log("Dawn");
    }
    void Day(float time)
    {
        changeIntensity(time, DayStart, DuskStart, initDayIntensity, initDuskIntensity);
        changeColor(time, DayStart, DuskStart, initDayColor, initDuskColor);
        changeFog(time, DayStart, DuskStart, initDayFog, initDuskFog, dayFogDens, midFogDens);


        //Debug.Log("Day");
    }
    void Dusk(float time)
    {
        changeIntensity(time, DuskStart, NightStart, initDuskIntensity, initNightIntensity);
        changeColor(time, DuskStart, NightStart, initDuskColor, initNightColor);
        changeFog(time, DuskStart, NightStart, initDuskFog, initNightFog, midFogDens, nightFogDens);

        // Trying out the mark-down stuff
        //Debug.Log("<b>Dusk</b>");
    }
    void Night(float time)
    {
        changeIntensity(time, NightStart, DawnStart, initNightIntensity, initDawnIntensity);
        changeColor(time, NightStart, DawnStart, initNightColor, initDawnColor);
        changeFog(time, NightStart, DawnStart, initNightFog, initDawnFog, nightFogDens, midFogDens);

        //Debug.Log("<i>Night</i>");
    }

    void changeFog(float time, float initTime, float finalTime, Color initFog, Color finalFog, float dayFogDensity, float nightFogDensity)
    {
        if (time < initTime)
        {
            time += 24;
        }
        if (finalTime < initTime)
        {
            finalTime += 24;
        }
        float duration = Math.Abs(finalTime - initTime);
        float perCentTime = (time - initTime) / duration;

        RenderSettings.fogColor = Color.Lerp(initFog, finalFog, perCentTime);
        RenderSettings.ambientSkyColor = RenderSettings.fogColor;
        RenderSettings.fogDensity = Mathf.Lerp(dayFogDensity, nightFogDensity, perCentTime);

    }

    void changeIntensity(float time, float initTime, float finalTime, float initIntensity, float finalIntensity)
    {
        if (time < initTime)
        {
            time += 24;
        }
        if (finalTime < initTime)
        {
            finalTime += 24;
        }
        float duration = Math.Abs(finalTime - initTime);
        float perCentTime = (time - initTime) / duration;

        Sun.intensity = Mathf.Lerp(initIntensity, finalIntensity, perCentTime);
    }
    void changeColor(float time, float initTime, float finalTime, Color initColor, Color finalColor)
    {
        // After 2359 it becomes 0000, if that happens, we don't want the math to get weird
        if (finalTime < initTime)
        {
            finalTime += 24f;
        }
        if (time < initTime)
        {
            time += 24;
        }
        float duration = Math.Abs(finalTime - initTime);
        float percentTime = (time - initTime) / duration;

        Sun.color = Color.Lerp(initColor, finalColor, percentTime);
        RenderSettings.ambientGroundColor = Sun.color;
    }


}