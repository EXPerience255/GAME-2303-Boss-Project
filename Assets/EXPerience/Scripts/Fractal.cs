using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace EXPerience
{
    public class Fractal : MonoBehaviour
    {
        [Header("Landing")]
        public bool hasLanded;
        public bool isLanding;
        public float landingSize;
        public float groundBuffer;
        public float landingWait;
        public float landingSpeed;
        public UnityEvent OnLand;
        private Vector3 landingPosition;

        [Header("Effects")]
        public GameObject deathParticles;
        public LineRenderer landingRenderer;
        public ParticleSystem dropParticles;

        private void Start()
        {
            landingRenderer.widthMultiplier = 0;

            if (!hasLanded) StartCoroutine(Landing());
            else landingRenderer.enabled = false;
        }

        private void Update()
        {
            if (!hasLanded) landingRenderer.widthMultiplier = Mathf.Lerp(landingRenderer.widthMultiplier, 1.75f, Time.deltaTime / landingWait);
            if (isLanding)
            {
                transform.position = Vector3.Lerp(transform.position, landingPosition, Time.deltaTime * landingSpeed);
                if (Vector3.Distance(transform.position, landingPosition) <= 0.1f)
                {
                    transform.position = landingPosition;
                    isLanding = false;
                    hasLanded = true;
                    dropParticles.Stop(false, ParticleSystemStopBehavior.StopEmitting);
                    Destroy(landingRenderer.gameObject);
                    OnLand?.Invoke();
                }
            }
        }

        public void Die()
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        IEnumerator Landing()
        {
            Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 50f, LayerMask.GetMask("Environment"));
            landingPosition = hit.point;

            landingRenderer.positionCount = 4;
            landingRenderer.SetPosition(0, landingPosition + new Vector3(landingSize / 2, groundBuffer, landingSize / 2));
            landingRenderer.SetPosition(1, landingPosition + new Vector3(landingSize / 2, groundBuffer, landingSize / -2));
            landingRenderer.SetPosition(2, landingPosition + new Vector3(landingSize / -2, groundBuffer, landingSize / -2));
            landingRenderer.SetPosition(3, landingPosition + new Vector3(landingSize / -2, groundBuffer, landingSize / 2));

            yield return new WaitForSeconds(landingWait);
            isLanding = true;
        }

        public void Reposition()
        {
            // TODO: Reposition at random
            transform.position = new Vector3(0, 30, 0);
            isLanding = false;
            landingWait = 1;
            Start();
        }
    }
}