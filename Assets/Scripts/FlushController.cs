using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using UnityChan;

public class FlushController : MonoBehaviour
{
    GameObject _girl;

    Image _image;
    bool _isFlushing;
    float _time;
    [SerializeField] float _fadeTime = 5f;
    [SerializeField] float _speed = 0.05f;

    AudioSource _audioSource;
    [SerializeField] AudioClip _shine;
    [SerializeField] AudioClip _yahho;
    Animator animator;
    [SerializeField] AnimationClip smile;

    void Start()
    {
        _image = GetComponent<Image>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!_isFlushing) return;
        
        var alpha = 1 - QuintIn(_time, _fadeTime, 0f, 1f);
        _image.color = new Color(1f, 1f, 1f, alpha);
        _time += Time.deltaTime;

        if (_time >= _fadeTime) {
            _isFlushing = false;
            _girl.GetComponent<IdleChanger>().ChangeState();
            //_audioSource.PlayOneShot(_yahho);
            ////(レイヤーの番号、どれだけアニメを出すか0～１）
            //animator.SetLayerWeight(1, 1f);
            //animator.Play("smile1@unitychan");
        }
    }

    public void Flush(GameObject girl)
    {
        _isFlushing = true;
        _girl = girl;
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        _image.color = Color.white;
        _audioSource.PlayOneShot(_shine);
    }

    // https://qiita.com/pixelflag/items/e5ddf0160781170b671b
    public static float QuintIn(float t, float totaltime, float min, float max)
    {
        max -= min;
        t /= totaltime;
        return max * t * t * t * t * t + min;
    }
}
