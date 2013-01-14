using System;
using System.Collections.Generic;
using System.Text;

namespace SharpMap.Layers
{
  public class HtmlLabelLayer: SharpMap.Layers.LabelLayer
  {
    public HtmlLabelLayer(string layername)
      : base(layername)
		{
		}

    protected override SharpMap.Rendering.Label CreateLabel(SharpMap.Geometries.Geometry feature, string text, float rotation, SharpMap.Styles.LabelStyle style, Map map, System.Drawing.Graphics g)
    {
      //System.Drawing.SizeF size = g.MeasureString(text, style.Font);

      System.Drawing.PointF position = map.WorldToImage(feature.GetBoundingBox().GetCentroid());
      //position.X = position.X - size.Width * (short)style.HorizontalAlignment * 0.5f;
      //position.Y = position.Y - size.Height * (short)style.VerticalAlignment * 0.5f;
      if (position.X /*- size.Width*/ > map.Size.Width || position.X /*+ size.Width */< 0 ||
        position.Y /*- size.Height*/ > map.Size.Height || position.Y /*+ size.Height*/ < 0)
        return null;
      else
      {
        SharpMap.Rendering.Label lbl;

        if (!style.CollisionDetection)
          lbl = new SharpMap.Rendering.Label(text, position, rotation, this.Priority, null, style);
        else
        {
          //Collision detection is enabled so we need to measure the size of the string
          lbl = new SharpMap.Rendering.Label(text, position, rotation, this.Priority,
            new SharpMap.Rendering.LabelBox(position.X /*- size.Width * 0.5f*/ - style.CollisionBuffer.Width, position.Y + /*size.Height * 0.5f*/ + style.CollisionBuffer.Height,
            /*size.Width +*/ 2f * style.CollisionBuffer.Width, /*size.Height +*/ style.CollisionBuffer.Height * 2f), style);
        }
        if (feature.GetType() == typeof(SharpMap.Geometries.LineString))
        {
          SharpMap.Geometries.LineString line = feature as SharpMap.Geometries.LineString;
          if (line.Length / map.PixelSize > 40/*size.Width*/) //Only label feature if it is long enough
            CalculateLabelOnLinestring(line, ref lbl, map);
          else
            return null;
        }

        return lbl;
      }
    }

