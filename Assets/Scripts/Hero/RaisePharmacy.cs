using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RaisePharmacy : MonoBehaviour
{
    public event UnityAction Colected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Pharmacy>(out Pharmacy pharmacy))
        {
            pharmacy.ToCollect();

            Colected?.Invoke();
        }
    }
}
