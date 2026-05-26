using UnityEngine;

public class MineExplosion : MonoBehaviour
{
    public float explodeTime = 2f;

    public float explosionRadius = 3f;
    public float pushForce = 5f;

    void Start()
    {
        Invoke("Explode", explodeTime);
    }

    void Explode()
    {
        Debug.Log("지뢰 폭발");

        Collider[] hits =
            Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hit in hits)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // 폭발 방향 계산
                Vector3 dir =
                    (hit.transform.position - transform.position).normalized;

                // 위쪽 힘 추가
                dir.y += 0.5f;

                // 직접 속도 적용
                rb.linearVelocity = dir * pushForce;
            }
        }

        Destroy(gameObject);
    }
}