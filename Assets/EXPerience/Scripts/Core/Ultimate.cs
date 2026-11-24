using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EXPerience
{
    public class Ultimate : Phase
    {
        [Header("Ultimate Exclusive")]
        [SerializeField] private GameManager manager;
        [SerializeField] private GameObject fireParticles;
        [SerializeField] private SpriteRenderer eye;
        [SerializeField] private Sprite newEye;
        [SerializeField] private GameObject explosion;
        [SerializeField] private GameObject[] destroyOnExplode;

        protected override void OnEnable()
        {
            FractalSpawner.Instance.TryHealingFractal(1f);
            FractalSpawner.Instance.TryHealingFractal(1f);
            base.OnEnable();
        }

        protected override IEnumerator FireLoop()
        {
            yield return new WaitForEndOfFrame();
        }

        protected override IEnumerator Invincibility()
        {
            eye.sprite = newEye;
            Fractal[] fractalsLeft = FindObjectsByType<Fractal>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            foreach (Fractal f in fractalsLeft)
                f.countsTowardsTotal = true;
            core.fractalsLeft = fractalsLeft.Length + 10;

            List<ParticleSystem> addedParticles = new List<ParticleSystem>();
            for (var i = 0; i < 5; i++)
            {
                GameObject addition = Instantiate(fireParticles, transform.position, Quaternion.identity);
                addedParticles.Add(addition.GetComponent<ParticleSystem>());
                FractalSpawner.Instance.SpawnFractal(false, transform.position);
                yield return new WaitForSeconds(0.2f);
                Fractal advancedFractal = FractalSpawner.Instance.SpawnFractal(true, transform.position);
                if (advancedFractal.GetComponent<MetaFractal>()) core.fractalsLeft += 2;
                yield return new WaitForSeconds(0.8f);
            }

            foreach (var p in addedParticles) p.Stop(false, ParticleSystemStopBehavior.StopEmitting);
            foreach (var g in destroyOnExplode) Destroy(g);
            GetComponent<MeshRenderer>().enabled = false;
            Instantiate(explosion, transform.position, Quaternion.identity);

            yield return new WaitUntil(() => core.fractalsLeft == 0);
            Debug.Log("Boss Complete!");
            manager.GoToNextLevel();
        }
    }
}