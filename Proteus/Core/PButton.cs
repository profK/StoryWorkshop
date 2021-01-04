using System;
using System.Text;
using Microsoft.JSInterop;

namespace Proteus.Core
{
    public class PButton:HTMLComponent
    {
        private DotNetObjectReference<PButton> objRefFromJS;
        public event Action<PButton> OnClick;
        public PButton(string text,string bkgImage=null) : base(MakeHTML(text,bkgImage))
        {
            objRefFromJS = DotNetObjectReference.Create(this);
            if (bkgImage != null)
            {
                _styleSettings["background-size"] = "100%";
                _styleSettings["background-origin"] = "content";
                _styleSettings["background-image"] = "url(" + bkgImage + ")";
            }
         
            ProteusContext.JSInvokeVoid("Proteus.attachOnClick",
                _domElement,objRefFromJS,"OnClickReceiver");
        }

        private static string MakeHTML(string text,string bkgdImage)
        {
            if (bkgdImage == null)
            {
                return "<button>" + text + "</button>";
            }
            else
            {
                return "<button>" + text + "</button>";
            }
           
        }

       

        [JSInvokable]
        public void OnClickReceiver()
        {
           OnClick?.Invoke(this);
        }
    }
}