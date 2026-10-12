using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LastScene : MonoBehaviour
{
    public Image black;

    public IEnumerator FadeOut()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            black.color = new Color (0,0,0,t);
            yield return null;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
