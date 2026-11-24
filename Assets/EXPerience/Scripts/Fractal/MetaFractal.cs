using UnityEngine;

namespace EXPerience
{
    public class MetaFractal : Fractal
    {
        [Header("Spawn Amount")]
        public int spawnAmount;

        public override void Die()
        {
            Ultimate ult = FindAnyObjectByType<Ultimate>();
            for (int i = 0; i < spawnAmount; i++)
            {
                Fractal fractal = FractalSpawner.Instance.SpawnFractal(false, transform.position + new Vector3(0, 2.5f, 0));
                if (!ult.enabled) fractal.countsTowardsTotal = false;
            }

            base.Die();
        }
    }
}