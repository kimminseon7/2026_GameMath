using UnityEngine;
using UnityEngine.SceneManagement;

public class ChaserEnemy : MonoBehaviour
{
    public Transform Player;
    public float rotationSpeed = 50f;
    public float detectionRange = 8f;
    public float dashSpeed = 15f;
    public float stopDistance = 1.2f;
    public bool isDashing = false;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (!isDashing)
        {
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            
        }
        
    }

    void CheckParry()
    {
        PlayerController pc = Player.GetComponent<PlayerController>();
    }
}
