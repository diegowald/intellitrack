using System;
using System.Collections.Generic;
using System.Text;

namespace IntelliTrack.Client.Application.HTMLFormatting
{
    public static class HtmlStyle
    {
        #region Properties

        /// <summary>
        /// Gets the stylesheet for sample documents
        /// </summary>
        public static string StyleSheet
        {
            get
            {
                return @"
                    h1, h2, h3 { color: navy; font-weight:normal; }
                    body { font:8pt Tahoma }
		            pre  { border:solid 1px gray; background-color:#eee; padding:1em }
                    .gray    { color:gray; }
                    .example { background-color:#efefef; corner-radius:5px; padding:0.5em; }
                    .caption { font-weight:bold }
                    .whitehole { background-color:white; corner-radius:5px; padding:10px; }
                    .servicesT { font-family:Verdana; font-weight:normal; font-size:8px;
                        color:#404040; width:320px; background-color:#fafafa; border:1px #6699CC solid;
                        border-collapse:collapse; border-spacing:0px; margin-top:0px;}
                    .servHd { border-bottom:2px solid #6699CC; background-color:#BEC8D1;
                        text-align:center; font-family:Verdana; font-weight:bold; font-size:8px; color:#404040;}
                    td { border-bottom: 1px dotted #6699CC; font-family: Verdana, sans-serif, Arial;
                        font-weight: normal; font-size:8px; color: #404040; background-color: white;
                        text-align: left; padding-left: 3px;}
                    .servBodL { border-left:1px dotted #CEDCEA; }";
            }
        }
        /*
        /// <summary>
        /// Gets a star image
        /// </summary>
        public static Image StarIcon
        {
            get { return Properties.Resources.favorites32; }
        }

        /// <summary>
        /// Gets the font icon
        /// </summary>
        public static Image FontIcon
        {
            get { return Properties.Resources.font32; }
        }

        /// <summary>
        /// Gets the comment icon
        /// </summary>
        public static Image CommentIcon
        {
            get { return Properties.Resources.comment16; }
        }

        /// <summary>
        /// Gets the image icon
        /// </summary>
        public static Image ImageIcon
        {
            get { return Properties.Resources.image32; }
        }

        /// <summary>
        /// Gets the method icon
        /// </summary>
        public static Image MethodIcon
        {
            get { return Properties.Resources.method16; }
        }

        /// <summary>
        /// Gets the property icon
        /// </summary>
        public static Image PropertyIcon
        {
            get { return Properties.Resources.property16; }
        }

*/

        #endregion

    }
}
