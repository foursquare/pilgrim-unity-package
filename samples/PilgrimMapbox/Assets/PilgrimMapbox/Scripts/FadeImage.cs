using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Animator))]
public class FadeImage : MonoBehaviour
{

    public UnityEvent _onFadeOutComplete;

    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void FadeOut()
    {
        _animator.SetTrigger("FadeOut");
    }

    public void FadeIn()
    {
        _animator.SetTrigger("FadeIn");
    }

    private void FadeOutComplete()
    {
        if (_onFadeOutComplete != null)
        {
            _onFadeOutComplete.Invoke();
        }
    }

}
