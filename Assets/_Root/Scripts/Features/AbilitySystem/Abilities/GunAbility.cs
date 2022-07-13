using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.AbilitySystem.Abilities
{
    internal sealed class GunAbility : IAbility
    {
        private const float Y_OFFSET = 1.5f;

        private readonly IAbilityItem _abilityItem;


        public GunAbility([NotNull] IAbilityItem abilityItem) =>
            _abilityItem = abilityItem ?? throw new ArgumentNullException(nameof(abilityItem));


        public void Apply(IAbilityActivator activator)
        {
            GameObject projectile = Object.Instantiate(_abilityItem.Projectile);
            Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
            
            Vector3 viewPosition = activator.BodyRigidbody.transform.position;
            projectile.transform.position = new(viewPosition.x, viewPosition.y + Y_OFFSET, viewPosition.z);
            
            Vector3 force = activator.ViewGameObject.transform.right * _abilityItem.Value;
            projectileRigidbody.AddForce(force, ForceMode2D.Force);
        }
    }
}
