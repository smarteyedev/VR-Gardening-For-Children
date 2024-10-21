using UnityEngine;

public class WaterDroplet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Water"))
        {
            Destroy(gameObject);
        }
    }
}
