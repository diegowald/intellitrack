using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.InteropServices;
using SharpMap;
using SharpMap.Geometries;
using System.Drawing;

namespace SharpMap.Geometries
{
  [Serializable]
  public class GISCircle: SharpMap.Geometries.Point, IComparable<GISCircle>
	{
    private double _Radius;
    private double _RadiusSexagecimal;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="radius"></param>
    public GISCircle(double x, double y, double radius):base(x, y)
    {
      Radius=radius;
    }

    /// <summary>
    /// 
    /// </summary>
    public GISCircle():this(0,0,0)
    {}

    public static GISCircle FromDMS(double longDegrees, double longMinutes, double longSeconds,
      double latDegrees, double latMinutes, double latSeconds, double radius)
    {
      return new GISCircle(longDegrees + longMinutes / 60 + longSeconds / 3600,
        latDegrees + latMinutes / 60 + latSeconds / 3600, radius);
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
		public GISCircle AsGISCircle()
		{
      return new GISCircle(X, Y, _Radius);
		}

    /// <summary>
    /// Gets or sets the Y coordinate of the point
    /// </summary>
    public double Radius
    {
      get
      {
        if (!IsEmpty())
          return _Radius;
        else throw new ApplicationException("GISCircle is empty");
      }
      set 
      { 
        _Radius = value;
        _RadiusSexagecimal = _Radius / 60 / 1845; 
        // 1845 : cantidad de metros por minuto en el ecuador... se puede mejorar este calculo
        base.SetIsEmpty = false; 
      }
    }

    public double RadiusSexagecimal
    {
      get
      {
        return _RadiusSexagecimal;
      }
    }

		/// <summary>
		/// Returns part of coordinate. Index 0 = X, Index 1 = Y, Index 2 = Radius
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public virtual double this[uint index]
		{
			get
			{
        if (IsEmpty())
          throw new ApplicationException("GISCircle is empty");
        else if (index == 0)
          return this.X;
        else if (index == 1)
          return this.Y;
        else if (index == 2)
          return this.Radius;
        else
          throw (new System.Exception("GISCircle index out of bounds"));
			}
			set
			{
        if (index == 0)
          this.X = value;
        else if (index == 1)
          this.Y = value;
        else if (index == 2)
          this.Radius = value;
        else
          throw (new System.Exception("GISCircle index out of bounds"));
				SetIsEmpty = false;
			}
		}
		/// <summary>
		/// Returns the number of ordinates for this point
		/// </summary>
		public virtual int NumOrdinates
		{
			get { return 3; }
		}
		/// <summary>
		/// Transforms the point to image coordinates, based on the map
		/// </summary>
		/// <param name="map">Map to base coordinates on</param>
		/// <returns>point in image coordinates</returns>
		public System.Drawing.PointF TransformToImage(Map map)
		{
			return SharpMap.Utilities.Transform.WorldtoMap(this, map);
		}

		#region "Inherited methods from abstract class Geometry"

    public virtual bool Equals(GISCircle c)
    {
      return c != null && c.X == X && c.Y == Y && c.Radius == _Radius && this.IsEmpty() == c.IsEmpty();
		}

		/// <summary>
		/// Serves as a hash function for a particular type. <see cref="GetHashCode"/> is suitable for use 
		/// in hashing algorithms and data structures like a hash table.
		/// </summary>
		/// <returns>A hash code for the current <see cref="GetHashCode"/>.</returns>
		public override int GetHashCode()
		{
      return X.GetHashCode() ^ Y.GetHashCode() ^ Radius.GetHashCode() ^ IsEmpty().GetHashCode();
		}

		/// <summary>
		///  The inherent dimension of this Geometry object, which must be less than or equal to the coordinate dimension.
		/// </summary>
		public override int Dimension
		{
			get { return 2; }
		}

		/// <summary>
		/// The boundary of a point is the empty set.
		/// </summary>
		/// <returns>null</returns>
		public override Geometry Boundary()
		{
      throw new NotImplementedException();
		}

		/// <summary>
		/// Returns the distance between this geometry instance and another geometry, as
		/// measured in the spatial reference system of this instance.
		/// </summary>
		/// <param name="geom"></param>
		/// <returns></returns>
		public override double Distance(Geometry geom)
		{
			if (geom.GetType() == typeof(SharpMap.Geometries.Point))
			{
        SharpMap.Geometries.Point p = geom as SharpMap.Geometries.Point;
				return Math.Sqrt(Math.Pow(this.X - p.X, 2) + Math.Pow(this.Y - p.Y, 2)) - _RadiusSexagecimal ;
			}
      else if (geom.GetType() == typeof(SharpMap.Geometries.GISCircle))
      {
        GISCircle c = geom as GISCircle;
        return Math.Sqrt(Math.Pow(this.X - c.X, 2) + Math.Pow(this.Y - c.Y, 2)) - _RadiusSexagecimal - c.RadiusSexagecimal;
      }
      else
        throw new Exception("The method or operation is not implemented for this geometry type.");
		}
		/// <summary>
		/// Returns the distance between this point and a <see cref="BoundingBox"/>
		/// </summary>
		/// <param name="box"></param>
		/// <returns></returns>
		public double Distance(BoundingBox box)
		{
			return box.Distance(this);
		}

		/// <summary>
		/// Returns a geometry that represents all points whose distance from this Geometry
		/// is less than or equal to distance. Calculations are in the Spatial Reference
		/// System of this Geometry.
		/// </summary>
		/// <param name="d">Buffer distance</param>
		/// <returns>Buffer around geometry</returns>
		public override Geometry Buffer(double d)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Geometry—Returns a geometry that represents the convex hull of this Geometry.
		/// </summary>
		/// <returns>The convex hull</returns>
		public override Geometry ConvexHull()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Returns a geometry that represents the point set intersection of this Geometry
		/// with anotherGeometry.
		/// </summary>
		/// <param name="geom">Geometry to intersect with</param>
		/// <returns>Returns a geometry that represents the point set intersection of this Geometry with anotherGeometry.</returns>
		public override Geometry Intersection(Geometry geom)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Returns a geometry that represents the point set union of this Geometry with anotherGeometry.
		/// </summary>
		/// <param name="geom">Geometry to union with</param>
		/// <returns>Unioned geometry</returns>
		public override Geometry Union(Geometry geom)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Returns a geometry that represents the point set difference of this Geometry with anotherGeometry.
		/// </summary>
		/// <param name="geom">Geometry to compare to</param>
		/// <returns>Geometry</returns>
		public override Geometry Difference(Geometry geom)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Returns a geometry that represents the point set symmetric difference of this Geometry with anotherGeometry.
		/// </summary>
		/// <param name="geom">Geometry to compare to</param>
		/// <returns>Geometry</returns>
		public override Geometry SymDifference(Geometry geom)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// The minimum bounding box for this Geometry.
		/// </summary>
		/// <returns></returns>
		public override BoundingBox GetBoundingBox()
		{
      return new BoundingBox(this.X - _RadiusSexagecimal, this.Y - _RadiusSexagecimal, this.X + _RadiusSexagecimal, this.Y + _RadiusSexagecimal);
		}
		/// <summary>
		/// Checks whether this point touches a <see cref="BoundingBox"/>
		/// </summary>
		/// <param name="box">box</param>
		/// <returns>true if they touch</returns>
		public bool Touches(BoundingBox box)
		{
			return box.Touches(this);
		}
		/// <summary>
		/// Checks whether this point touches another <see cref="Geometry"/>
		/// </summary>
		/// <param name="geom">Geometry</param>
		/// <returns>true if they touch</returns>
		public override bool Touches(Geometry geom)
		{
      return Touches(geom.GetBoundingBox());
/*			if (geom is Point && this.Equals(geom)) return true;
			throw new NotImplementedException("Touches not implemented for this feature type");*/
		}

		/// <summary>
		/// Returns true if this instance contains 'geom'
		/// </summary>
		/// <param name="geom">Geometry</param>
		/// <returns>True if geom is contained by this instance</returns>
		public override bool Contains(Geometry geom)
		{
//      System.Diagnostics.Debug.WriteLine(Distance(geom));
//      System.Diagnostics.Debug.WriteLine(RadiusSexagecimal);
      return (Distance(geom) <= RadiusSexagecimal);
		}

		#endregion

		/// <summary>
		/// This method must be overridden using 'public new [derived_data_type] Clone()'
		/// </summary>
		/// <returns>Clone</returns>
    public new GISCircle Clone()
    {
      return new GISCircle(this.X, this.Y, this.Radius);
    }

		#region IComparable<GISCircle> Members

		/// <summary>
		/// Comparator used for ordering point first by ascending X, then by ascending Y.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public virtual int CompareTo(GISCircle other)
		{
			if (this.X < other.X || this.X == other.X && this.Y < other.Y)
				return -1;
			else if (this.X > other.X || this.X == other.X && this.Y > other.Y)
				return 1;
			else// (this.X == other.X && this.Y == other.Y)
				return 0;
		}

		#endregion

	}
}
