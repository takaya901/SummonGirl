using UnityEngine;
using Image = UnityEngine.UI.Image;

public class FlushController : MonoBehaviour
{
    Image _image;
    bool _isFlushing;
    float _time;
    const float LEAPTIME = 2f;
    [SerializeField] float _speed = 0.05f;

    AudioSource _audioSource;
    [SerializeField] AudioClip _shine;
    [SerializeField] AudioClip _yahho;

    void Start()
    {
        _image = GetComponent<Image>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!_isFlushing) return;

        //var t = Mathf.Min(_time / LEAPTIME, 1f);
        //var alpha = Mathf.Lerp(_image.color.a, 0f, _time * _speed);
        //var alpha = Mathf.MoveTowards(_image.color.a, 0f, Time.deltaTime * _speed);
        var alpha = Mathf.Max(0f, 1 - QuintIn(_time, 5f, 0f, _image.color.a));
        _image.color = new Color(1f, 1f, 1f, alpha);
        _time += Time.deltaTime;

        if (_image.color == Color.clear) {
            _isFlushing = false;
            _audioSource.PlayOneShot(_yahho);
        }
    }

    public void Flush()
    {
        _isFlushing = true;
        _image.color = Color.white;
        _audioSource.PlayOneShot(_shine);
    }

    public static float QuintIn(float t, float totaltime, float min, float max)
    {
        max -= min;
        t /= totaltime;
        return max * t * t * t * t * t + min;
    }
}
