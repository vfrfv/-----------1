using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
[RequireComponent(typeof(IHelth))]
public class SmoothHealthBar : MonoBehaviour
{
    [SerializeField] private GameObject _helthSourse;

    private IHelth _health;
    private Coroutine _coroutine;
    private Slider _slider;

    private void Awake()
    {
        _health = _helthSourse.GetComponent<IHelth>();
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _health.Changed += Fill;
    }

    private void OnDisable()
    {
        _health.Changed -= Fill;
    }

    private void Fill(float currentValue)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(SmoothlyChange(currentValue));
    }

    private IEnumerator SmoothlyChange(float currentValue)
    {
        float degreeVolumeChange = 10f;
        bool IsChanges = true;

        while (IsChanges)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, currentValue, degreeVolumeChange * Time.deltaTime);

            yield return null;
        }
    }
}
