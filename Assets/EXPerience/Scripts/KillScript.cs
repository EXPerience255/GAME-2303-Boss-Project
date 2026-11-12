using UnityEngine;

namespace EXPerience
{
    public class KillScript : MonoBehaviour
    {
        [SerializeField] protected float lifetime;

        void Start()
        {
            Destroy(gameObject, lifetime);
        }
    }
}