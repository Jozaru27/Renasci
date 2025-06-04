using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    [SerializeField] float fogRadius;
    [SerializeField] GameObject fogPlane;
    [SerializeField] LayerMask fogLayer;

    float radiusSqr { get { return fogRadius * fogRadius; } }
    Transform playerPos;

    Mesh mesh;
    Vector3[] vertices;
    Color[] colors;

    private void Start()
    {
        playerPos = GameObject.Find("Player").GetComponent<Transform>();
        fogPlane.SetActive(true);

        Initialize();
    }

    private void Update()
    {
        Ray ray = new Ray(transform.position, playerPos.position - transform.position);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, fogLayer))
        {
            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 v = fogPlane.transform.TransformPoint(vertices[i]);
                float distance = Vector3.SqrMagnitude(v - hit.point);

                if (distance < radiusSqr)
                {
                    float alpha = Mathf.Min(colors[i].a, distance / radiusSqr);
                    colors[i].a = alpha;
                }
            }

            UpdateColor();
        }
    }

    void Initialize()
    {
        mesh = fogPlane.GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        colors = new Color[vertices.Length];

        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.black;
        }

        UpdateColor();
    }

    void UpdateColor()
    {
        mesh.colors = colors;
    }
}
