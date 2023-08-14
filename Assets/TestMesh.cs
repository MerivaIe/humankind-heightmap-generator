using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class TestMesh : MonoBehaviour {

	Mesh thisMesh;
	List<Vector3> vertices;
	List<int> triangles;

	void Awake () {
		GetComponent<MeshFilter>().mesh = thisMesh = new Mesh();
		thisMesh.name = "Hex Mesh";
		vertices = new List<Vector3>();
		triangles = new List<int>();
	}

    public void Triangulate () {
		thisMesh.Clear();
		vertices.Clear();
		triangles.Clear();
        Vector3 center = new Vector3(0f,0f,0f);
        Debug.Log("center = " + center);
        Vector3 vert1 = new Vector3();
        vert1 = center + Hex.corners[0];
        Debug.Log("vert1 = " + vert1);
        Vector3 vert2 = new Vector3();
        vert2 = center + Hex.corners[1];
        Debug.Log("vert2 = " + vert2);
        AddTriangle(center, vert1, vert2);
		thisMesh.vertices = vertices.ToArray();
        for (int i = 0; i < thisMesh.vertices.Length; i++) {
            Debug.Log("Loggin vertices: " + i + " is " + thisMesh.vertices[i]);
        }
		thisMesh.triangles = triangles.ToArray();
        for (int i = 0; i < thisMesh.triangles.Length; i++) {
            Debug.Log("Loggin triangle: " + i + " is " + thisMesh.triangles[i]);
        }
		thisMesh.RecalculateNormals();
	}

	void AddTriangle (Vector3 v1, Vector3 v2, Vector3 v3) {
		int vertexIndex = vertices.Count;
		vertices.Add(v1);
		vertices.Add(v2);
		vertices.Add(v3);
		triangles.Add(vertexIndex);
		triangles.Add(vertexIndex + 1);
		triangles.Add(vertexIndex + 2);
	}
}
