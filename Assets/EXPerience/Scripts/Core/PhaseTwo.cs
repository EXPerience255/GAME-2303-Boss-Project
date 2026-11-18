using System.Collections;
using UnityEngine;

namespace EXPerience
{
    public class PhaseTwo : Phase
    {
        protected override IEnumerator FireLoop()
        {
            yield return new WaitForSeconds(0.5f);
            PlayShootWarning();
            yield return new WaitForSeconds(0.5f);
            Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            StartCoroutine(FireLoop());
        }

        protected override IEnumerator Invincibility()
        {
            PlayBarrierWarning();
            yield return new WaitForSeconds(0.5f);
            isProtected = true;
            FractalSpawner.Instance.TryHealingFractal(0.25f);
            for (var i = 0; i < 4; i++) FractalSpawner.Instance.SpawnFractal(false, transform.position);
            core.fractalsLeft = 3;
            yield return new WaitUntil(() => core.fractalsLeft == 0);
            isProtected = false;
        }
    }
}