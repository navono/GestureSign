﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GestureSign.CorePlugins.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("GestureSign.CorePlugins.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
        ///&lt;Language DisplayName=&quot;English&quot; Culture=&quot;en&quot; Version=&quot;6.1&quot;&gt;
        ///  &lt;CorePlugins&gt;
        ///    &lt;ActivateWindow&gt;
        ///      &lt;Name&gt;Activate Window&lt;/Name&gt;
        ///      &lt;Description&gt;Activate the specified window&lt;/Description&gt;
        ///      &lt;ClassName&gt;Window Class Name&lt;/ClassName&gt;
        ///      &lt;WindowTitle&gt;Window Title&lt;/WindowTitle&gt;
        ///      &lt;UsingRegularExpression&gt;Match as pattern&lt;/UsingRegularExpression&gt;
        ///      &lt;Timeout&gt;Wait for window to exist&lt;/Timeout&gt;
        ///      &lt;Millisecond&gt;milliseconds&lt;/Millisecond&gt;
        ///    &lt; [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string en {
            get {
                return ResourceManager.GetString("en", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &lt;?xml version=&quot;1.0&quot; encoding=&quot;utf-8&quot; ?&gt;
        ///&lt;Language DisplayName=&quot;中文&quot; Culture=&quot;zh-CN&quot; Version=&quot;6.1&quot;&gt;
        ///  &lt;CorePlugins&gt;
        ///    &lt;ActivateWindow&gt;
        ///      &lt;Name&gt;激活窗口&lt;/Name&gt;
        ///      &lt;Description&gt;使指定窗口前置&lt;/Description&gt;
        ///      &lt;ClassName&gt;窗口类名&lt;/ClassName&gt;
        ///      &lt;WindowTitle&gt;窗口名&lt;/WindowTitle&gt;
        ///      &lt;UsingRegularExpression&gt;使用正则表达式&lt;/UsingRegularExpression&gt;
        ///      &lt;Timeout&gt;等待窗口时间&lt;/Timeout&gt;
        ///      &lt;Millisecond&gt;毫秒&lt;/Millisecond&gt;
        ///    &lt;/ActivateWindow&gt;
        ///    &lt;Delay&gt;
        ///      &lt;Name&gt;延时&lt;/Name&gt;
        ///      &lt;Description&gt;延迟 {0} 毫秒&lt;/Descriptio [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string zh {
            get {
                return ResourceManager.GetString("zh", resourceCulture);
            }
        }
    }
}
