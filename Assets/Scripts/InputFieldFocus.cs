using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldFocus : MonoBehaviour 
{
    void Start() 
    {
        Input.backButtonLeavesApp = true;
        
        var i = GetComponent<TMP_InputField>();
        i.ActivateInputField(); //InputFieldにフォーカスを持たせる
//        i.onEndEdit.AddListener(
//            delegate(string text) {
//                if (!string.IsNullOrEmpty(text)) {
//                    Debug.Log(text); //textの送り先
//                    i.text = "";
//                }
//                i.ActivateInputField(); //InputFieldにフォーカスを持たせる
//            }
//        );
    }
}