using System.Collections;
using UnityEngine;

namespace EXPerience
{
    public abstract class Phase : MonoBehaviour
    {
        protected Damageable health;
        protected BossCore core;

        [Header("Barrier")]
        public Transform barrier;
        public float barrierSpeed;
        public bool isProtected;
        public ParticleSystem[] barrierWarning;

        [Header("Turret")]
        public GameObject bulletPrefab;
        public Transform shootPoint;
        public ParticleSystem[] shotWarning;

        [Header("Phase")]
        public int requiredHits;
        [SerializeField] private int currentHits;
        public Phase nextPhase;

        protected virtual void OnEnable()
        {
            health = GetComponent<Damageable>();
            core = GetComponent<BossCore>();
            GetComponent<BossCore>().phase++;
            StartCoroutine(FireLoop());
            StartCoroutine(Invincibility());
        }

        private void OnDisable() => Destroy(this);

        private void Update()
        {
            if (isProtected) barrier.localScale = Vector3.Lerp(barrier.localScale, new Vector3(1.2f, 1.2f, 1.2f), Time.deltaTime * barrierSpeed);
            else barrier.localScale = Vector3.Lerp(barrier.localScale, new Vector3(0.9f, 0.9f, 0.9f), Time.deltaTime * barrierSpeed);

            health.enabled = !isProtected;
        }

        public void TakeHit()
        {
            currentHits++;
            if (currentHits >= requiredHits)
            {
                nextPhase.enabled = true;
                enabled = false;
            }
            else StartCoroutine(Invincibility());
        }

        protected void PlayShootWarning()
        {
            foreach (ParticleSystem sys in shotWarning) sys.Play();
        }

        protected void PlayBarrierWarning()
        {
            foreach (ParticleSystem sys in barrierWarning) sys.Play();
        }

        protected abstract IEnumerator FireLoop();
        protected abstract IEnumerator Invincibility();
    }
}