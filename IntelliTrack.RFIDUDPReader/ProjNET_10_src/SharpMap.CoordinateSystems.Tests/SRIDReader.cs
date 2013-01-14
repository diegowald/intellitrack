using System;
using System.Collections.Generic;
using System.Text;
using SharpMap.CoordinateSystems;

namespace ProjNet.UnitTests
{
    internal class SRIDReader
    {
        private const string filename = @"..\..\SRID.csv";

        public struct WKTstring {
            /// <summary>
            /// Well-known ID
            /// </summary>
            public int WKID;
            /// <summary>
            /// Well-known Text
            /// </summary>
            public string WKT;
        }

        /// <summary>
        /// Enumerates all SRID's in the SRID.csv file.
        /// </summary>
        /// <returns>Enumerator</returns>
        public static IEnumerable<WKTstring> GetSRIDs()
        {
            using (System.IO.StreamReader sr = System.IO.File.OpenText(filename))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    int split = line.IndexOf(';');
                    if (split > -1)
                    {
                        WKTstring wkt = new WKTstring();
                        wkt.WKID = int.Parse(line.Substring(0, split));
                        wkt.WKT = line.Substring(split + 1);
                        yield return wkt;
                    }
                }
                sr.Close();
            }
        }
        /// <summary>
        /// Gets a coordinate system from the SRID.csv file
        /// </summary>
        /// <param name="id">EPSG ID</param>
        /// <returns>Coordinate system, or null if SRID was not found.</returns>
        public static ICoordinateSystem GetCSbyID(int id)
        {
   			CoordinateSystemFactory fac = new CoordinateSystemFactory();
            foreach (SRIDReader.WKTstring wkt in SRIDReader.GetSRIDs())
            {
                if (wkt.WKID == id)
                {
                    return SharpMap.Converters.WellKnownText.CoordinateSystemWktReader.Parse(wkt.WKT) as ICoordinateSystem;
                }
            }
            return null;
        }
    }
}
