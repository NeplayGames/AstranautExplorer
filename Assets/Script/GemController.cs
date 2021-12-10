using System;
using UnityEngine;
    

public class GemController : MonoBehaviour
{
   public static event Action<GameObject> CollectGem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Astraunut"))
        {
            if(CollectGem!=null)
                CollectGem(this.gameObject);
        }
    }
}
