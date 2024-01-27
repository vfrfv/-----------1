using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Hero))]
public class SuperAbility : MonoBehaviour
{
    private const string AnimationSuperAbility = "SuperAbility";

    [SerializeField] private Enemy _enemy;
    [SerializeField] private Animator _animator;

    private int _timeAbility = 6;
    private Hero _hero;
    private Coroutine _coroutine;
    private int _seconds = 0;

    private void Awake() => 
        _hero = GetComponent<Hero>();

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                enemy.Freeze();

                StopRunningCoriutine();

                _coroutine = StartCoroutine(StartAbility());
                _animator.SetTrigger(AnimationSuperAbility);
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                enemy.Unfreeze();
                StopRunningCoriutine();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _enemy.Unfreeze();

        StopRunningCoriutine();
    }

    private void StopRunningCoriutine()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
    }   

    private IEnumerator StartAbility()
    {
        int amountDelay = 1;
        var delay = new WaitForSeconds(amountDelay);

        while (_seconds < _timeAbility && _hero.Value < _hero.MaxValue)
        {
            _hero.AddLife(_enemy.GiveLife());
            _seconds++;

            yield return delay;
        }
    }
}
