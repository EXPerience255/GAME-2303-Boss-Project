using UnityEngine;

namespace EXPerience
{
    public class Relocator : MonoBehaviour
    {
        Fractal fractal;

        private void Awake() => fractal = GetComponentInParent<Fractal>();

        private void OnTriggerStay(Collider other)
        {
            Fractal oFractal = other.gameObject.GetComponent<Fractal>();
            if (oFractal == null) return;
            else
            {
                if (fractal.isLanding) fractal.Reposition();
                else fractal.Reposition(3);
            }
        }
    }
}