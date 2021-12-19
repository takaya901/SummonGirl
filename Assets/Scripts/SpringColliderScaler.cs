using System;
using UnityEngine;
using UnityChan; // Scope for SpringCollider/SpringBone
 
// https: //dennou-note.blogspot.com/2015/04/sd.html
public class SpringColliderScaler : MonoBehaviour 
{
    float resizeMag; //!< Resize number
 
    void ChangeRadius_Spring(GameObject go)
    {
        //SpringCollider[] cs_sc = go.GetComponentsInChildren<SpringCollider>(true);//note: find to hide object too when set true.
        SpringCollider[] cs_sc = go.GetComponentsInChildren<SpringCollider>();
//        Debug.Log ("Found SpringCollider="+cs_sc.Length);
        foreach (var t in cs_sc) {
            t.radius *= resizeMag;
        }
        SpringBone[] cs_sb = go.GetComponentsInChildren<SpringBone>();
//        Debug.Log ("Found SpringBone="+cs_sb.Length);
        foreach (var t in cs_sb) {
            t.radius *= resizeMag;
        }
    }

    private void Update()
    {
        
    }

    void Awake()
    {
        resizeMag = transform.localScale.x * 0.6f;
        ChangeRadius_Spring(gameObject);
    }
}