    /// <summary>
    /// Renders the layer
    /// </summary>
    /// <param name="g">Graphics object reference</param>
    /// <param name="map">Map which is rendered</param>
    public override void Render(System.Drawing.Graphics g, SharpMap.Map map)
    {
      if (this.Style.Enabled && this.Style.MaxVisible >= map.Zoom && this.Style.MinVisible < map.Zoom)
      {
        if (this.DataSource == null)
          throw (new ApplicationException("DataSource property not set on layer '" + this.LayerName + "'"));
        g.TextRenderingHint = this.TextRenderingHint;
        g.SmoothingMode = this.SmoothingMode;

        SharpMap.Geometries.BoundingBox envelope = map.Envelope; //View to render
        if (this.CoordinateTransformation != null)
          envelope = SharpMap.CoordinateSystems.Transformations.GeometryTransform.TransformBox(envelope, this.CoordinateTransformation.MathTransform.Inverse());

        SharpMap.Data.FeatureDataSet ds = new SharpMap.Data.FeatureDataSet();
        this.DataSource.Open();
        this.DataSource.ExecuteIntersectionQuery(envelope, ds);
        this.DataSource.Close();
        if (ds.Tables.Count == 0)
        {
          base.Render(g, map);
          return;
        }
        SharpMap.Data.FeatureDataTable features = (SharpMap.Data.FeatureDataTable)ds.Tables[0];

        //Initialize label collection
        List<SharpMap.Rendering.Label> labels = new List<SharpMap.Rendering.Label>();

        //List<System.Drawing.Rectangle> LabelBoxes; //Used for collision detection
        //Render labels
        for (int i = 0; i < features.Count; i++)
        {
          SharpMap.Data.FeatureDataRow feature = features[i];
          if (this.CoordinateTransformation != null)
            features[i].Geometry = SharpMap.CoordinateSystems.Transformations.GeometryTransform.TransformGeometry(features[i].Geometry, this.CoordinateTransformation.MathTransform);

          SharpMap.Styles.LabelStyle style = null;
          if (this.Theme != null) //If thematics is enabled, lets override the style
            style = this.Theme.GetStyle(feature) as SharpMap.Styles.LabelStyle;
          else
            style = this.Style;

          float rotation = 0;
          if (this.RotationColumn != null && this.RotationColumn != "")
            float.TryParse(feature[this.RotationColumn].ToString(), System.Globalization.NumberStyles.Any, SharpMap.Map.numberFormat_EnUS, out rotation);

          string text;
          if ( _getLabelMethod != null)
            text = _getLabelMethod(feature);
          else
            text = feature[this.LabelColumn].ToString();

          if (text != null && text != String.Empty)
          {
            if (feature.Geometry is SharpMap.Geometries.GeometryCollection)
            {
              if (this.MultipartGeometryBehaviour == MultipartGeometryBehaviourEnum.All)
              {
                foreach (SharpMap.Geometries.Geometry geom in (feature.Geometry as Geometries.GeometryCollection))
                {
                  SharpMap.Rendering.Label lbl = CreateLabel(geom, text, rotation, style, map, g);
                  if (lbl != null)
                    labels.Add(lbl);
                }
              }
              else if (this.MultipartGeometryBehaviour == MultipartGeometryBehaviourEnum.CommonCenter)
              {
                SharpMap.Rendering.Label lbl = CreateLabel(feature.Geometry, text, rotation, style, map, g);
                if (lbl != null)
                  labels.Add(lbl);
              }
              else if (this.MultipartGeometryBehaviour == MultipartGeometryBehaviourEnum.First)
              {
                if ((feature.Geometry as Geometries.GeometryCollection).Collection.Count > 0)
                {
                  SharpMap.Rendering.Label lbl = CreateLabel((feature.Geometry as Geometries.GeometryCollection).Collection[0], text, rotation, style, map, g);
                  if (lbl != null)
                    labels.Add(lbl);
                }
              }
              else if (this.MultipartGeometryBehaviour == MultipartGeometryBehaviourEnum.Largest)
              {
                Geometries.GeometryCollection coll = (feature.Geometry as Geometries.GeometryCollection);
                if (coll.NumGeometries > 0)
                {
                  double largestVal = 0;
                  int idxOfLargest = 0;
                  for (int j = 0; j < coll.NumGeometries; j++)
                  {
                    SharpMap.Geometries.Geometry geom = coll.Geometry(j);
                    if (geom is Geometries.LineString && ((Geometries.LineString)geom).Length > largestVal)
                    {
                      largestVal = ((Geometries.LineString)geom).Length;
                      idxOfLargest = j;
                    }
                    if (geom is Geometries.MultiLineString && ((Geometries.MultiLineString)geom).Length > largestVal)
                    {
                      largestVal = ((Geometries.LineString)geom).Length;
                      idxOfLargest = j;
                    }
                    if (geom is Geometries.Polygon && ((Geometries.Polygon)geom).Area > largestVal)
                    {
                      largestVal = ((Geometries.Polygon)geom).Area;
                      idxOfLargest = j;
                    }
                    if (geom is Geometries.MultiPolygon && ((Geometries.MultiPolygon)geom).Area > largestVal)
                    {
                      largestVal = ((Geometries.MultiPolygon)geom).Area;
                      idxOfLargest = j;
                    }
                  }

                  SharpMap.Rendering.Label lbl = CreateLabel(coll.Geometry(idxOfLargest), text, rotation, style, map, g);
                  if (lbl != null)
                    labels.Add(lbl);
                }
              }
            }
            else
            {
              SharpMap.Rendering.Label lbl = CreateLabel(feature.Geometry, text, rotation, style, map, g);
              if (lbl != null)
                labels.Add(lbl);
            }
          }
        }
        if (labels.Count > 0) //We have labels to render...
        {
          if (this.Style.CollisionDetection && this._LabelFilter != null)
            this._LabelFilter(labels);
          for (int i = 0; i < labels.Count; i++)
            SharpMap.Rendering.VectorRenderer.DrawHtmlLabel(g, labels[i].LabelPoint, labels[i].Style.Offset, labels[i].Text, map);
        }
        labels = null;
      }
      //base.Render(g, map);
    }
  }
}
