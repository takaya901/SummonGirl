using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

//https: //qiita.com/shun-shun123/items/1aa646049474d0e244be
[RequireComponent(typeof(ARRaycastManager))]
public class Summoner : MonoBehaviour
{
    [SerializeField] GameObject _girlPrefab = null;
    [SerializeField] FlushController _flush = null;
    [SerializeField] GameObject _screenSpaceUI = null;
    [SerializeField] Animator animator;
    [SerializeField] AnimationClip smileAnim;

    GameObject _girl;
    ARRaycastManager _raycastManager;
    ARPlaneManager _planeManager;
    static List<ARRaycastHit> _hits = new List<ARRaycastHit>();
    public float Height { get; set; }

    /// <summary>
    /// Invoked whenever an object is placed in on a plane.
    /// </summary>
    // public static event Action OnPlacedGirl;

    void Start()
    {
        FadeManager.FadeIn();
        _raycastManager = GetComponent<ARRaycastManager>();
        _planeManager = GetComponent<ARPlaneManager>();
    }

    // 検出した平面をタッチしたら彼女を召喚
    void Update()
    {
        // 彼女召喚後はスキップ
        if (_girl) return;
        if (Input.touchCount <= 0) return;

        var touchPos = Input.GetTouch(0).position;
        if (!_raycastManager.Raycast(touchPos, _hits, TrackableType.Planes)) {
            return;
        }
        Summon();
    }

    void Summon()
    {
        // Raycastの衝突情報は距離によってソートされるため、0番目が最も近い場所でヒットした情報となります
        var hitPose = _hits[0].pose;
        var pos = hitPose.position;

        _girl = Instantiate(_girlPrefab, pos, Quaternion.identity);
        _girl.transform.localScale = Vector3.one * Height;
        _flush.Flush(_girl);

        // カメラに向くようにY軸のみ回転
        var lookRotation = Quaternion.LookRotation(Camera.main.transform.position - _girl.transform.position, Vector3.up);
        lookRotation.z = 0;
        lookRotation.x = 0;
        _girl.transform.rotation = lookRotation;

        _screenSpaceUI.SetActive(false);
        _planeManager.requestedDetectionMode = PlaneDetectionMode.None;
        Input.backButtonLeavesApp = false;
        //UIManagerにUI非表示のアクションを実行させる
        // OnPlacedGirl?.Invoke();
    }
}
