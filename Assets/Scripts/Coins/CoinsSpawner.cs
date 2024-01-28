using System.Collections;
using UnityEngine;

public class CoinsSpawner : MonoBehaviour
{
    [SerializeField] private Coin _prefabCoin;
    [SerializeField] private Transform _spawnPoints;

    private Coroutine _coroutine;
    private Coin _coin;

    public void Start()
    {
        _coin = Instantiate(_prefabCoin, _spawnPoints.position, Quaternion.identity);      
        _coin.Colected += OnColected;
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        _coin.Colected -= OnColected;
    }

    private void OnColected()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(Enaibl());
    }

    private IEnumerator Enaibl()
    {
        float appearanceTimer = 3f;

        yield return new WaitForSeconds(appearanceTimer);

        _coin.gameObject.SetActive(true);
    }
}
