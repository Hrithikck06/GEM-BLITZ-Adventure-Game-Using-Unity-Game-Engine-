using UnityEngine;

public class GemsManager : MonoBehaviour
{
    public GameObject Gems;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        for (int i=0;i<=4; i++)
        {
            var pos = new Vector3(Random.Range(-45f, 30f), 0f, Random.Range(-20f, 20f));
            Instantiate(Gems, pos, Quaternion.identity);
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
