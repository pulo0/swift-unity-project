using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraShake : MonoBehaviour
{
   public IEnumerator Shake(float duration, float magnitude)
   {
       Vector3 startPos = transform.localPosition;

       float elapsed = 0f;

       while (elapsed < duration)
       {
           float defaultShakeValue = 1f;

           float x = Random.Range(-defaultShakeValue, defaultShakeValue) * magnitude;
           float y = Random.Range(-defaultShakeValue, defaultShakeValue) * magnitude;

           transform.localPosition = new Vector3(x, y, startPos.z);

           elapsed += Time.deltaTime;

           yield return null; 
       }

       transform.localPosition = startPos;
   } 
}
