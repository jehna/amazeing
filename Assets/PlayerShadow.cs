using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    Transform player;
    public LayerMask planeLayer;
    RenderTexture renderTexture;
    public Material paintMaterial;
    private Texture2D targetTexture;

    void Start()
    {
        // Initialize the RenderTexture
        renderTexture = new RenderTexture(1024 * 4, 1024 * 4, 0, RenderTextureFormat.ARGB32);
        targetTexture = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
        // Assign the RenderTexture to the plane's material
        GetComponent<Renderer>().material.mainTexture = renderTexture;
    }

    void Update()
    {
        RaycastHit hit;
        player = FindFirstObjectByType<Player>().transform;
        Ray ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(player.position));

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, planeLayer))
        {
            Vector2 uv = new Vector2(hit.textureCoord.x, 1 - hit.textureCoord.y);
            PaintTexture(uv);
        }
    }

    void PaintTexture(Vector2 uv)
    {
        // Paint the texture using the UV coordinates
        PaintAtUV(uv);
    }

    void PaintAtUV(Vector2 uv)
    {
        // Activate the RenderTexture
        RenderTexture.active = renderTexture;
        // Draw the circle at the UV position
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, renderTexture.width, renderTexture.height, 0);

        // Set the paint material
        paintMaterial.SetPass(0);

        // Draw a circle
        GL.Begin(GL.QUADS);
        float radius = 20.0f; // Adjust the radius as needed
        GL.TexCoord2(0, 0); GL.Vertex3(uv.x * renderTexture.width - radius, uv.y * renderTexture.height - radius, 0);
        GL.TexCoord2(1, 0); GL.Vertex3(uv.x * renderTexture.width + radius, uv.y * renderTexture.height - radius, 0);
        GL.TexCoord2(1, 1); GL.Vertex3(uv.x * renderTexture.width + radius, uv.y * renderTexture.height + radius, 0);
        GL.TexCoord2(0, 1); GL.Vertex3(uv.x * renderTexture.width - radius, uv.y * renderTexture.height + radius, 0);
        GL.End();

        GL.PopMatrix();

        // Apply the changes to the texture
        RenderTexture.active = null;
    }
}
