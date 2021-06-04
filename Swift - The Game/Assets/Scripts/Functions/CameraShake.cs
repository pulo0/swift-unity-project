using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraShake : MonoBehaviour
{
   public IEnumerator Shake(float duration, float magnitude)
   {
       var startPos = transform.localPosition;
       var elapsed = 0f;

       while (elapsed < duration)
       {
           const float defaultShakeValue = 1f;
           var x = Random.Range(-defaultShakeValue, defaultShakeValue) * magnitude;
           var y = Random.Range(-defaultShakeValue, defaultShakeValue) * magnitude;

           transform.localPosition = new Vector3(x, y, startPos.z);

           elapsed += Time.deltaTime;

           yield return null; 
       }

       transform.localPosition = startPos;
   } 
}
