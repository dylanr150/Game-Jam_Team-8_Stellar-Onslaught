using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 5f;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 velocity = new Vector3(0, bulletSpeed * Time.deltaTime, 0);

        pos += transform.rotation * velocity;
        transform.position = pos;
    }
}
