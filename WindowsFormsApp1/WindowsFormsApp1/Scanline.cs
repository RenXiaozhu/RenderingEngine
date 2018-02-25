using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace RenderingEngine
{
    class Scanline
    {
        private Device device;
        private Edge[] ET;  // Edge Table 边的分类表
        private Edge AEL; //活化边表
        private int height;
        private int width;
        private Color4 finalColor;
        private List<Triangle> angleList;
        public Color4 backgroundColor;
        public Color4 forwardColor;

        public Scanline(Device device)
        {
            this.device = device;
            this.width = device.width;
            this.height = device.height;
            backgroundColor = new Color4(255, 255, 255);
            forwardColor = new Color4(255, 0, 0);
            finalColor = new Color4(222, 128, 50);
            this.angleList = new List<Triangle>();

            this.ET = new Edge[this.height];
            for (int i = 0; i < this.height; i++)
            {
                this.ET[i] = new Edge();
            }
        }



        void InsertEdge(ref Edge root, Edge e)
        {
            Edge newEdge = (Edge)e.Clone();

        }



        public void StartScan(Scene scene)
        {
            // 模型坐标系转换
            CATransform.ModelTransformToWindow(scene);


            for (int i = 0; i < scene.mesh.triangles.Length; i+=1)
            {
                Vertex[] tmp = scene.mesh.TmpVertices;
                Triangle triangle = scene.mesh.triangles[i];
                TriangleModel model = new TriangleModel(tmp[triangle.a], tmp[triangle.b], tmp[triangle.c]);

                //this.device.PutPixel((int)vt.Position.x, (int)vt.Position.y, vt.Color);
               

                    //this.device.DrawMidPointLine(new Vector2(vt.Position.x, vt.Position.y), new Vector2(next.Position.x, next.Position.y), vt.Color);
                    //this.device.DrawDDALine(new Vector2(vt.Position.x, vt.Position.y), new Vector2(next.Position.x, next.Position.y), vt.Color);
                    this.device.DrawTriangles(model);
            }
        }

		// 需要屏幕坐标，vt是裁剪后的三角形， orivt是原始三角形
		public void ProcessScanLine(TriangleModel vt, TriangleModel orivt, Scene scene)
		{
			int yMin = this.height;
			int yMax = 0;

			Vertex[] vertices = vt.Vertices;

			for (int i = 0; i < vertices.Length; i += 1)
			{
				for (int j = i + 1; j < vertices.Length; j += 1)
				{
					Vector4 screen1 = vertices[i].ScreenSpacePosition;
					Vector4 screen2 = vertices[j].ScreenSpacePosition;
					int a = (int)screen1.y;
					int b = (int)screen2.y;
					int c = (int)screen1.x;
					int d = (int)screen2.x;

					if (a != (int)b)
					{
						if (a > yMax)
							yMax = a;
						if (b > yMax)
							yMax = b;
						if (a < yMin)
							yMin = a;
						if (b < yMin)
							yMin = b;
						if (yMax > this.height)
							yMax = this.height;
						if (yMin < 0)
							yMin = 0;

						int ymin = a > b ? b : a;
						if (ymin < 0)
							ymin = 0;

						int ymax = a > b ? a : b;
						if (ymax > this.height)
							ymax = this.height;

						float x = a > b ? d : c;
						float dx = (float)(c-d)*1.0f / (float)(a - b);

						Edge e = new Edge();
						e.ymax = ymax;
						e.deltax = dx;
						e.yvMin = a > b ? vertices[j] : vertices[i];
						e.yvMax = a > b ? vertices[i] : vertices[j];
						InsertEdge(ref ET[ymin].nextEdge, e);

					}
				}
			}
			AEL = new Edge();
			for (int i = yMin; i < yMax; i += 1)
			{
				while (ET[i].nextEdge != null)
				{
					InsertEdge(ref AEL.nextEdge, ET[i].nextEdge);
					ET[i].nextEdge = ET[i].nextEdge.nextEdge;
				}

				if (AEL.nextEdge == null) continue;
				//填充扫描线
				Edge a1 = (Edge)AEL.nextEdge.Clone();
				Edge a2 = (Edge)AEL.nextEdge.nextEdge.Clone();

				//双线性插值
				Vector4 screenA1V1 = a1.yvMin.ScreenSpacePosition;
				Vector4 screenA1V2 = a1.yvMax.ScreenSpacePosition;
				Vector4 screenA2V1 = a2.yvMin.ScreenSpacePosition;
				Vector4 screenA2V2 = a2.yvMax.ScreenSpacePosition;

				double r1 = (i - screenA1V1.y) / (screenA1V2.y - screenA1V1.y);
				double r2 = (i - screenA2V1.y) / (screenA2V2.y - screenA2V1.y);
				r1 = MathUtil.Clamp01(r1);
				r2 = MathUtil.Clamp01(r2);

				double z1 = MathUtil.Interp(screenA1V1.z, screenA1V2.z, r1);
				double z2 = MathUtil.Interp(screenA2V1.z, screenA2V2.z, r2);

				double nDotL1 = 0, nDotL2 = 0;

				if (scene.light.IsEnable)
				{
					double nDotA1V1 = scene.light.ComputeNDotL(a1.yvMin.Position, a1.yvMin.Normal);
					double nDotA1V2 = scene.light.ComputeNDotL(a1.yvMax.Position, a1.yvMax.Normal);
					double nDotA2V1 = scene.light.ComputeNDotL(a2.yvMin.Position, a2.yvMin.Normal);
					double nDotA2V2 = scene.light.ComputeNDotL(a2.yvMax.Position, a2.yvMax.Normal);
					nDotL1 = MathUtil.Interp(nDotA1V1, nDotA1V2, r1);
					nDotL2 = MathUtil.Interp(nDotA2V1, nDotA2V2, r2);
				}

				Color4 c1 = MathUtil.ColorInterp(a1.yvMin.Color, a1.yvMax.Color, r1);
				Color4 c2 = MathUtil.ColorInterp(a2.yvMin.Color, a2.yvMax.Color, r2);
				Color4 c3 = new Color4();
				if (scene.renderState == Scene.RenderState.TextureMapping)
				{
					orivt.PreCalculateWeight();
				}

				while (a1 != null && a2 != null)
				{
					for (int x = (int)AEL.nextEdge.x ; x < (int)AEL.nextEdge.nextEdge.x ; x += 1)
					{
						double r3 = MathUtil.Clamp01((x - a1.x) / (a2.x - a1.x));
						double z = MathUtil.Interp(z1, z2, r3);
						if (scene.renderState == Scene.RenderState.GouraduShading)
						{
							if (scene.light.IsEnable)
							{
								double nDotL = MathUtil.Interp(nDotL1, nDotL2, r3);
								c3 = MathUtil.ColorInterp(c1, c2, r3);
								finalColor = c3 * scene.light.GetDiffuseColor((float)nDotL) + c3 * DirectionLight.AmbientColor;
							}
							else
							{
								c3 = MathUtil.ColorInterp(c1, c2, r3);
								finalColor = c3;
							}

						}
						else if(scene.renderState ==Scene.RenderState.TextureMapping)
						{
							orivt.CalWeight(new Vector4(x, i, 0, 0));
							Vector4 uv = orivt.GetInterUV();
							finalColor = this.device.Text2D((float)uv.x, (float)uv.y, scene.mesh.texture);
						}
						this.device.DrawPoint(new Vector4(x, i, z, 0), finalColor);
					}

					if (a2.nextEdge != null && a2.nextEdge.nextEdge != null)
					{
						a1 = (Edge)a2.nextEdge.Clone();
						a2 = (Edge)a1.nextEdge.Clone();
					}
					else
					{
						break;
					}
				}
				// 删除 y=ymax-1的边
				Edge p = AEL;
				while (p.nextEdge != null)
				{
					if (p.nextEdge.ymax - 1 == i)
					{
						Edge pDelete = p.nextEdge;
					}
					else
					{
						p = p.nextEdge;
					}
				}
			}
		}

        public void addTriangle(Triangle triangle)
        {
            this.angleList.Add(triangle);
        }
    }
}
