using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Animator))]
public class FadeImage : MonoBehaviour
{

    [SerializeField]
    private UnityEvent _onFadeOutComplete;

    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void FadeOut()
    {
        _animator.SetTrigger("FadeOut");
    }

    private void FadeOutComplete()
    {
        if (_onFadeOutComplete != null)
        {
            _onFadeOutComplete.Invoke();
            Destroy(gameObject);
        }
    }

}
