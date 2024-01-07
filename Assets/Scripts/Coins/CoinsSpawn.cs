using System.Collections;
using UnityEngine;

public class CoinsSpawn : MonoBehaviour
{
    [SerializeField] Coin _prefabCoin;
    [SerializeField] Transform _spawnPoints;

    private Coroutine _coroutine;
    private Coin _coin;

    public void Start()
    {
        _coin = Instantiate(_prefabCoin, _spawnPoints.position, Quaternion.identity);

        _coin.Colected += OnColected;
    }

    private void OnColected()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(Eneibl());
    }

    private IEnumerator Eneibl()
    {
        float appearanceTimer = 3f;

        yield return new WaitForSeconds(appearanceTimer);

        _coin.gameObject.SetActive(true);
    }
}
