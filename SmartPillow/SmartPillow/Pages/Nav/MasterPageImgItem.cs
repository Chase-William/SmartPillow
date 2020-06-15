using System;
using System.Collections.Generic;
using System.Text;
/*

    This file contains the MasterPageImgItem class.   
    The MasterPageImgItem is used to store information for the MasterPage class.   
 
*/
namespace SmartPillow.Pages.Nav
{
    public class MasterPageImgItem
    {
        /// <summary>
        ///     The title used for displaying the item
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        ///     The type to be instantiated when invoked.
        /// </summary>
        public Type TargetType { get; set; }
        /// <summary>
        ///      The path to the image to be displayed along side the title.
        /// </summary>
        public string ImgPath { get; set; }
    }
}
