using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Transfer : MonoBehaviour
{
//    Material _transferMat;
//    string _threshold = "_Threshold";
//    float _currentVelocity;
//    
//    void Start()
//    {
//        _transferMat = GetComponent<Renderer>().material;
//    }
//
//    void Update()
//    {
//        var height = Mathf.SmoothDamp(_transferMat.GetFloat(_threshold), 1f, ref _currentVelocity, 2f);
//        if (_transferMat.HasProperty(_threshold)) {
//            _transferMat.SetFloat(_threshold, height);
//        }
//    }

    Dictionary<string, string> _mat2Shader = new Dictionary<string, string>()
    {
        {"body (Instance)", "UnityChan/Skin"},
        {"face (Instance)", "UnityChan/Skin"},
        {"skin1 (Instance)", "UnityChan/Skin"},
        {"hair (Instance)", "UnityChan/Skin"},
        {"eye_L1 (Instance)", "UnityChan/Eye - Transparent"},
        {"eye_R1 (Instance)", "UnityChan/Eye - Transparent"},
        {"eyebase (Instance)", "UnityChan/Eye"},
        {"eyeline (Instance)", "UnityChan/Eyelash - Transparent"},
        {"mat_cheek (Instance)", "UnityChan/Blush - Transparent"}
    };

    // mesh_root下のパーツのシェーダーを操作する
    List<Material> _materials = new List<Material>();
    string _threshold = "_Threshold";
    float _currentVelocity;

    [SerializeField] Shader _transfer = null;
    [SerializeField] Shader _transferFace = null;

    void Start()
    {
        //転送シェーダーを持つ部位を取得
        var parts = gameObject
            .GetDecendants()
            .Where(part => part.GetComponent<Renderer>())
            .Where(part => part.GetComponent<Renderer>().material.HasProperty(_threshold));
        
        foreach (var part in parts) {
            _materials.Add(part.GetComponent<Renderer>().material);
        }

        Debug.Log(_materials);
    }

    void Update()
    {
        foreach (var material in _materials) 
        {
            if (material.shader != _transfer && material.shader != _transferFace) return;

            var target = material.shader == _transferFace ? 1.5f : -1f;
            var height = Mathf.MoveTowards(material.GetFloat(_threshold), target, Time.deltaTime * 0.3f);
            material.SetFloat(_threshold, height);

            if (material.shader == _transfer && material.GetFloat(_threshold) <= -1f) {
                material.shader = Shader.Find(_mat2Shader[material.name]);
            }
            // else if(material.shader == _transferFace && material.GetFloat(_threshold) >= 1.5f) {
            //     material.shader = Shader.Find(_mat2Shader[material.name]);
            // }

            // if (material.name == "hair (Instance)") {
            //     GameObject.FindGameObjectWithTag("DebugText").GetComponent<Text>().text = material.shader.name;
            // }
        }
    }
}
