using System.Collections;
using UnityEngine;

namespace EXPerience
{
    public class Turret : MonoBehaviour
    {
        bool activated;
        Transform player;

        [Header("Stats")]
        public float rotationSpeed;
        public float fireDelay;
        public float chargeTime;

        [Header("Effects and References")]
        public GameObject bulletPrefab;
        public Transform turretBase;
        public Transform shootPoint;
        public ParticleSystem shotWarning;

        private void Awake()
        {
            player = FindAnyObjectByType<PlayerLogic>().transform;
        }

        public void ActivateTurret()
        {
            activated = true;
            StartCoroutine(FireLoop());
        }

        private void Update()
        {
            if (!activated) return;

            Vector3 playerDirection = (player.position - turretBase.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            turretBase.transform.rotation = Quaternion.Slerp(turretBase.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }

        private IEnumerator FireLoop()
        {
            yield return new WaitForSeconds(fireDelay);
            shotWarning.Play();
            yield return new WaitForSeconds(chargeTime);
            Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            StartCoroutine(FireLoop());
        }
    }
}