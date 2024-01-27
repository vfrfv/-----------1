using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pharmacy : MonoBehaviour
{
    public event UnityAction Colected;

    public void ToCollect()
    {
        gameObject.SetActive(false);

        Colected?.Invoke();
    }
}
