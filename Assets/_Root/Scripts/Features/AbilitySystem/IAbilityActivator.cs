using Game.Car;
using UnityEngine;

namespace Features.AbilitySystem
{
    internal interface IAbilityActivator
    {
        GameObject ViewGameObject { get; }
        Rigidbody2D BodyRigidbody { get; }
        CarModel Model { get; }
    }
}
