using TMPro;
using UnityEngine;

public class SimpleAngleLaunch : MonoBehaviour
{
    public TMP_InputField angleInputField;
    public GameObject spherePrefab;
    public Transform firePoint;
    public float force = 15f;

   public void Launch() // Canvas에 버튼 만들고 OnClick 이벤트에 연결
   {
        float angle = float.Parse(angleInputField.text);
        float rad = angle * Mathf.Deg2Rad;

        Vector3 dir = new Vector3(Mathf.Cos(rad), 0f, Mathf.Sin(rad));

        GameObject sphere = Instantiate(spherePrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = sphere.GetComponent<Rigidbody>();

        rb.AddForce((dir + Vector3.up * .3f).normalized * force, ForceMode.Impulse);
   }
    
    

    
   
}
