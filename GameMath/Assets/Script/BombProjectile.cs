using UnityEngine;

public class BombProjectile : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 23f;
    public float gravity = 10f;

    [Header("Explosion")]
    public float explosionRadius = 5f;
    public float explosionForce = 15f;

    private Vector3 moveDir;
    private Vector3 velocity;

    private int bounceCount = 0;
    private bool exploded = false;

    void Start()
    {
        // 플레이어가 보는 방향으로 발사
        moveDir = transform.forward;

        // 초기 속도
        velocity = moveDir * moveSpeed;
    }

    void Update()
    {
        // 중력 적용
        velocity.y -= gravity * Time.deltaTime;

        // 이동
        transform.position += velocity * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 적에 닿으면 폭발
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Explode();
            return;
        }

        // 바닥 충돌 처리
        if (collision.gameObject.CompareTag("Ground"))
        {
            bounceCount++;

            Debug.Log("바닥 충돌 횟수 : " + bounceCount);

            Vector3 normal = collision.contacts[0].normal;

            // Reflect 없이 반사 계산
            velocity =
                velocity -
                2f * Vector3.Dot(velocity, normal) * normal;

            // 속도 감소
            velocity *= 0.6f;

            // 위로 살짝 튀게
            velocity.y += 5f;

            // 3번 튕기면 폭발
            if (bounceCount >= 3)
            {
                Explode();
            }
        }
    }

    void Explode()
    {
        if (exploded) return;

        exploded = true;

        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hit in hits)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 dir = (hit.transform.position - transform.position).normalized;

                rb.linearVelocity = dir * explosionForce;
            }
        }

        Destroy(gameObject);
    }
}