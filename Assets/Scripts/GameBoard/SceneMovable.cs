using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMovable : MonoBehaviour
{
    public int dif;
    public float newPos;
    public float timeCounter;
    public float currentPos;
    float timeToMove = 1f;
    
    void Update(){
        if (newPos > currentPos){
            timeCounter+=Time.deltaTime;
            Hopping();
        }
    }

    void Hopping(){
        float vecZ = Mathf.Lerp(currentPos, newPos, timeCounter/timeToMove);
        transform.position = new Vector3(0, Mathf.Abs(Mathf.Sin(vecZ*Mathf.PI)), vecZ);
    }
}
