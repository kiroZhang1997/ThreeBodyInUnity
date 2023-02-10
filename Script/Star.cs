using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private Vector3 beginSpeed = Vector3.zero;//初速度默认为0
    [SerializeField] private float mass = 15f;//质量
    [SerializeField] private List<Star> otherStars;//其他的天体
    
    [HideInInspector] public Rigidbody rigidbodySelf;
    
    private ConstantForce _constantForce;
    private const float G = 1f;//在本模拟环境中引力常数=1

    
    private void Start()
    {
        if (transform.GetComponent<Rigidbody>())
        {
            rigidbodySelf = transform.GetComponent<Rigidbody>();
            rigidbodySelf.velocity = beginSpeed;
            rigidbodySelf.mass = mass;
        }
        else
        {
            Debug.LogError("天体没有挂刚体组件");
        }

        if (transform.GetComponent<ConstantForce>())
        {
            _constantForce = transform.GetComponent<ConstantForce>();
        }
        else
        {
            Debug.LogError("天体没有挂ConstantForce组件");
        }
        
    }

    private void FixedUpdate()
    {
        Vector3 gravitation = Vector3.zero;
        foreach (Star otherStar in otherStars)
        {
            //天体之间的方向
            Vector3 direction = otherStar.transform.position - transform.position;
            //F=(G * Mm)/r2 => 万有引力=引力常数*两天体质量乘积/两天体距离的平方
            gravitation += (otherStar.rigidbodySelf.mass * rigidbodySelf.mass * G * direction.normalized) /
                           direction.sqrMagnitude;
            _constantForce.force = gravitation;
        }
    }
    
}
