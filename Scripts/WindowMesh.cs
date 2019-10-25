using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 
 public class WindowMesh : MonoBehaviour {
 
     public Material material;
 
     Vector3[] verts = {
     new Vector3(-0.5f, 0.5f, 0.0f), new Vector3( 0.5f, 0.5f, 0.0f), 
     new Vector3( 0.5f,-0.5f, 0.0f), new Vector3(-0.5f,-0.5f, 0.0f),
     new Vector3(-0.2f, 0.3f, 0.0f), new Vector3( 0.2f, 0.3f, 0.0f), 
     new Vector3( 0.2f,-0.1f, 0.0f), new Vector3(-0.2f,-0.1f, 0.0f),
     new Vector3(-0.2f, 0.3f, 0.0f), new Vector3( 0.2f, 0.3f, 0.0f), 
     new Vector3( 0.2f,-0.1f, 0.0f), new Vector3(-0.2f,-0.1f, 0.0f),
     new Vector3(-0.2f, 0.3f, 0.1f), new Vector3( 0.2f, 0.3f, 0.1f), 
     new Vector3( 0.2f,-0.1f, 0.1f), new Vector3(-0.2f,-0.1f, 0.1f),
     new Vector3(-0.2f, 0.3f, 0.1f), new Vector3( 0.2f, 0.3f, 0.1f), 
     new Vector3( 0.2f,-0.1f, 0.1f), new Vector3(-0.2f,-0.1f, 0.1f)
     };
 
     int[] tris = {
         0,1,4, 1,5,4, 1,2,5, 2,6,5,
         2,3,6, 3,7,6, 3,0,7, 0,4,7,
         8,9,12, 9,13,12, 9,10,13, 10,14,13,
         10,11,14, 11,15,14, 11,8,15, 8,12,15
         //, 16,17,19, 17,18,19 // uncomment this line in order to close the window
     };
 
     void Start () {
         MeshFilter mF = gameObject.AddComponent<MeshFilter> (); // as MeshFilter;
         MeshRenderer render = gameObject.AddComponent<MeshRenderer> () as MeshRenderer;
         render.material = material;
         Mesh msh = new Mesh ();
         msh.vertices = verts;
         msh.triangles = tris;
         msh.RecalculateNormals ();
         mF.mesh = msh;
     }
 
     void Update () {
         
     }
 }