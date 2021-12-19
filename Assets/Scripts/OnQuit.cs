using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnQuit : MonoBehaviour
{
    [SerializeField] AudioClip[] _bye;
    [SerializeField] AudioClip _onQuitVoice;
    [SerializeField] AudioClip _onCanceledVoice;
    AudioSource _audioSource;

    bool _isQuiting;
    bool _isQuit, _isCanceled;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 終了ボイスの再生が終わってからアプリ終了
        if (_isQuiting && !_audioSource.isPlaying) {
            Application.Quit();
        }
        //if (_isQuit)
        //{
        //    _audioSource.Stop();
        //    _audioSource.PlayOneShot(_bye[Random.Range(0, _bye.Length)]);
        //    _isQuiting = true;
        //    _isQuit = false;
        //}
        //else if (_isCanceled)
        //{
        //    _audioSource.Stop();
        //    _audioSource.PlayOneShot(_onCanceledVoice);
        //    _isCanceled = false;
        //}

        if (Input.GetKeyDown(KeyCode.Escape)) {
            ShowQuitDialog();
        }
    }

    /// <summary>
    /// 戻るボタンが押されたら確認のダイアログを表示
    /// </summary>
    void ShowQuitDialog()
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(_onQuitVoice);

        CBNativeDialog.Instance.Show(title: "",
            message: "本当にお別れしますか？",
            positiveButtonTitle: "はい",
            positiveButtonAction: () => { QuitApplication(); },
            negativeButtonTitle: "もうちょっとだけ…",
            negativeButtonAction: () => { OnCanceled(); });
    }

    void QuitApplication()
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(_bye[Random.Range(0, _bye.Length)]);
        _isQuiting = true;
    }

    void OnCanceled()
    {
        _audioSource.Stop();
        _audioSource.PlayOneShot(_onCanceledVoice);
    }
}
