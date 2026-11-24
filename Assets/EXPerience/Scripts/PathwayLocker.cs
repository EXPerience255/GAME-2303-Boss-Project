using UnityEngine;

public class PathwayLocker : MonoBehaviour
{
    public GameObject pathway;
    public GameObject barrier;

    public void LockPathway()
    {
        Destroy(pathway);
        barrier.SetActive(true);
    }
}
