using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CardGame.Web.Controllers.HtmlHelpers
{
    public class ChartThemes
    {
        #region CHARTTHEMES
        /// <summary>
        /// Theme for Charthelper
        /// </summary>
        /// <returns></returns>
        public static string GetMyCustomTheme()
        {
            return @"
<Chart BackColor=""Transparent"" BackGradientStyle=""DiagonalRight"" BorderColor=""129, 75, 55"" 
   BorderWidth=""5"" BorderlineDashStyle=""Solid"" Palette=""EarthTones"" 
   AntiAliasing=""All"">
              
   <ChartAreas>
      <ChartArea Name=""Default"" _Template_=""All""
         BackColor=""Transparent"" BackSecondaryColor=""Transparent"" 
         BorderColor=""64, 64, 64, 64"" BorderDashStyle=""Solid"" 
         ShadowColor=""Transparent"">
      </ChartArea>
   </ChartAreas>
   <Legends>        
        <Legend _Template_=""All"" BackColor=""Transparent"" Font=""Xena, 16pt, style=Bold""  IsTextAutoFit=""False""/>
   </Legends>
</Chart>";
        }
    } 
    #endregion
}