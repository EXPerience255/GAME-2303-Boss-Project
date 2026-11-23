using UnityEngine;

namespace EXPerience
{
    public class FractalSpawner : MonoBehaviour
    {
        public static FractalSpawner Instance;
        public Vector3 minBounds, maxBounds;
        public GameObject healingFractal;
        public GameObject[] basicFractals, advancedFractals;
        public GameObject[] basicEffects, advancedEffects;

        private void Awake()
        {
            Instance = this;
        }

        public Fractal SpawnFractal(bool isAdvanced, Vector3 effectSource)
        {
            GameObject fractal;
            GameObject effect;
            int fractalSelect = Random.Range(0, basicFractals.Length);
            if (isAdvanced) 
            {
                fractal = advancedFractals[fractalSelect];
                effect = advancedEffects[fractalSelect];
            }
            else
            {
                fractal = basicFractals[fractalSelect];
                effect = basicEffects[fractalSelect];
            }

            Vector3 fractalPosition = new Vector3
            (
                Random.Range(minBounds.x, maxBounds.x),
                Random.Range(minBounds.y, maxBounds.y),
                Random.Range(minBounds.z, maxBounds.z)
            );

            Fractal f = Instantiate(fractal, fractalPosition, Quaternion.LookRotation(Vector3.back, Vector3.up)).GetComponent<Fractal>();
            ParticleSystem p = Instantiate(effect, effectSource, Quaternion.LookRotation(Vector3.up, Vector3.up)).GetComponent<ParticleSystem>();
            p.Play();
            return f;
        }

        public void TryHealingFractal(float chance)
        {
            Vector3 fractalPosition = new Vector3
            (
                Random.Range(minBounds.x, maxBounds.x),
                Random.Range(minBounds.y, maxBounds.y),
                Random.Range(minBounds.z, maxBounds.z)
            );
            float attempt = Random.Range(0f, 1f);

            if (attempt > 1 - chance)
                Instantiate(healingFractal, fractalPosition, Quaternion.LookRotation(Vector3.back, Vector3.up)).GetComponent<Fractal>().countsTowardsTotal = false;
        }

        [ContextMenu("Spawn Basic Fractal")]
        private void TestSpawnBasic() => SpawnFractal(false, new Vector3(0, 0, 0));

        [ContextMenu("Spawn Advanced Fractal")]
        private void TestSpawnAdvanced() => SpawnFractal(true, new Vector3(0, 0, 0));
    }
}