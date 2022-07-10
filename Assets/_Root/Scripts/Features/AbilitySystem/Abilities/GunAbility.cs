using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Features.AbilitySystem.Abilities
{
    internal sealed class GunAbility : IAbility
    {
        private const float Y_OFFSET = 1.5f;

        private readonly AbilityItemConfig _config;


        public GunAbility([NotNull] AbilityItemConfig config) =>
            _config = config ?? throw new ArgumentNullException(nameof(config));


        public void Apply(IAbilityActivator activator)
        {
            GameObject projectile = Object.Instantiate(_config.Projectile);
            Rigidbody2D projectileRigidbody = projectile.GetComponent<Rigidbody2D>();
            
            Vector3 viewPosition = activator.BodyRigidbody.transform.position;
            projectile.transform.position = new(viewPosition.x, viewPosition.y + Y_OFFSET, viewPosition.z);
            
            Vector3 force = activator.ViewGameObject.transform.right * _config.Value;
            projectileRigidbody.AddForce(force, ForceMode2D.Force);
        }
    }
}
