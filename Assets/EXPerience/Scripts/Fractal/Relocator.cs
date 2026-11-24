using UnityEngine;

namespace EXPerience
{
    public class Relocator : MonoBehaviour
    {
        Fractal fractal;

        private void Awake() => fractal = GetComponent<Fractal>();

        private void Start()
        {
            RaycastHit[] hits = Physics.BoxCastAll(transform.position, new Vector3(7, 49, 7), Vector3.down);
            foreach (RaycastHit hit in hits)
            {
                Fractal hitFractal = hit.collider.gameObject.GetComponent<Fractal>();
                if (hitFractal == null) continue;
                else if (hitFractal == fractal) continue;
                else
                {
                    if (fractal.isLanding) fractal.Reposition();
                    else fractal.Reposition(3);
                    break;
                }
            }
        }
    }
}