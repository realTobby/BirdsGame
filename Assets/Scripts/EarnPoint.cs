using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EarnPoint : MonoBehaviour
{
    public bool IsTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if(this.transform.CompareTag("finish"))
        {
            if(other.gameObject.GetComponent<BirdFly>().Points >= 6)
            {
                SceneManager.LoadScene(0);
            }
        }
        else
        {
            if (IsTriggered == false)
            {
                other.gameObject.GetComponent<AudioSource>().Play();
                other.gameObject.GetComponent<BirdFly>().Points += 1;
                IsTriggered = true;
                Destroy(this.gameObject);
            }
        }

        
        
    }
}
