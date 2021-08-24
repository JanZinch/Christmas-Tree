using System.Collections;
using UnityEngine;

[AddComponentMenu("Pool/PooledObject")]
public class PooledObject : MonoBehaviour
{       
    public IEnumerator ReturnToPool(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }

}
