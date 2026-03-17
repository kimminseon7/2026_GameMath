using UnityEngine;

public class DotExample : MonoBehaviour
{
    public Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    // Update is called once per frame
    void Update()
    {
        
        
        Vector3 toPlayer = player.position - transform.position; // 플레이어를 보는 방향

        toPlayer.y = 0;

        Vector3 forward = transform.forward; // 적의 앞 방향 z+
        forward.y = 0;

        forward.Normalize();
        toPlayer.Normalize();

        float dot = Vector3.Dot(forward, toPlayer);

        if (dot > 0f)
        {
            Debug.Log("플레이어가 적 앞");
        }
        else if (dot < 0f)
        {
            Debug.Log("플레이어가 적 뒤");
        }
        else
        {
            Debug.Log("플레이어가 적의 옆쪽에 있음");
        }
    }
}
