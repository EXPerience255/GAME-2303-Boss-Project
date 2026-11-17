using UnityEngine;

namespace EXPerience
{
    public class BossCore : MonoBehaviour
    {
        Transform player;
        Transform core;
        [SerializeField] float rotationSpeed;
        public int fractalsLeft;
        public int phase;
        public Phase[] phases;

        private void Awake()
        {
            player = FindAnyObjectByType<PlayerLogic>().transform;
            core = transform;
        }

        private void Update()
        {
            Vector3 playerDirection = (player.position - core.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            core.rotation = Quaternion.Slerp(core.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }

        public void TakeHit() => phases[phase - 1].TakeHit();
    }
}