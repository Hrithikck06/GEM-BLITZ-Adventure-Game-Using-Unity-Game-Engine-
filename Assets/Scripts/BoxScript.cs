using UnityEngine;

public class BoxScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(10f, 10f, 10f);
        transform.rotation = Quaternion.Euler(0f, 45f, 0f);
        transform.localScale = new Vector3(10f, 10f, 10f);
    }
}
