
using UnityEngine;

public class SpawnObejct : MonoBehaviour
{
    public GameObject templatePrefab;

    void Start()
    {
        Instantiate(templatePrefab,new Vector3(15,0,0), Quaternion.identity);
    }
}
