using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SunBehavior : MonoBehaviour
{
    #region Fields
    Light[] sunLightGO;
    [Header("Lys Referencer")]
    [SerializeField]
    Light sunLightRef, moonLightRef;

    //Color Controller
    [Header("Color Controller")]
    [SerializeField]
    Color startSunColor, endSunColor;
    [SerializeField]
    List<Material> listOfDayLightMaterials = new List<Material>();
    [SerializeField]
    List<Material> listOfNightMaterials = new List<Material>();
    List<Material> currentLigthSourceMats = new List<Material>();

    List<float> listOfTimers = new List<float>() { 
                        2f, 3f, 5f, 8f, 
                        12f, 15f }; //Sample.: 1 sec / [i] ( ChangeColor() )..
    List<float> currentListOfTimers = new List<float>();

    bool changeColor;
    [SerializeField]
    bool sunColorChangerIsOn = true;
    [SerializeField]
    int ChangeVar = 0;
    [SerializeField]
    bool dayTime; //Static bool

    //SunRotation
    [SerializeField]
    bool sunRotationIsOn = false;

    float xCur;
    float xMax, xMin;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Setup Refs
        sunLightGO = GetComponentsInChildren<Light>();
        foreach (Light lys in sunLightGO)
        {
            if (lys.gameObject.name.ToLower().Contains("sunlight"))
            {
                sunLightRef = lys;
            }
            if (lys.gameObject.name.ToLower().Contains("moonlight"))
            {
                moonLightRef = lys;
            }
        }

        //Set values
        ChangeVar = 0;
        sunLightRef.color = listOfDayLightMaterials[0].color;

        startSunColor = sunLightRef.color;
        changeColor = false;

        //Rotation
        xMax = 185;
        xMin = -5;
        xCur = xMin;

        xCur = TimePeriodCalc(listOfTimers.Sum());
    }

    // Update is called once per frame
    void Update()
    {
        SunRotator();
        //SunColorChanger();
        SunColor();
    }

    #region Sun Rotation Handler
    void SunRotator()
    {
        if (sunRotationIsOn)
        {
            if (dayTime == false && changeCycle == true && transform.eulerAngles.z >= 190f)
            {
                SunRotationSets();
            }
            else if (dayTime == true && changeCycle == true && transform.eulerAngles.z >= 190f)
            {
                SunRotationSets();
            }

            transform.Rotate(new Vector3(0f, 0f, xCur), Space.Self);
        }
    }

    void SunRotationSets()
    {
        changeCycle = false;
        dayTime = !dayTime;
        isColorListRunning = false;
        transform.eulerAngles = new Vector3(0f, 0f, xMin);
    }
    #endregion


    #region SunColerChanger v1.2 - 18/12-19
    void SunColor()
    {
        if (sunColorChangerIsOn)
        {
            if (isColorListRunning == false)
            {
                if (dayTime == true)
                {
                    StartCoroutine(ListController(listOfDayLightMaterials)); //DayLight mats
                    print($"Im here.. SunColor(), Daytime True.!");
                }
                else
                {
                    StartCoroutine(ListController(listOfNightMaterials)); //NightLight mats
                    print($"Im here.. SunColor(), Nighttime True.!");
                }
            }
        }
    }

    bool isColorListRunning = false;
    float _timeLerpColorToAnother;
    float _tid;

    IEnumerator ListController(List<Material> mats)
    {
        currentLigthSourceMats = mats;
        currentListOfTimers = listOfTimers;
        //Start
        isColorListRunning = true; 

        yield return StartCoroutine(RunningColorList(currentLigthSourceMats, currentListOfTimers));
        currentLigthSourceMats.Reverse();
        currentListOfTimers.Reverse();

        yield return StartCoroutine(RunningColorList(currentLigthSourceMats, currentListOfTimers));
        //currentLigthSourceMats.Clear();
        //currentListOfTimers.Clear();

        yield return new WaitForEndOfFrame();
        //isColorListRunning = false;
        //End
        //dayTime = !dayTime;
    }

    IEnumerator RunningColorList(List<Material> mats, List<float> timers)
    {
        print($"Start RunningColorList with {mats.First()}");
        
        for (ChangeVar = 0; ChangeVar < mats.Count -1; ChangeVar++)
        {
            _tid = TimePeriodCalc(timers[ChangeVar]);
            startSunColor = mats[ChangeVar].color;
            if (ChangeVar == mats.Count)
            {
                endSunColor = mats[ChangeVar].color;
            }
            else
            {
                endSunColor = mats[ChangeVar + 1].color;
            }

            yield return StartCoroutine(ColorLerper());
        }

        print($"Ending RunningColorList() with {mats.Last()}");
        yield return null;
    }
    IEnumerator ColorLerper()
    {
        _timeLerpColorToAnother = 0;
        while (sunLightRef.color != endSunColor)
        {
            _timeLerpColorToAnother += _tid * Time.deltaTime; //<------- this piece of work..
            sunLightRef.color = Color.Lerp(startSunColor, endSunColor, _timeLerpColorToAnother);
            yield return null;
        }
        yield return null;
    }

    #endregion

    public float TimePeriodCalc(float seconds)
    {
        return (1 / seconds);
    }

    #region Sun Color Handler - OLD d. 18/12-19
    void SunColorChanger()
    {
        if (sunColorChangerIsOn)
        {
            if (changeColor == false)
            {
                ChangeColorOfSun();
                _tid = TimePeriodCalc(listOfTimers[ChangeVar]); //Used in ChangeColor();
            }
            else
            {
                ChangeColor();
            }
        }
    }

    int moodChange = 0;
    bool changeCycle = true;
    void ChangeColorOfSun()
    {
        moodChange++;
        if (moodChange % 12 == 0)
        {
            moodChange = 0;
            changeCycle = true;
            if (currentLigthSourceMats == listOfNightMaterials)
            {
                currentLigthSourceMats = listOfDayLightMaterials;
            }
            else
            {
                currentLigthSourceMats = listOfNightMaterials;
            }
        }
        changeColor = true;
        endSunColor = ChangeMat().color;
    }

    Material ChangeMat()
    {
        if (ChangeVar >= currentLigthSourceMats.Count)
        {
            ChangeVar = 0;
            currentLigthSourceMats.Reverse();
            listOfTimers.Reverse();
        }

        return currentLigthSourceMats[ChangeVar];
    }

    void ChangeColor()
    {
        _timeLerpColorToAnother += _tid * Time.deltaTime; //<------- this piece of work..
        sunLightRef.color = Color.Lerp(startSunColor, endSunColor, _timeLerpColorToAnother);
        if (sunLightRef.color == endSunColor && _timeLerpColorToAnother != 0f)
        {
            startSunColor = endSunColor;
            _timeLerpColorToAnother = 0f;
            changeColor = false;
            ChangeVar++;
        }
    }
    #endregion
}