using UnityEngine;

namespace EXPerience
{
    public class MetaFractal : Fractal
    {
        [Header("Spawn Amount")]
        public int spawnAmount;

        public override void Die()
        {
            for (int i = 0; i < spawnAmount; i++)
                FractalSpawner.Instance.SpawnFractal(false, transform.position + new Vector3(0, 2.5f, 0)).countsTowardsTotal = false;

            base.Die();
        }
    }
}