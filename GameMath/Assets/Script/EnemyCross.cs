using UnityEngine;

public class EnemyCross : MonoBehaviour
{
    public Transform target;

    Vector3 CrossProduct(Vector3 A, Vector3 B)
    {
        return new Vector3(
            A.y * B.z - A.z * B.y,
            A.z * B.x - A.x * B.z,
            A.x * B.y - A.y * B.x
        );
    }

    void Update()
    {
        Vector3 forward = transform.forward;
        Vector3 dirToTarget = (target.position - transform.position).normalized;

        Vector3 cross = CrossProduct(forward, dirToTarget);

        if (cross.y > 0.1f)
        {
            Debug.Log("적이 왼쪽에 있습니다.");
        }
        else if (cross.y < -0.1f)
        {
            Debug.Log("적이 오른쪽에 있습니다.");
        }
    }
}
