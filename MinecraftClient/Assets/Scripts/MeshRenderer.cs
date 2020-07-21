using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRenderer : MonoBehaviour {

    public GameObject BasicCubeMesh;
    List<Vector3> Vertices = new List<Vector3>();
    List<int> Triangles = new List<int>();

    void Start() {
        //GenerateMesh(chunkData);
    }

    public void GenerateMesh(List<Vector3> chunk) {
        Mesh mesh = new Mesh();
        GameObject[] tmpMeshes;
        CombineInstance[] combineInstances = new CombineInstance[]();

        //Add textures
        for (int i = 0; i < chunk.Count; i++) {
            GameObject tmpMesh = Instantiate(BasicCubeMesh, chunk[i], Quaternion.identity, gameObject.transform);
            combineInstances[i].mesh = tmpMesh.GetComponent<MeshFilter>().sharedMesh;
            combineInstances[i].transform = tmpMesh.GetComponent<MeshFilter>().transform.localToWorldMatrix;
            tmpMeshes[i] = tmpMesh;
        }
        mesh.CombineMeshes(combineInstances);
        GetComponent<MeshFilter>().mesh = mesh;
        for (int i = 0; i < chunk.Count; i++)
            Destroy(tmpMeshes[i]);
        mesh.Optimize();
        GetComponent<MeshFilter>().mesh = mesh;
        //Recalculate Everything
    }
}
