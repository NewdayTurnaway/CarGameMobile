using JoostenProductions;
using UnityEngine;

namespace Game.Car
{
    internal sealed class CarView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _bodyRigidbody;
        private Transform _bodyTransform;

        private float _xPos;

        public Rigidbody2D BodyRigidbody => _bodyRigidbody;

        private void Start()
        {
            _bodyTransform = _bodyRigidbody.transform;
            _xPos = _bodyTransform.position.x;
            UpdateManager.SubscribeToUpdate(KeepXPosition);
        }

        private void OnDestroy() =>
            UpdateManager.UnsubscribeFromUpdate(KeepXPosition);

        private void KeepXPosition() => 
            _bodyTransform.position = new Vector2(_xPos, _bodyTransform.position.y);
    }
}
