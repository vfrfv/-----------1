using UnityEngine;

public class Player : MonoBehaviour
{
    private StateMashine _stateMashine;

    private void Start()
    {
        _stateMashine = new StateMashine();
        _stateMashine.Initialize(new Walking());
    }
}
