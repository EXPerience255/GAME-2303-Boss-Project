using System.Collections;
using UnityEngine;

namespace EXPerience
{
    [RequireComponent(typeof(Rigidbody))]
    public class RecursiveProjectile : Projectile
    {
        Rigidbody rb;
        Transform player;

        public float redirectDelay;
        public float redirectFreeze;
        public float maxRedirects = 2;

        protected override void Start()
        {
            rb = GetComponent<Rigidbody>();
            player = FindAnyObjectByType<PlayerLogic>().transform;
            base.Start();
            StartCoroutine(RedirectLoop());
        }

        private IEnumerator RedirectLoop()
        {
            maxRedirects--;
            if (maxRedirects < 0) yield break;
            yield return new WaitForSeconds(redirectDelay);
            rb.linearVelocity = Vector3.zero;
            yield return new WaitForSeconds(redirectFreeze);
            transform.LookAt(player, Vector3.up);
            base.Start();
            StartCoroutine(RedirectLoop());
        }
    }
}