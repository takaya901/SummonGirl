using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour 
{
    Animator _animator;
    Transform _cameraTf;

    void Start() 
    {
        _animator = GetComponent<Animator>();
        _cameraTf = Camera.main.transform;
    }

    void OnAnimatorIK(int layerIndex)
    {
        //どの部位がどのくらい見るかを決める
        _animator.SetLookAtWeight(1.0f, 0.0f, 1.0f, 0.0f, 0f);
        //どこを見るか（今回はカメラの位置）
        _animator.SetLookAtPosition(_cameraTf.position);
    }
}