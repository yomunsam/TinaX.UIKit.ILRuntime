using TinaX.UIKit;
using TinaX.UIKit.Internal.Adaptor;
using TinaX.UIKit.Internal.CLRMethodRedirections;

namespace TinaX.XILRuntime.Registers
{
    public unsafe static class UIKitILRTRegisterExtend
    {
        public static IXILRuntime RegisterUIKit(this IXILRuntime xil)
        {
            //跨域继承适配器
            xil.RegisterCrossBindingAdaptor(new XUIBehaviourAdaptors());

            //注册委托适配器
            xil.DelegateManager.RegisterMethodDelegate<IUIEntity, XException>();

            //CLR重定向
            RedirectUIKit.Register(xil);

            return xil;
        }
    }
}
