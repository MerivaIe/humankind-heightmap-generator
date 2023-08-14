using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class HexMesh : MonoBehaviour {

	Mesh hexMesh;
	List<Vector3> vertices;
	List<int> triangles;

	void Awake () {
		GetComponent<MeshFilter>().mesh = hexMesh = new Mesh();
		hexMesh.name = "Hex Mesh";
		vertices = new List<Vector3>();
		triangles = new List<int>();
	}

    public void Triangulate (Hashtable map) {
		hexMesh.Clear();
		vertices.Clear();
		triangles.Clear();
        foreach (DictionaryEntry kvPair in map) {
            Tile tile = (Tile)kvPair.Value;
			TriangulateTile(tile);
            Debug.Log("Triangulated tile with index " + (int)kvPair.Key);
		}
		hexMesh.vertices = vertices.ToArray();
		hexMesh.triangles = triangles.ToArray();
        Debug.Log("Vertices count = " + vertices.Count);
        Debug.Log("Triangles count = " + triangles.Count);
		hexMesh.RecalculateNormals();
	}

    void TriangulateTile (Tile tile) {
        Vector3 center = tile.transform.position;
		for (int i = 0; i < 6; i++) {
			AddTriangle(center, center + Hex.corners[i], center + Hex.corners[i+1]);
            //Debug.Log("i = " + i + ". Added triangle with center and two corners " + (vert1) + " and " + (vert2));
		}
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
