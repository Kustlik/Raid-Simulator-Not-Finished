using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StatsRadarChart : MonoBehaviour
{
    [SerializeField] private Material radarMaterial;
    [SerializeField] private Texture2D radarTexture2D;

    private Stats stats;
    private CanvasRenderer radarMeshCanvasRenderer;

    private void Awake()
    {
        radarMeshCanvasRenderer = transform.Find("radarMesh").GetComponent<CanvasRenderer>();
    }

    public void SetStats(Stats stats)
    {
        this.stats = stats;
        stats.OnStatsChanged += Stats_OnStatsChanged;
        UpdateStatsVisual();
    }

    private void Stats_OnStatsChanged(object sender, System.EventArgs e)
    {
        UpdateStatsVisual();
    }

    private void UpdateStatsVisual()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[6];
        Vector2[] uv = new Vector2[6];
        int[] triangles = new int[3 * 5];

        float angleIncrement = 360f / 5;
        float radarChartSize = 1.20156f;

        Vector3 hpsVertex = Quaternion.Euler(0, 0, -angleIncrement * 0) * Vector3.up * radarChartSize * stats.GetStatAmountNormalized(Stats.Type.Hps);
        int hpsVertexIndex = 1;
        Vector3 surviVertex = Quaternion.Euler(0, 0, -angleIncrement * 1) * Vector3.up * radarChartSize * stats.GetStatAmountNormalized(Stats.Type.Survi);
        int surviVertexIndex = 2;
        Vector3 stabilityVertex = Quaternion.Euler(0, 0, -angleIncrement * 2) * Vector3.up * radarChartSize * stats.GetStatAmountNormalized(Stats.Type.Stability);
        int stabilityVertexIndex = 3;
        Vector3 rangeVertex = Quaternion.Euler(0, 0, -angleIncrement * 3) * Vector3.up * radarChartSize * stats.GetStatAmountNormalized(Stats.Type.Range);
        int rangeVertexIndex = 4;
        Vector3 dpsVertex = Quaternion.Euler(0, 0, -angleIncrement * 4) * Vector3.up * radarChartSize * stats.GetStatAmountNormalized(Stats.Type.Dps);
        int dpsVertexIndex = 5;

        vertices[0] = Vector3.zero;
        vertices[hpsVertexIndex] = hpsVertex;
        vertices[surviVertexIndex] = surviVertex;
        vertices[stabilityVertexIndex] = stabilityVertex;
        vertices[rangeVertexIndex] = rangeVertex;
        vertices[dpsVertexIndex] = dpsVertex;

        uv[0] = Vector2.zero;
        uv[hpsVertexIndex] = Vector2.one;
        uv[surviVertexIndex] = Vector2.one;
        uv[stabilityVertexIndex] = Vector2.one;
        uv[rangeVertexIndex] = Vector2.one;
        uv[dpsVertexIndex] = Vector2.one;

        triangles[0] = 0;
        triangles[1] = hpsVertexIndex;
        triangles[2] = surviVertexIndex;
        
        triangles[3] = 0;
        triangles[4] = surviVertexIndex;
        triangles[5] = stabilityVertexIndex;

        triangles[6] = 0;
        triangles[7] = stabilityVertexIndex;
        triangles[8] = rangeVertexIndex;

        triangles[9] = 0;
        triangles[10] = rangeVertexIndex;
        triangles[11] = dpsVertexIndex;

        triangles[12] = 0;
        triangles[13] = dpsVertexIndex;
        triangles[14] = hpsVertexIndex;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        radarMeshCanvasRenderer.SetMesh(mesh);
        radarMeshCanvasRenderer.SetMaterial(radarMaterial, radarTexture2D);
    }
}
