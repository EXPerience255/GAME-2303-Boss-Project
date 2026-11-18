using System.Collections;
using UnityEngine;

namespace EXPerience
{
    public class PhaseOne : Phase
    {
        protected override IEnumerator FireLoop()
        {
            yield return new WaitForSeconds(1.5f);
            PlayShootWarning();
            yield return new WaitForSeconds(0.5f);
            Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            StartCoroutine(FireLoop());
        }

        protected override IEnumerator Invincibility()
        {
            PlayBarrierWarning();
            yield return new WaitForSeconds(0.5f);
            FractalSpawner.Instance.TryHealingFractal(0.05f);
            isProtected = true;
            yield return new WaitForSeconds(4);
            isProtected = false;
        }
    }
}