using System.Collections;
using GeoAPI.Geometries;

namespace GisSharpBlog.NetTopologySuite.Operation.Distance
{
    /// <summary>
    /// A ConnectedElementPointFilter extracts a single point
    /// from each connected element in a Geometry
    /// (e.g. a polygon, linestring or point)
    /// and returns them in a list. The elements of the list are 
    /// <c>com.vividsolutions.jts.operation.distance.GeometryLocation</c>s.
    /// </summary>
    public class ConnectedElementLocationFilter : IGeometryFilter
    {
        /// <summary>
        /// Returns a list containing a point from each Polygon, LineString, and Point
        /// found inside the specified point. Thus, if the specified point is
        /// not a GeometryCollection, an empty list will be returned. The elements of the list 
        /// are <c>com.vividsolutions.jts.operation.distance.GeometryLocation</c>s.
        /// </summary>
        public static IList GetLocations(IGeometry geom)
        {
            IList locations = new ArrayList();
            geom.Apply(new ConnectedElementLocationFilter(locations));
            return locations;
        }

        private IList locations = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="locations"></param>
        ConnectedElementLocationFilter(IList locations)
        {
            this.locations = locations;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="geom"></param>
        public void Filter(IGeometry geom)
        {
            if (geom is IPoint || geom is ILineString || geom is IPolygon)
                locations.Add(new GeometryLocation(geom, 0, geom.Coordinate));
        }
    }
}
