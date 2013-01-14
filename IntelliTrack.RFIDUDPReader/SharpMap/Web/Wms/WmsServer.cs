// Copyright 2005, 2006 - Morten Nielsen (www.iter.dk)
//
// This file is part of SharpMap.
// SharpMap is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// SharpMap is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with SharpMap; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA 

using System;
using System.Collections.Generic;
using System.Text;

namespace SharpMap.Web.Wms
{
	/// <summary>
	/// This is a helper class designed to make it easy to create a WMS Service
	/// </summary>
	public class WmsServer
	{

		/// <summary>
		/// Generates a WMS 1.3.0 compliant response based on a <see cref="SharpMap.Map"/> and the current HttpRequest.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The Web Map Server implementation in SharpMap requires v1.3.0 compatible clients,
		/// and support the basic operations "GetCapabilities" and "GetMap"
		/// as required by the WMS v1.3.0 specification. SharpMap does not support the optional
		/// GetFeatureInfo operation for querying.
		/// </para>
		/// <example>
		/// Creating a WMS server in ASP.NET is very simple using the classes in the SharpMap.Web.Wms namespace.
		/// <code lang="C#">
		/// void page_load(object o, EventArgs e)
		/// {
		///		//Get the path of this page
		///		string url = (Request.Url.Query.Length>0?Request.Url.AbsoluteUri.Replace(Request.Url.Query,""):Request.Url.AbsoluteUri);
		///		SharpMap.Web.Wms.Capabilities.WmsServiceDescription description =
		///			new SharpMap.Web.Wms.Capabilities.WmsServiceDescription("Acme Corp. Map Server", url);
		///		
		///		// The following service descriptions below are not strictly required by the WMS specification.
		///		
		///		// Narrative description and keywords providing additional information 
		///		description.Abstract = "Map Server maintained by Acme Corporation. Contact: webmaster@wmt.acme.com. High-quality maps showing roadrunner nests and possible ambush locations.";
		///		description.Keywords.Add("bird");
		///		description.Keywords.Add("roadrunner");
		///		description.Keywords.Add("ambush");
		///		
		///		//Contact information 
		///		description.ContactInformation.PersonPrimary.Person = "John Doe";
		///		description.ContactInformation.PersonPrimary.Organisation = "Acme Inc";
		///		description.ContactInformation.Address.AddressType = "postal";
		///		description.ContactInformation.Address.Country = "Neverland";
		///		description.ContactInformation.VoiceTelephone = "1-800-WE DO MAPS";
		///		//Impose WMS constraints
		///		description.MaxWidth = 1000; //Set image request size width
		///		description.MaxHeight = 500; //Set image request size height
		///		
		///		//Call method that sets up the map
		///		//We just add a dummy-size, since the wms requests will set the image-size
		///		SharpMap.Map myMap = MapHelper.InitializeMap(new System.Drawing.Size(1,1));
		///		
		///		//Parse the request and create a response
		///		SharpMap.Web.Wms.WmsServer.ParseQueryString(myMap,description);
		/// }
		/// </code>
		/// </example>
		/// </remarks>
		/// <param name="map">Map to serve on WMS</param>
		/// <param name="description">Description of map service</param>
		public static void ParseQueryString(SharpMap.Map map, Capabilities.WmsServiceDescription description)
		{
			if (map == null)
				throw (new ArgumentException("Map for WMS is null"));
			if (map.Layers.Count == 0)
				throw (new ArgumentException("Map doesn't contain any layers for WMS service"));

			if (System.Web.HttpContext.Current == null)
				throw (new ApplicationException("An attempt was made to access the WMS server outside a valid HttpContext"));

			System.Web.HttpContext context = System.Web.HttpContext.Current;

			//IgnoreCase value should be set according to the VERSION parameter
			//v1.3.0 is case sensitive, but since it causes a lot of problems with several WMS clients, we ignore casing anyway.
			bool ignorecase = true; 

			//Check for required parameters
			//Request parameter is mandatory
			if (context.Request.Params["REQUEST"] == null)
				{ WmsException.ThrowWmsException("Required parameter REQUEST not specified"); return; }
			//Check if version is supported
			if (context.Request.Params["VERSION"] != null)
			{
				if (String.Compare(context.Request.Params["VERSION"], "1.3.0", ignorecase) != 0)
					{ WmsException.ThrowWmsException("Only version 1.3.0 supported"); return; }
			}
			else //Version is mandatory if REQUEST!=GetCapabilities. Check if this is a capabilities request, since VERSION is null
			{
				if (String.Compare(context.Request.Params["REQUEST"], "GetCapabilities", ignorecase) != 0)
					{ WmsException.ThrowWmsException("VERSION parameter not supplied"); return; }
			}

			//If Capabilities was requested
			if (String.Compare(context.Request.Params["REQUEST"], "GetCapabilities", ignorecase) == 0)
			{
        GetCapabilities(map, ref description, context);
			}
			else if (String.Compare(context.Request.Params["REQUEST"], "GetMap", ignorecase) == 0) //Map requested
			{
        GetMap(map, ref description, context, ignorecase);
			}
      else if (String.Compare(context.Request.Params["REQUEST"], "getfeatureinfo", ignorecase) == 0) //Map requested
      {
        GetFeatureInfo(map, context);
      }
      else
        WmsException.ThrowWmsException(WmsException.WmsExceptionCode.OperationNotSupported, "Invalid request");
		}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="map"></param>
    /// <param name="context"></param>
    private static void GetFeatureInfo(SharpMap.Map map, System.Web.HttpContext context)
    {
      //Check for required parameters
      if (context.Request.Params["LAYERS"] == null)
      { WmsException.ThrowWmsException("Required parameter LAYERS not specified"); return; }
      if (context.Request.Params["QUERY_LAYERS"] == null)
      { WmsException.ThrowWmsException("Required parameter QUERY_LAYERS not specified"); return; }
      if (context.Request.Params["STYLES"] == null)
      { WmsException.ThrowWmsException("Required parameter STYLES not specified"); return; }
      if (context.Request.Params["SRS"] == null)
      { WmsException.ThrowWmsException("Required parameter CRS not specified"); return; }
      /*else if (context.Request.Params["SRS"] != "EPSG:" + map.Layers[0].SRID.ToString())
      { WmsException.ThrowWmsException(WmsException.WmsExceptionCode.InvalidCRS, "CRS not supported"); return; }*/
      if (context.Request.Params["BBOX"] == null)
      { WmsException.ThrowWmsException(WmsException.WmsExceptionCode.InvalidDimensionValue, "Required parameter BBOX not specified"); return; }
      if (context.Request.Params["WIDTH"] == null)
      { WmsException.ThrowWmsException(WmsException.WmsExceptionCode.InvalidDimensionValue, "Required parameter WIDTH not specified"); return; }
      if (context.Request.Params["HEIGHT"] == null)
      { WmsException.ThrowWmsException(WmsException.WmsExceptionCode.InvalidDimensionValue, "Required parameter HEIGHT not specified"); return; }
      if (context.Request.Params["INFO_FORMAT"] == null)
      { WmsException.ThrowWmsException("Required parameter INFO_FORMAT not specified"); return; }
      if (context.Request.Params["FEATURE_COUNT"] == null)
      { WmsException.ThrowWmsException("Required parameter FEATURE_COUNT not specified"); return; }
      if (context.Request.Params["X"] == null)
      { WmsException.ThrowWmsException("Required parameter X not specified"); return; }
      if (context.Request.Params["Y"] == null)
      { WmsException.ThrowWmsException("Required parameter Y not specified"); return; }


      //Set layers on/off
      if (context.Request.Params["LAYERS"] != "") //If LAYERS is empty, use all layers
      {
        string[] layers = context.Request.Params["LAYERS"].Split(new char[] { ',' });
        foreach (SharpMap.Layers.ILayer layer in map.Layers)
          layer.Enabled = false;

        bool layerNotFound = true;
        string layerNotFoundName = "";
        foreach (string layer in layers)
        {
          SharpMap.Layers.ILayer lay = map.Layers.Find(delegate(SharpMap.Layers.ILayer findlay) { return findlay.LayerName == layer; });
          if (lay == null)
          {
            WmsException.ThrowWmsException(WmsException.WmsExceptionCode.LayerNotDefined, "Unknown layer '" + layer + "'");
            return;
          }
          else
          {
            if (lay is SharpMap.Layers.VectorLayer)
            {
              lay.Enabled = true;
              layerNotFound = false;
            }
            else
            {
              lay.Enabled = false;
              layerNotFoundName = layer;
            }
          }
        }
        if (layerNotFound)
          WmsException.ThrowWmsException(WmsException.WmsExceptionCode.LayerNotQueryable, "No se puede consultar la capa '" + layerNotFoundName + "'");
      }
      else
      {
        foreach (SharpMap.Layers.ILayer lay in map.Layers)
        {
          if (lay is SharpMap.Layers.VectorLayer)
            lay.Enabled = true;
          else
            lay.Enabled = false;
        }
      }

      
      SharpMap.Geometries.BoundingBox bbox = ParseBBOX(context.Request.Params["bbox"]);
      if (bbox == null)
      {
        WmsException.ThrowWmsException("Invalid parameter BBOX");
        return;
      }

      int w = 0;
      int h = 0;
      if (!int.TryParse(context.Request.Params["WIDTH"], out w))
        WmsException.ThrowWmsException(WmsException.WmsExceptionCode.InvalidFormat, "Invalid WIDTH");
      if (!int.TryParse(context.Request.Params["HEIGHT"], out h))
        WmsException.ThrowWmsException(WmsException.WmsExceptionCode.InvalidFormat, "Invalid HEIGHT");

      map.Size = new System.Drawing.Size(w, h);
      map.ZoomToBox(bbox);

      float x = 0;
      float y = 0;
      if (!float.TryParse(context.Request.Params["X"], System.Globalization.NumberStyles.Float, SharpMap.Map.numberFormat_EnUS, out x))
        WmsException.ThrowWmsException(WmsException.WmsExceptionCode.InvalidPoint, "Invalid x coordinate");
      if (!float.TryParse(context.Request.Params["Y"], System.Globalization.NumberStyles.Float, SharpMap.Map.numberFormat_EnUS, out y))
        WmsException.ThrowWmsException(WmsException.WmsExceptionCode.InvalidPoint, "Invalid y coordinate");

      System.Drawing.PointF ptf = new System.Drawing.PointF(x, y);
      SharpMap.Geometries.Point pt = map.ImageToWorld(ptf);

      // Para cada layer activo realizo la consulta:
      SharpMap.Data.FeatureDataRow fdrow = null;
      foreach (SharpMap.Layers.ILayer layer in map.Layers)
      {
        if (layer.Enabled)
        {
          SharpMap.Layers.VectorLayer vlayer = layer as SharpMap.Layers.VectorLayer;
          vlayer.DataSource.Open();
          double distance = 0.0;
          fdrow = vlayer.FindGeoNearPoint(pt, 0.1, ref distance);
          vlayer.DataSource.Close();
        }
      }

      string response = "";
      if (OnProcessWMSGetFeatureInfoResponse != null)
      {
        response = OnProcessWMSGetFeatureInfoResponse(fdrow, context.Request.Params["INFO_FORMAT"], context.Request.Params["LAYERS"]);
      }

      context.Response.Clear();
      context.Response.ContentType = context.Request.Params["INFO_FORMAT"];
      context.Response.Write(response);
      context.Response.End();
    }

