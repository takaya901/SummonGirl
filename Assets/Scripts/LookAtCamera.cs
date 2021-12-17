using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookAtCamera : MonoBehaviour 
{
    Animator _animator;
    Transform _cameraTf;
    Text text;

    void Start() 
    {
        _animator = GetComponent<Animator>();
        _cameraTf = Camera.main.transform;
        text = GameObject.FindGameObjectWithTag("DebugText").GetComponent<Text>();
    }

    void OnAnimatorIK(int layerIndex)
    {
        //どの部位がどのくらい見るかを決める
        _animator.SetLookAtWeight(1.0f, 0.0f, 1.0f, 0.0f, 0f);
        //どこを見るか（今回はカメラの位置）
        _animator.SetLookAtPosition(_cameraTf.position);
    }

    /// <summary>
    /// カメラとの角度が閾値を超えたら体ごと向きを変える
    /// </summary>
    void Update()
    {
        var camPos = _cameraTf.position - transform.position;
        camPos.y = 0f;
        var angle = Vector3.Angle(gameObject.transform.forward, camPos);
        text.text = angle.ToString();
        if (angle > 30f)
        {
            var lookRotation = Quaternion.LookRotation(camPos, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, 0.1f);
        }
    }
}