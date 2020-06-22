using ILRuntime.CLR.Method;
using ILRuntime.CLR.Utils;
using ILRuntime.Runtime;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TinaX.XILRuntime;
using UnityEngine;
using UniRx;
using TinaX.XComponent.Utils;
using TinaX.XComponent;
using TinaX.XILRuntime.Internal.Redirect;

namespace TinaX.UIKit.Internal.CLRMethodRedirections
{
    public unsafe static class RedirectUIKit
    {
        private static RedirectMapping mapping;
        private static IXILRuntime m_XIL => TinaX.XCore.GetMainInstance().Services.Get<IXILRuntime>();

        static RedirectUIKit()
        {
            mapping = new RedirectMapping();

            // ------ UIName / Behaviour / Args
            mapping.Register("OpenUIAsync", 0, 3, new string[]{
                "System.String",
                "TinaX.XComponent.XBehaviour",
                "System.Object[]"
            }, OpenUIAsync_Task_Name_Behaviour_Args);

            mapping.Register("OpenUI", 0, 3, new string[]{
                "System.String",
                "TinaX.XComponent.XBehaviour",
                "System.Object[]"
            }, OpenUI_Name_Behavior_Args);

            mapping.Register("OpenUIAsync", 0, 4, new string[]{
                "System.String",
                "TinaX.XComponent.XBehaviour",
                "System.Action`2[TinaX.UIKit.IUIEntity,TinaX.XException]",
                "System.Object[]"
            }, OpenUIAsync_Callback_Name_Behaviour_Args);

            // ------ UIName / Behaviour / Params /Args

            mapping.Register("OpenUI", 0, 4, new string[]{
                "System.String",
                "TinaX.XComponent.XBehaviour",
                "TinaX.UIKit.OpenUIParam",
                "System.Object[]"
            }, OpenUI_Name_Behaviour_Param_Args);
            
            mapping.Register("OpenUIAsync", 0, 4, new string[]{
                "System.String",
                "TinaX.XComponent.XBehaviour",
                "TinaX.UIKit.OpenUIParam",
                "System.Object[]"
            }, OpenUIAsync_Task_Name_Behaviour_Param_Args);
            
            mapping.Register("OpenUIAsync", 0, 5, new string[]{
                "System.String",
                "TinaX.XComponent.XBehaviour",
                "TinaX.UIKit.OpenUIParam",
                "System.Action`2[TinaX.UIKit.IUIEntity,TinaX.XException]",
                "System.Object[]"
            }, OpenUIAsync_Callback_Name_Behavior_Param_Args);


        }

        public static void Register(IXILRuntime xil)
        {
            var methods = typeof(IUIKit).GetMethods();
            foreach(var method in methods)
            {
                var redirection = mapping.GetRedirection(method);
                if (redirection == null)
                    continue;

                xil.RegisterCLRMethodRedirection(method, redirection);
            }
        }

        
        

        #region UIName / Behaviour / Args

        //Task<IUIEntity> OpenUIAsync(string UIName, XBehaviour behaviour, params object[] args);
        private static StackObject* OpenUIAsync_Task_Name_Behaviour_Args(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 4);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Object[] _args = (System.Object[])typeof(System.Object[]).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            TinaX.XComponent.XBehaviour behaviour = (TinaX.XComponent.XBehaviour)typeof(TinaX.XComponent.XBehaviour).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            System.String _UIName = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 4);
            TinaX.UIKit.IUIKit instance_of_this_method = (TinaX.UIKit.IUIKit)typeof(TinaX.UIKit.IUIKit).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            //得在这里实现依赖注入
            m_XIL.InjectObject(behaviour);

            var result_of_this_method = instance_of_this_method.OpenUIAsync(_UIName, _args); //单纯打开UI，不传入behaviour
            result_of_this_method.ToObservable()
                .ObserveOnMainThread()
                .SubscribeOnMainThread()
                .Subscribe(entity =>
                {
                    if (entity.UIPage.UIMainHandler != null && entity.UIPage.UIMainHandler is TinaX.XComponent.XComponent)
                    {
                        var xcomponent = entity.UIPage.UIMainHandler as TinaX.XComponent.XComponent;
                        XILXComponentUtil.InjectBindings(xcomponent, behaviour);
                        xcomponent.AddBehaviour(behaviour);
                    }
                    RedirectUIKitSafe.SetEntityIfXUIBehaviour(ref behaviour, ref entity);
                }, e => { });

            object obj_result_of_this_method = result_of_this_method;
            if (obj_result_of_this_method is CrossBindingAdaptorType)
            {
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        //IUIEntity OpenUI(string UIName, XBehaviour behaviour, params object[] args);
        private static StackObject* OpenUI_Name_Behavior_Args(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 4);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Object[] @args = (System.Object[])typeof(System.Object[]).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            TinaX.XComponent.XBehaviour behaviour = (TinaX.XComponent.XBehaviour)typeof(TinaX.XComponent.XBehaviour).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            System.String @UIName = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 4);
            TinaX.UIKit.IUIKit instance_of_this_method = (TinaX.UIKit.IUIKit)typeof(TinaX.UIKit.IUIKit).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            //依赖注入
            m_XIL.InjectObject(behaviour);
            
