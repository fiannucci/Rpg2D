﻿using UnityEngine;
using System.Collections;

public static class DrawingUtilities
{
    public static void DrawQuad(Material aMaterial, Color aColor, float aAlpha)
    {
        aColor.a = aAlpha;
        aMaterial.SetPass(0);
        GL.PushMatrix();
        GL.LoadOrtho();
        GL.Begin(GL.QUADS);
        GL.Color(aColor);
        GL.Vertex3(0, 0, -1);
        GL.Vertex3(0, 1, -1);
        GL.Vertex3(1, 1, -1);
        GL.Vertex3(1, 0, -1); //L'HO AGGIUNTO IO
        GL.End();
        GL.PopMatrix();
    }
}
