using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    [Header("Map Size")]
    public int width = 30;
    public int depth = 30;

    [Header("Height")]
    public int maxHeight = 10;

    [Header("Water")]
    public int waterLevel = 3;

    [Header("Blocks")]
    public GameObject dirtPrefab;
    public GameObject grassPrefab;
    public GameObject waterPrefab;

    float offsetX;
    float offsetZ;

    void Start()
    {
        RandomizeOffset();
        GenerateTerrain();
        GenerateWater();
    }

    void RandomizeOffset()
    {
        offsetX = Random.Range(0f, 1000f);
        offsetZ = Random.Range(0f, 1000f);
    }

    void GenerateTerrain()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                int height = GetHeight(x, z);

                for (int y = 0; y <= height; y++)
                {
                    GameObject block;

                    if (y == height)
                        block = grassPrefab;
                    else
                        block = dirtPrefab;

                    Instantiate(
                        block,
                        new Vector3(x, y, z),
                        Quaternion.identity,
                        transform);
                }
            }
        }
    }

    int GetHeight(int x, int z)
    {
        float nx = (x + offsetX) * 0.25f;
        float nz = (z + offsetZ) * 0.25f;

        float value =
            Mathf.Sin(nx) +
            Mathf.Cos(nz);

        value = (value + 2f) / 4f;

        return Mathf.RoundToInt(value * maxHeight);
    }

    void GenerateWater()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                int height = GetHeight(x, z);

                if (height < waterLevel)
                {
                    for (int y = height + 1; y <= waterLevel; y++)
                    {
                        Instantiate(
                            waterPrefab,
                            new Vector3(x, y, z),
                            Quaternion.identity,
                            transform);
                    }
                }
            }
        }
    }
}