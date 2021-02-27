using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Player Data", order = 0)]
    public class PlayerData : ScriptableObject
    {
        public float speed;
    }
}