            var entity = instance_of_this_method.OpenUI(@UIName, @args);

            if(entity.UIPage.UIMainHandler != null && entity.UIPage.UIMainHandler is XComponent.XComponent)
            {
                var xcomponent = entity.UIPage.UIMainHandler as XComponent.XComponent;
                XILXComponentUtil.InjectBindings(xcomponent, behaviour);
                xcomponent.AddBehaviour(behaviour);
            }
            RedirectUIKitSafe.SetEntityIfXUIBehaviour(ref behaviour, ref entity);

            object obj_result_of_this_method = entity;
            if (obj_result_of_this_method is CrossBindingAdaptorType)
            {
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, entity);
        }

        //void OpenUIAsync(string UIName, XBehaviour behaviour, Action<IUIEntity, XException> callback, params object[] args);
        private static StackObject* OpenUIAsync_Callback_Name_Behaviour_Args(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 5);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Object[] @args = (System.Object[])typeof(System.Object[]).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Action<TinaX.UIKit.IUIEntity, TinaX.XException> callback = (System.Action<TinaX.UIKit.IUIEntity, TinaX.XException>)typeof(System.Action<TinaX.UIKit.IUIEntity, TinaX.XException>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            TinaX.XComponent.XBehaviour behaviour = (TinaX.XComponent.XBehaviour)typeof(TinaX.XComponent.XBehaviour).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 4);
            System.String @UIName = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 5);
            TinaX.UIKit.IUIKit instance_of_this_method = (TinaX.UIKit.IUIKit)typeof(TinaX.UIKit.IUIKit).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            //依赖注入处理 
            m_XIL.InjectObject(behaviour);
            instance_of_this_method.OpenUIAsync(@UIName,(entity, err)=> { 
                if(err != null)
                {
                    callback?.Invoke(entity, err);
                }
                else
                {
                    if (entity.UIPage.UIMainHandler != null && entity.UIPage.UIMainHandler is XComponent.XComponent)
                    {
                        var xcomponent = entity.UIPage.UIMainHandler as XComponent.XComponent;
                        XILXComponentUtil.InjectBindings(xcomponent, behaviour);
                        xcomponent.AddBehaviour(behaviour);
                    }

                    RedirectUIKitSafe.SetEntityIfXUIBehaviour(ref behaviour, ref entity);
                    callback?.Invoke(entity, err);
                }
            }, @args);

            return __ret;
        }

        #endregion

        #region UIName / Behaviour / Params /Args

        //IUIEntity OpenUI(string UIName, XBehaviour behaviour, OpenUIParam openUIParam, params object[] args);
        private static StackObject* OpenUI_Name_Behaviour_Param_Args(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 5);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Object[] @args = (System.Object[])typeof(System.Object[]).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            TinaX.UIKit.OpenUIParam openUIParam = (TinaX.UIKit.OpenUIParam)typeof(TinaX.UIKit.OpenUIParam).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            TinaX.XComponent.XBehaviour behaviour = (TinaX.XComponent.XBehaviour)typeof(TinaX.XComponent.XBehaviour).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 4);
            System.String @UIName = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 5);
            TinaX.UIKit.IUIKit instance_of_this_method = (TinaX.UIKit.IUIKit)typeof(TinaX.UIKit.IUIKit).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            //依赖注入
            m_XIL.InjectObject(behaviour);
            openUIParam.DependencyInjection = false;
            openUIParam.xBehaviour = null;
            var entity = instance_of_this_method.OpenUI(@UIName, openUIParam, @args);

            if (entity.UIPage.UIMainHandler != null && entity.UIPage.UIMainHandler is XComponent.XComponent)
            {
                var xcomponent = entity.UIPage.UIMainHandler as XComponent.XComponent;
                XILXComponentUtil.InjectBindings(xcomponent, behaviour);
                xcomponent.AddBehaviour(behaviour);
            }
            RedirectUIKitSafe.SetEntityIfXUIBehaviour(ref behaviour, ref entity);

            object obj_result_of_this_method = entity;
            if (obj_result_of_this_method is CrossBindingAdaptorType)
            {
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, entity);
        }

        //Task<IUIEntity> OpenUIAsync(string UIName, XBehaviour behaviour, OpenUIParam openUIParam, params object[] args);
        private static StackObject* OpenUIAsync_Task_Name_Behaviour_Param_Args(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 5);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Object[] args = (System.Object[])typeof(System.Object[]).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            TinaX.UIKit.OpenUIParam openUIParam = (TinaX.UIKit.OpenUIParam)typeof(TinaX.UIKit.OpenUIParam).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            TinaX.XComponent.XBehaviour behaviour = (TinaX.XComponent.XBehaviour)typeof(TinaX.XComponent.XBehaviour).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 4);
            System.String @UIName = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 5);
            TinaX.UIKit.IUIKit instance_of_this_method = (TinaX.UIKit.IUIKit)typeof(TinaX.UIKit.IUIKit).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            //依赖注入
            m_XIL.InjectObject(behaviour);
            openUIParam.xBehaviour = null;
            openUIParam.DependencyInjection = false;
            var task_entity = instance_of_this_method.OpenUIAsync(@UIName, openUIParam, args);
            task_entity.ToObservable()
                .ObserveOnMainThread()
                .SubscribeOnMainThread()
                .Subscribe(entity =>
                {
                    if (entity.UIPage.UIMainHandler != null && entity.UIPage.UIMainHandler is TinaX.XComponent.XComponent)
                    {
                        var xcomponent = entity.UIPage.UIMainHandler as TinaX.XComponent.XComponent;
                        XILXComponentUtil.InjectBindings(xcomponent, behaviour);
                        xcomponent.AddBehaviour(behaviour);
                    }
                    RedirectUIKitSafe.SetEntityIfXUIBehaviour(ref behaviour, ref entity);

                },e=> { });

            object obj_result_of_this_method = task_entity;
            if (obj_result_of_this_method is CrossBindingAdaptorType)
            {
                return ILIntepreter.PushObject(__ret, __mStack, ((CrossBindingAdaptorType)obj_result_of_this_method).ILInstance);
            }
            return ILIntepreter.PushObject(__ret, __mStack, task_entity);
        }

        //void OpenUIAsync(string UIName, XBehaviour behaviour, OpenUIParam openUIParam, Action<IUIEntity, XException> callback, params object[] args);
        static StackObject* OpenUIAsync_Callback_Name_Behavior_Param_Args(ILIntepreter __intp, StackObject* __esp, IList<object> __mStack, CLRMethod __method, bool isNewObj)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            StackObject* ptr_of_this_method;
            StackObject* __ret = ILIntepreter.Minus(__esp, 6);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 1);
            System.Object[] @args = (System.Object[])typeof(System.Object[]).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 2);
            System.Action<TinaX.UIKit.IUIEntity, TinaX.XException> callback = (System.Action<TinaX.UIKit.IUIEntity, TinaX.XException>)typeof(System.Action<TinaX.UIKit.IUIEntity, TinaX.XException>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 3);
            TinaX.UIKit.OpenUIParam openUIParam = (TinaX.UIKit.OpenUIParam)typeof(TinaX.UIKit.OpenUIParam).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 4);
            TinaX.XComponent.XBehaviour behaviour = (TinaX.XComponent.XBehaviour)typeof(TinaX.XComponent.XBehaviour).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 5);
            System.String UIName = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            ptr_of_this_method = ILIntepreter.Minus(__esp, 6);
            TinaX.UIKit.IUIKit instance_of_this_method = (TinaX.UIKit.IUIKit)typeof(TinaX.UIKit.IUIKit).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack));
            __intp.Free(ptr_of_this_method);

            //依赖注入
            m_XIL.InjectObject(behaviour);
            openUIParam.xBehaviour = null;
            openUIParam.DependencyInjection = false;
            instance_of_this_method.OpenUIAsync(UIName, openUIParam, (entity, err)=> {
                if (err != null)
                    callback?.Invoke(entity, err);
                else
                {
                    if (entity.UIPage.UIMainHandler != null && entity.UIPage.UIMainHandler is XComponent.XComponent)
                    {
                        var xcomponent = entity.UIPage.UIMainHandler as XComponent.XComponent;
                        XILXComponentUtil.InjectBindings(xcomponent, behaviour);
                        xcomponent.AddBehaviour(behaviour);
                    }

                    RedirectUIKitSafe.SetEntityIfXUIBehaviour(ref behaviour, ref entity);
                    callback?.Invoke(entity, err);
                }
                
            }, @args);
            

            return __ret;
        }
        #endregion

    }

    internal static class RedirectUIKitSafe
    {
        //internal static async Task<IUIEntity> WaitUITask(Task<IUIEntity> task, XBehaviour behaviour)
        //{
        //    var entity = await task;
        //    if (entity.UIPage.UIMainHandler != null && entity.UIPage.UIMainHandler is TinaX.XComponent.XComponent)
        //    {
        //        var xcomponent = entity.UIPage.UIMainHandler as TinaX.XComponent.XComponent;
        //        XILXComponentUtil.InjectBindings(xcomponent, behaviour);
        //        xcomponent.AddBehaviour(behaviour);
        //    }

        //    SetEntityIfXUIBehaviour(ref behaviour, ref entity);
        //    return entity;
        //}


        internal static void SetEntityIfXUIBehaviour(ref XBehaviour behaviour, ref IUIEntity entity)
        {
            if (behaviour is XUIBehaviour)
            {
                var uibehaviour = behaviour as XUIBehaviour;
                uibehaviour.UIEntity = entity;
            }
        }
    }

}
