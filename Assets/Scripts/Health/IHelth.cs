using System;

public interface IHelth
{
    event Action<float> Changed;

    int Value { get; }
}
