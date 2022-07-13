using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Features.AbilitySystem.Abilities
{
    internal sealed class JumpAbility : IAbility
    {
        private readonly IAbilityItem _abilityItem;


        public JumpAbility([NotNull] IAbilityItem abilityItem) =>
            _abilityItem = abilityItem ?? throw new ArgumentNullException(nameof(abilityItem));


        public void Apply(IAbilityActivator activator)
        {
            Rigidbody2D carBody = activator.BodyRigidbody;
            float jumpHeight = activator.Model.JumpHeight;
            float forceValue = _abilityItem.Value + Mathf.Sqrt(-2.0f * Physics.gravity.y * jumpHeight);
            Vector2 force = activator.ViewGameObject.transform.up * forceValue;
            carBody.AddForce(force, ForceMode2D.Impulse);
        }
    }
}
