using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    public GameObject pointPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //Highest Y = 58
        //Lowest Y = 5.6

        //Highest X = 38.7
        //Lowest X = -72.2

        //Highest Z = -113
        //Lowest Z = -292

        for (int i = 0; i < Random.Range(10, 25); i++)
        {
            float randomY = Random.Range(5.6f, 58);
            float randomX = Random.Range(38.7f, -72.2f);
            float randomZ = Random.Range(-113, -292);

            Instantiate(pointPrefab, new Vector3(randomX, randomY, randomZ), Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));


        }

        // Update is called once per frame
        void Update()
    {
        

        
        }

        






    }
}
