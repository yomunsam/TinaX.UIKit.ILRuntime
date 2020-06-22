using System;
using System.Collections.Generic;
using System.Reflection;
using TinaX.UIKit;
using TinaX.UIKit.Internal.Adaptor;
using TinaXEditor.XILRuntime;

namespace TinaXEditor.UIKit.XILRuntime.Generator
{
    public class UIKitCLRGenerateDefine : ICLRGenerateDefine
    {
        public void GenerateByAssemblies_InitILRuntime(ILRuntime.Runtime.Enviorment.AppDomain appdomain)
        {
            //注册跨域
            appdomain.RegisterCrossBindingAdaptor(new XUIBehaviourAdaptors());
        }

        /// <summary>
        /// 生成CLR绑定代码的列表
        /// </summary>
        /// <returns></returns>
        public List<Type> GetCLRBindingTypes() => new List<Type>
        {
            typeof(IUIKit),
            typeof(TinaX.UIKit.UIKit),
            typeof(IUIEntity),
            typeof(UIPage),
        };

        public HashSet<FieldInfo> GetCLRBindingExcludeFields() => null;

        public HashSet<MethodBase> GetCLRBindingExcludeMethods() => null;



        public List<Type> GetDelegateTypes() => null;

        public List<Type> GetValueTypeBinders() => null;
    }
}

