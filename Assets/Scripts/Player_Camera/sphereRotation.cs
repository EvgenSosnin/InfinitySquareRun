
using UnityEngine;

public class sphereRotation : MonoBehaviour
{
    public float speed = 3f;

    void Update()
    {

        float x = 45 * Time.deltaTime;
        float z = 45 * Time.deltaTime;
        float y = 0;

        transform.Rotate(new Vector3(x, y, z));

        
    }
}
