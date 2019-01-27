using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingData : MonoBehaviour
{
    public int position;
    public int owner;
    public BuildingData(int pos, int own){
        position = pos;
        owner = own;
    }
}
