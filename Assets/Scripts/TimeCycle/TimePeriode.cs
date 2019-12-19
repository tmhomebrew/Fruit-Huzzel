using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePeriode : MonoBehaviour
{
    #region Fields
    [Header("--Pause for \"Time Periode\"--")]
    [SerializeField]
    bool pauseTimeCalc;
    [Header("--Sekunder for en cyclus--")]
    [SerializeField]
    [Range(1f, 60f)]
    float intervalTime;

    float _tid, sinVal, sinValMax, _lerpTime;

    //[SerializeField]
    //float _timeOfTheDay = 0f; //Should/could show a Time system in game..?
    #endregion
    #region Properties
    public float IntervalTime { get => intervalTime; private set => intervalTime = value; }
    public bool PauseTimeCalc { get => pauseTimeCalc; set => pauseTimeCalc = value; }

    #endregion

    /// <summary>
    /// Constructor for TimePeriode.
    /// Can calculate a time interval in seconds.
    /// </summary>
    /// <param name="seconds">Number of seconds for a run through.</param>
    public TimePeriode(float seconds = 10)
    {
        IntervalTime = seconds;
    }

    void Start()
    {
        _tid = (1 / IntervalTime); //TempTime Set by construtor, default 10s..
        sinValMax = 1f;
        _lerpTime = 0f;
        PauseTimeCalc = false;
    }

    void Update()
    {
        if (!PauseTimeCalc)
        {
            //TimePeriodCalc(IntervalTime);
        }
    }

    /// <summary>
    /// Returns a float 
    /// </summary>
    /// <returns>Returns a float between [-1 , 1 ].</returns>
    public float TimePeriodCalc(float seconds)
    {
        if (_tid != (1 / seconds))
        {
            print($"{_tid / (seconds)}");
            return _tid = (1 / (seconds)); //Pure Ref in Inspector
        }
        else
            return _tid;
    }
    //TEST
    //public float TimePeriodCalc()
    //{
    //    if (_tid != (1 / IntervalTime))
    //    {
    //        _tid = (1 / (IntervalTime)); //Pure Ref in Inspector
    //    }
    //    _lerpTime += (_tid) * Time.deltaTime; //<-- 0.1f = v ~ ( 1sec / v(2) = 5s)
    //    sinVal = Mathf.Lerp(0, sinValMax, _lerpTime); //<-- Interpolates between (0)/-sinValMax and sinValMax..

    //    //// now check if the interpolator has reached 1.0
    //    //// and swap maximum and minimum so game object moves
    //    //// in the opposite direction.
    //    if (_lerpTime >= 1.0f)
    //    {
    //        float temp = sinValMax;
    //        sinValMax = -sinValMax; //<-- -sinValMax is to set the minimum Interpolation for sinVal..
    //        sinValMax = -temp;
    //        _lerpTime = 0.0f;

    //        //Sets other Values..
    //    }

    //    //if (sinVal < 0)

    //    print($"SinusValue.: {sinVal} + TimeValue.: {_lerpTime}.."); //Only positive numbers..

    //    return sinVal;
    //}
}