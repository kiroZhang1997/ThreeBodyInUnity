using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//纪元计算
public class EarthCalendar : MonoBehaviour
{
    //纪元定义
    public enum Epoch
    {
        Default,
        EternalEpoch,//恒纪元
        ChaoticEpoch //乱纪元
    }
    
    [SerializeField] private List<Transform> stars;
    [SerializeField] private float warmDistanceMin;//恒纪元定义:距离恒星最小距离
    [SerializeField] private float warmDistanceMax;//恒纪元定义:距离恒星最大距离
    [SerializeField] private Text epochText;

    [HideInInspector] public Epoch currentEpoch = Epoch.Default;
    
    private Epoch _lastEpoch = Epoch.Default;
    private float _timer = 0f;//恒定计时
    private float _tmpTimer = 0f;//计时打点

    private string _special = "";//特殊天文现象
    private Transform _centerStar = null;//恒纪元围绕的恒星
    
    private void Update()
    {
        _timer += Time.deltaTime;
        EpochDecide();
    }

    private void EpochDecide()
    {
        int count = 0;
        foreach (Transform star in stars)
        {
            float distance = (star.position - transform.position).magnitude;
            if (distance >= warmDistanceMin && distance <= warmDistanceMax)
            {
                count++;
            }

            _centerStar = star;
        }


        switch (count)
        {
            case 0:
                currentEpoch = Epoch.ChaoticEpoch;
                _special = "三颗飞星";
                break;
            case 1:
                _special = " ";
                currentEpoch = Epoch.EternalEpoch;//和仅一个恒星处于合适距离时为恒纪元
                break;
            case 2:
                _special = " ";
                currentEpoch = Epoch.ChaoticEpoch;
                break;
            case 3:
                _special = "三日凌空";
                currentEpoch = Epoch.ChaoticEpoch;
                break;
        }
        

        if (currentEpoch != _lastEpoch)
        {
            if (currentEpoch == Epoch.ChaoticEpoch)
            {
                String description = "";
                String duration = "";
                if (_timer - _tmpTimer < 3.65)
                {
                    float dayCount = Mathf.Ceil((_timer - _tmpTimer) * 100 ) ;
                    duration =  dayCount + "地球天";
                    description = description = "上一个恒纪元持续了" + dayCount + "地球天,现在是乱纪元" + "\n" + _special;
                }
                else
                {
                    duration =  (_timer - _tmpTimer).ToString("f2") + "地球年";
                    description = "上一个恒纪元持续了" + (_timer - _tmpTimer).ToString("f2") + "地球年,现在是乱纪元" + "\n" + _special;
                }
                Debug.Log(description);
                epochText.text = description;
            }
            if (currentEpoch == Epoch.EternalEpoch)
            {
                String description = "";
                String duration = "";
                if (_timer - _tmpTimer < 3.65)
                {
                    float dayCount = Mathf.Ceil((_timer - _tmpTimer) * 100) ;
                    duration = dayCount + "地球天";
                    description = description = "上一个乱纪元持续了" + dayCount + "地球天,现在是恒纪元"+"\n围绕"+_centerStar.name + "旋转";
                }
                else
                {
                    duration = (_timer - _tmpTimer).ToString("f2") + "地球年";
                    description = "上一个乱纪元持续了" + (_timer - _tmpTimer).ToString("f2") + "地球年,现在是恒纪元";
                }
                Debug.Log(description);
                epochText.text = description;
            }
            
            _lastEpoch = currentEpoch;
            _tmpTimer = _timer;
        }
    }
}
