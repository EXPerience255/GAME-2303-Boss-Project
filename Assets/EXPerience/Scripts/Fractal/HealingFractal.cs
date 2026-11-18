using UnityEngine;

namespace EXPerience
{
    public class HealingFractal : Fractal
    {
        [Header("Heal Amount")]
        public int healAmount;

        public override void Die()
        {
            Damage heal = new Damage
            {
                amount = -healAmount,
                direction = Vector3.zero,
                knockbackForce = 0
            };
            FindAnyObjectByType<PlayerLogic>().GetComponent<Damageable>().Hit(heal);

            base.Die();
        }
    }
}