    public delegate string ProcessWMSGetFeatureInfoResponse(SharpMap.Data.FeatureDataRow featureDataRow, string Response_Format, string layers);
    public static event ProcessWMSGetFeatureInfoResponse OnProcessWMSGetFeatureInfoResponse;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="map"></param>
    /// <param name="description"></param>
    /// <param name="context"></param>
    private static void GetCapabilities(SharpMap.Map map, ref Capabilities.WmsServiceDescription description, System.Web.HttpContext context)
    {
      //Service parameter is mandatory for GetCapabilities request
      if (context.Request.Params["SERVICE"] == null)
      { WmsException.ThrowWmsException("Required parameter SERVICE not specified"); return; }

      if (String.Compare(context.Request.Params["SERVICE"], "WMS") != 0)
        WmsException.ThrowWmsException("Invalid service for GetCapabilities Request. Service parameter must be 'WMS'");


      System.Xml.XmlDocument capabilities = Wms.Capabilities.GetCapabilities(map, description);
      context.Response.Clear();
      context.Response.ContentType = "text/xml";
      System.Xml.XmlWriter writer = System.Xml.XmlWriter.Create(context.Response.OutputStream);
      capabilities.WriteTo(writer);
      writer.Close();
      context.Response.End();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="map"></param>
    /// <param name="description"></param>
    /// <param name="context"></param>
    /// <param name="ignorecase"></param>
    private static void GetMap(SharpMap.Map map, ref Capabilities.WmsServiceDescription description, System.Web.HttpContext context, bool ignorecase)
    {
      string QueryString = context.Request.QueryString.ToString();
      System.Diagnostics.Debug.WriteLine(context.Request.QueryString);
      if (RequestIsCached(QueryString))
      {
        SendCachedData(context);
      }
      else
      {
        bool MapIsStatic = true;
        //Check for required parameters
        if (context.Request.Params["LAYERS"] == null)
        { WmsException.ThrowWmsException("Required parameter LAYERS not specified"); return; }
        if (context.Request.Params["STYLES"] == null)
        { WmsException.ThrowWmsException("Required parameter STYLES not specified"); return; }
        if (context.Request.Params["CRS"] == null)
        { WmsException.ThrowWmsException("Required parameter CRS not specified"); return; }
        else if (context.Request.Params["CRS"] != "EPSG:" + map.Layers[0].SRID.ToString())
        { WmsException.ThrowWmsException(WmsException.WmsExceptionCode.InvalidCRS, "CRS not supported"); return; }
        if (context.Request.Params["BBOX"] == null)
        { WmsException.ThrowWmsException(WmsException.WmsExceptionCode.InvalidDimensionValue, "Required parameter BBOX not specified"); return; }
        if (context.Request.Params["WIDTH"] == null)
        { WmsException.ThrowWmsException(WmsException.WmsExceptionCode.InvalidDimensionValue, "Required parameter WIDTH not specified"); return; }
        if (context.Request.Params["HEIGHT"] == null)
        { WmsException.ThrowWmsException(WmsException.WmsExceptionCode.InvalidDimensionValue, "Required parameter HEIGHT not specified"); return; }
        if (context.Request.Params["FORMAT"] == null)
        { WmsException.ThrowWmsException("Required parameter FORMAT not specified"); return; }

        //Set background color of map
        if (String.Compare(context.Request.Params["TRANSPARENT"], "TRUE", ignorecase) == 0)
          map.BackColor = System.Drawing.Color.Transparent;
        else if (context.Request.Params["BGCOLOR"] != null)
        {
          try { map.BackColor = System.Drawing.ColorTranslator.FromHtml(context.Request.Params["BGCOLOR"]); }
          catch { WmsException.ThrowWmsException("Invalid parameter BGCOLOR"); return; };
        }
        else
          map.BackColor = System.Drawing.Color.White;

        //Get the image format requested
        System.Drawing.Imaging.ImageCodecInfo imageEncoder = GetEncoderInfo(context.Request.Params["FORMAT"]);
        if (imageEncoder == null)
        {
          WmsException.ThrowWmsException("Invalid MimeType specified in FORMAT parameter");
          return;
        }

        //Parse map size
        int width = 0;
        int height = 0;
        if (!int.TryParse(context.Request.Params["WIDTH"], out width))
        {
          WmsException.ThrowWmsException(WmsException.WmsExceptionCode.InvalidDimensionValue, "Invalid parameter WIDTH");
          return;
        }
        else if (description.MaxWidth > 0 && width > description.MaxWidth)
        {
          WmsException.ThrowWmsException(WmsException.WmsExceptionCode.OperationNotSupported, "Parameter WIDTH too large");
          return;
        }
        if (!int.TryParse(context.Request.Params["HEIGHT"], out height))
        {
          WmsException.ThrowWmsException(WmsException.WmsExceptionCode.InvalidDimensionValue, "Invalid parameter HEIGHT");
          return;
        }
        else if (description.MaxHeight > 0 && height > description.MaxHeight)
        {
          WmsException.ThrowWmsException(WmsException.WmsExceptionCode.OperationNotSupported, "Parameter HEIGHT too large");
          return;
        }
        map.Size = new System.Drawing.Size(width, height);

        SharpMap.Geometries.BoundingBox bbox = ParseBBOX(context.Request.Params["bbox"]);
        if (bbox == null)
        {
          WmsException.ThrowWmsException("Invalid parameter BBOX");
          return;
        }
        map.PixelAspectRatio = ((double)width / (double)height) / (bbox.Width / bbox.Height);
        map.Center = bbox.GetCentroid();
        map.Zoom = bbox.Width;

        //Set layers on/off
        if (context.Request.Params["LAYERS"] != "") //If LAYERS is empty, use default layer on/off settings
        {
          string[] layers = context.Request.Params["LAYERS"].Split(new char[] { ',' });
          if (description.LayerLimit > 0)
          {
            if (layers.Length == 0 && map.Layers.Count > description.LayerLimit ||
              layers.Length > description.LayerLimit)
            {
              WmsException.ThrowWmsException(WmsException.WmsExceptionCode.OperationNotSupported, "Too many layers requested");
              return;
            }
          }
          foreach (SharpMap.Layers.ILayer layer in map.Layers)
            layer.Enabled = false;
          foreach (string layer in layers)
          {
            SharpMap.Layers.ILayer lay = map.Layers.Find(delegate(SharpMap.Layers.ILayer findlay) { return findlay.LayerName == layer; });
            if (lay == null)
            {
              WmsException.ThrowWmsException(WmsException.WmsExceptionCode.LayerNotDefined, "Unknown layer '" + layer + "'");
              return;
            }
            else
            {
              lay.Enabled = true;
              if (lay is SharpMap.Layers.VectorLayer)
              {
                if (!((lay as SharpMap.Layers.VectorLayer).DataSource is SharpMap.Data.Providers.ShapeFile))
                {
                  MapIsStatic = false;
                }
              }
            }
          }
        }
        //Render map
        System.Drawing.Image img = map.GetMap();

        //Png can't stream directy. Going through a memorystream instead
        System.IO.MemoryStream MS = new System.IO.MemoryStream();
        img.Save(MS, imageEncoder, null);
        img.Dispose();
        byte[] buffer = MS.ToArray();
        context.Response.Clear();
        context.Response.ContentType = imageEncoder.MimeType;
        context.Response.OutputStream.Write(buffer, 0, buffer.Length);
        if (MapIsStatic)
          SaveCachedData(QueryString, imageEncoder.MimeType, buffer);

        context.Response.End();

      }
    }
 

		/// <summary>
		/// Used for setting up output format of image file
		/// </summary>
		private static System.Drawing.Imaging.ImageCodecInfo GetEncoderInfo(String mimeType)
		{
			foreach(System.Drawing.Imaging.ImageCodecInfo encoder in System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders())
				if (encoder.MimeType == mimeType)
					return encoder;
			return null;
		}

		/// <summary>
		/// Parses a boundingbox string to a boundingbox geometry from the format minx,miny,maxx,maxy. Returns null if the format is invalid
		/// </summary>
		/// <param name="strBBOX">string representation of a boundingbox</param>
		/// <returns>Boundingbox or null if invalid parameter</returns>
		private static SharpMap.Geometries.BoundingBox ParseBBOX(string strBBOX)
		{
			string[] strVals = strBBOX.Split(new char[] {','});
			if(strVals.Length!=4)
				return null;
			double minx = 0; double miny = 0;
			double maxx = 0; double maxy = 0;
			if (!double.TryParse(strVals[0], System.Globalization.NumberStyles.Float, SharpMap.Map.numberFormat_EnUS, out minx))
				return null;
			if (!double.TryParse(strVals[2], System.Globalization.NumberStyles.Float, SharpMap.Map.numberFormat_EnUS, out maxx))
				return null;
			if (maxx < minx)
				return null;

			if (!double.TryParse(strVals[1], System.Globalization.NumberStyles.Float, SharpMap.Map.numberFormat_EnUS, out miny))
				return null;
			if (!double.TryParse(strVals[3], System.Globalization.NumberStyles.Float, SharpMap.Map.numberFormat_EnUS, out maxy))
				return null;
			if (maxy < miny)
				return null;

			return new SharpMap.Geometries.BoundingBox(minx, miny, maxx, maxy);
		}


    private static void SaveCachedData(string QueryString, string MimeType, byte[] buffer)
    {
      if (SqlConnectionStringCache != "")
      {
        System.Data.SqlClient.SqlConnection connection = new
          System.Data.SqlClient.SqlConnection(SqlConnectionStringCache);
        try
        {
          connection.Open();
          System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("insert into MapCache "
            + "(Picture, MimeType, QueryString) values (@picture, @MimeType, @QueryString)", connection);
          cmd.Parameters.AddWithValue("@picture", buffer);
          cmd.Parameters.AddWithValue("@MimeType", MimeType);
          cmd.Parameters.AddWithValue("@QueryString", QueryString);
          cmd.ExecuteNonQuery();
        }
        finally
        {
          connection.Close();
        }
      }
    }

    private static void SendCachedData(System.Web.HttpContext context)
    {
      if (SqlConnectionStringCache != "")
      {
        string QueryString = context.Request.QueryString.ToString();
        System.IO.MemoryStream stream = new System.IO.MemoryStream();
        System.Data.SqlClient.SqlConnection connection = new
          System.Data.SqlClient.SqlConnection(SqlConnectionStringCache);
        try
        {
          connection.Open();
          System.Data.SqlClient.SqlCommand command = new
            System.Data.SqlClient.SqlCommand("select Picture from MapCache WHERE QueryString = @QueryString", connection);
          System.Data.SqlClient.SqlParameter prm = new System.Data.SqlClient.SqlParameter("@QueryString", QueryString);
          command.Parameters.Add(prm);
          byte[] image = (byte[])command.ExecuteScalar();
          stream.Write(image, 0, image.Length);
          //System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(stream);
          command = new System.Data.SqlClient.SqlCommand("select MimeType from MapCache", connection);
          string MimeType = (string)command.ExecuteScalar();
          context.Response.ContentType = MimeType;
          //bitmap.Save(context.Response.OutputStream, ImageFormat.Gif);
          context.Response.OutputStream.Write(image, 0, image.Length);
        }
        catch (Exception e)
        {
          System.Diagnostics.Debug.Write(e);
          System.Diagnostics.Debug.Flush();
        }
        finally
        {
          connection.Close();
          stream.Close();
        }
      }
    }

    private static bool RequestIsCached(string Request)
    {
      bool exists = false;
      if (SqlConnectionStringCache != "")
      {
        System.Data.SqlClient.SqlConnection connection = new
          System.Data.SqlClient.SqlConnection(SqlConnectionStringCache);
        try
        {
          connection.Open();
          System.Data.SqlClient.SqlCommand command = new
            System.Data.SqlClient.SqlCommand("select count(*) from MapCache WHERE QueryString = @QueryString", connection);
          System.Data.SqlClient.SqlParameter prm = new System.Data.SqlClient.SqlParameter("@QueryString", Request);
          command.Parameters.Add(prm);
          exists = (bool)((int)command.ExecuteScalar() != 0);
        }
        catch (Exception ex)
        {
          string s = ex.Message;
        }
        finally
        {
          connection.Close();
        }
      }
      return exists;
    }

    private static string _SqlConnectionStringCache = "";//@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\InteliTrack\DB\Test.mdf;Integrated Security=True;Connect Timeout=30;User Instance=True"

    /// <summary>
    /// Configura el string de conexion para almacenar el cache de imagenes.
    /// </summary>
    public static string SqlConnectionStringCache
    {
      get
      {
        return _SqlConnectionStringCache;
      }
      set
      {
        _SqlConnectionStringCache = value.Trim();
      }
    }
        

	}
}
