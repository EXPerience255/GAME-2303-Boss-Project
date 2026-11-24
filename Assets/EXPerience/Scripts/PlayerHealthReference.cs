using UnityEngine;

namespace EXPerience
{
    public class PlayerHealthReference : MonoBehaviour
    {
        public static PlayerHealthReference Instance;
        public int storedHealth;
        public int maxHealth;

        private void Awake() => Instance = this;
        public void SetMax(int health)
        {
            maxHealth = health;
            storedHealth = health;
        }
        public void ChangeHealth(Damage damage) => storedHealth -= damage.amount;
    }
}