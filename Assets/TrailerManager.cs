using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct trailerOptions
{
    public Mesh mesh;
    public Material[] materials;
}
public class TrailerManager : MonoBehaviour
{
    public MeshFilter mesh;
    public trailerOptions[] trailerOptions;
    public TruckManager truckManager;
    public GameObject trailer;
    

    public void StartCargo()
    {
        trailer.transform.parent = null;
        trailer.SetActive(true);
        mesh.sharedMesh = trailerOptions[truckManager.cargo.cargoType].mesh;
        mesh.GetComponent<MeshRenderer>().materials = trailerOptions[truckManager.cargo.cargoType].materials;
    }

    public void StopCargo()
    {
        trailer.SetActive(false);
        trailer.transform.parent = transform;
    }

}
