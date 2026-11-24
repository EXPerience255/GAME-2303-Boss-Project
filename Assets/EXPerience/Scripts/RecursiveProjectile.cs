using System.Collections;
using UnityEngine;

namespace EXPerience
{
    [RequireComponent(typeof(Rigidbody))]
    public class RecursiveProjectile : Projectile
    {
        Rigidbody rb;
        Transform player;
        ParticleSystem particles;

        public float redirectDelay;
        public float redirectFreeze;
        public float maxRedirects = 2;

        protected override void Start()
        {
            rb = GetComponent<Rigidbody>();
            player = FindAnyObjectByType<PlayerLogic>().transform;
            particles = GetComponent<ParticleSystem>();
            base.Start();
            StartCoroutine(RedirectLoop());
        }

        private IEnumerator RedirectLoop()
        {
            maxRedirects--;
            if (maxRedirects < 0) yield break;
            yield return new WaitForSeconds(redirectDelay);
            rb.linearVelocity = Vector3.zero;
            particles.Play();
            yield return new WaitForSeconds(redirectFreeze);
            transform.LookAt(player, Vector3.up);
            base.Start();
            StartCoroutine(RedirectLoop());
        }
    }
}