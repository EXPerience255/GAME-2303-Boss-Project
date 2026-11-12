using UnityEngine;

namespace EXPerience
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        public float force;

        private void Start()
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);
        }

        public void DestroyProjectile()
        {
            Destroy(gameObject);
        }
    }
}