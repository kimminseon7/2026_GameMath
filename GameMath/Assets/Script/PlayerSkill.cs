using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSkill : MonoBehaviour
{
    [Header("Bomb")]
    public GameObject bombPrefab;

    [Header("Mine")]
    public GameObject minePrefab;

    public float mineSpawnDistance = 20f;

    void Update()
    {
        // Q 키 : 폭탄 발사
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            ShootBomb();
        }

        // E 키 : 지뢰 설치
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            SpawnMine();
        }
    }

    void ShootBomb()
    {
        Vector3 spawnPos =
    transform.position +
    transform.forward * 6f +
    Vector3.up * 1f;

        Instantiate(
            bombPrefab,
            spawnPos,
            transform.rotation
        );
    }

    void SpawnMine()
    {
        Vector3 spawnPos =
            transform.position +
            transform.forward * mineSpawnDistance;

        Instantiate(
            minePrefab,
            spawnPos,
            Quaternion.identity
        );
    }
}