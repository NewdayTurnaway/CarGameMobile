using Profile;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = nameof(GameConfig), menuName = "Configs/" + nameof(GameConfig))]
    internal sealed class GameConfig : ScriptableObject
    {
        [field: SerializeField] public float SpeedCar { get; private set; }
        [field: SerializeField] public float JumpHeight { get; private set; }
        [field: SerializeField] public GameState InitialState { get; private set; }
    } 
}
