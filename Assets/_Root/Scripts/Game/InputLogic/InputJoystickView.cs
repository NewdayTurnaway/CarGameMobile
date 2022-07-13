using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Game.InputLogic
{
    internal sealed class InputJoystickView : BaseInputView
    {
        [SerializeField] private float _inputMultiplier = 10;

        protected override void Move()
        {
            float axisOffset = CrossPlatformInputManager.GetAxis(Constants.Inputs.HORIZONTAL);
            float moveValue = _inputMultiplier * Time.deltaTime * axisOffset;

            float abs = Mathf.Abs(moveValue);
            float sign = Mathf.Sign(moveValue);

            if (sign > 0)
                OnRightMove(abs);
            else if (sign < 0)
                OnLeftMove(abs);
        }
    }
}
