using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int position;
    public int owner;

    public Building(BuildingData d){
        position = d.position;
        owner = d.owner;
    }
    public void Init(Building d){
        position = d.position;
        owner = d.owner;
    }
}
