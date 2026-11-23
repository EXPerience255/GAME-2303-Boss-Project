using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace EXPerience
{
    public class Ultimate : Phase
    {
        [SerializeField] GameManager manager;

        protected override void OnEnable()
        {
            FractalSpawner.Instance.TryHealingFractal(1f);
            base.OnEnable();
        }

        protected override IEnumerator FireLoop()
        {
            yield return new WaitForEndOfFrame();
        }

        protected override IEnumerator Invincibility()
        {
            Fractal[] fractalsLeft = FindObjectsByType<Fractal>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            core.fractalsLeft = fractalsLeft.Length + 10;

            for (var i = 0; i < 5; i++)
            {
                FractalSpawner.Instance.SpawnFractal(false, transform.position);
                yield return new WaitForSeconds(0.2f);
                FractalSpawner.Instance.SpawnFractal(true, transform.position);
                yield return new WaitForSeconds(0.8f);
            }
            
            yield return new WaitUntil(() => core.fractalsLeft == 0);
            manager.GoToNextLevel();
        }
    }
}