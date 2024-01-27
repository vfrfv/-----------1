public class Running : State
{
    private Attack _attack;

    public override void Enter()
    {
        _attack.enabled = true;
    }

    public override void Exit()
    {
        _attack.enabled = false;
    }
}
