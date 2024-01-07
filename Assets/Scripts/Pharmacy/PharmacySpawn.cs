using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PharmacySpawn : MonoBehaviour
{
    [SerializeField] Pharmacy _prefabPharmacy;
    [SerializeField] Transform[] _spawnPoints;

    private List<Pharmacy> _pharmacies = new List<Pharmacy>();
    private Pharmacy _pharmacy;
    private Coroutine _coroutine;

    public void Start()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            _pharmacy = Instantiate(_prefabPharmacy, _spawnPoints[i].position, Quaternion.identity);
            _pharmacy.gameObject.SetActive(false);

            _pharmacies.Add(_pharmacy);

            _pharmacy.Colected += OnColected;
        }

        OnColected();
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
        int appearanceTimer = 10;

        yield return new WaitForSeconds(appearanceTimer);

        int index = Random.Range(0, _spawnPoints.Length);

        _pharmacies[index].gameObject.SetActive(true);
    }
}
