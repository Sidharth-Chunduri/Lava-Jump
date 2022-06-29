using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //shakes the camera a given amount for a given time
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector2 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        //conintues to shake until the time is complete
        while(elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector2(x, y);

            elapsed += Time.deltaTime;

            yield return null;
        }

        //moves the camera
        transform.localPosition = originalPos;
    }
}
