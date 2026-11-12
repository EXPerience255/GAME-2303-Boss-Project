using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace EXPerience
{
    public class Chaser : MonoBehaviour
    {
        bool activated;
        bool charging;
        Transform player;
        Navigator nav;
        Rigidbody rb;
        Vector3 targetNode;
        int nodeIndex;

        [Header("Stats")]
        public float detectTime;
        private float detectTimer;
        public float moveSpeed;
        public float chargeSpeed;
        public float chargeDistance;
        public float chargeTime;

        [Header("Effects")]
        public Renderer fuse;
        public ParticleSystem fuseParticles;
        public Material fusePrimedMaterial;
        public GameObject explosion;
        public SpriteRenderer eye;
        public Sprite chargeEyeSprite;

        private void Awake()
        {
            nav = GetComponent<Navigator>();
            rb = GetComponent<Rigidbody>();
            player = FindAnyObjectByType<PlayerLogic>().transform;
        }

        public void ActivateChaser()
        {
            activated = true;
        }

        private void Update()
        {
            if (!activated) return;

            detectTimer -= Time.deltaTime;
            if (detectTimer < 0) nav.CalculatePathToPosition(player.position);

            targetNode = nav.PathNodes[nodeIndex];
            Vector3 dirToNode = targetNode - transform.position;
            dirToNode.y = 0;
            dirToNode.Normalize();
            transform.forward = dirToNode;

            float distanceToNode = Vector3.Distance(targetNode, transform.position);
            if (distanceToNode < 3)
            {
                nodeIndex++;
                if (nodeIndex >= nav.PathNodes.Count)
                {
                    nodeIndex = 0;
                    nav.CalculatePathToPosition(player.position);
                    return;
                }
            }

            Vector3 targetVelocity = transform.forward * moveSpeed;
            targetVelocity.y = rb.linearVelocity.y;
            rb.linearVelocity = targetVelocity;

            if (!charging)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, player.position);
                if (distanceToPlayer < chargeDistance) Charge();
            }
            else chargeTime -= Time.deltaTime;
            if (chargeTime < 0) Explode();
        }

        private void Charge()
        {
            charging = true;
            moveSpeed = chargeSpeed;
            eye.sprite = chargeEyeSprite;
            fuse.material = fusePrimedMaterial;
            fuseParticles.gameObject.SetActive(true);
        }

        private void Explode()
        {
            Instantiate(explosion, transform.position + new Vector3(0, 2.5f, 0), Quaternion.identity);
            GetComponent<Fractal>().Die();
        }
    }
}