using System;
using GLOM.Geometry;

namespace Proteus.Core

{
    public class PLabel:HTMLComponent
    {
        public PLabel(string text) : base(MakeHTML(text))
        {
            BackgroundColor = 0xFFFFFFFF; // default white
        }
        
        public UInt32 BackgroundColor { set; get; }

        private static string MakeHTML(string text)
        {
            return "<span>" + text + "</span>";
        }

        public override void Layout(Point position, Size space)
        {
            base.Layout(position, space);
            _styleSettings["background-color"] = "#"+BackgroundColor.ToString("X8");
        }
    }
}