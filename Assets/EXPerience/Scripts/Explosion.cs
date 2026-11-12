using UnityEngine;


namespace EXPerience
{
    public class Explosion : KillScript
    {
        [SerializeField] float maxSize;

        private void Update()
        {
            Vector3 maxScale = new Vector3(maxSize, maxSize, maxSize);
            transform.localScale = Vector3.Lerp(transform.localScale, maxScale, Time.deltaTime / lifetime);
        }
    }
}