﻿#pragma checksum "..\..\..\Login\SvhOrderIdLookupPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FFAA63AE8BB4FF8C6C229816FED1FB41"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using YellowstonePathology.UI.TemplateSelector;


namespace YellowstonePathology.UI.Login {
    
    
    /// <summary>
    /// SvhOrderIdLookupPage
    /// </summary>
    public partial class SvhOrderIdLookupPage : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 40 "..\..\..\Login\SvhOrderIdLookupPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxSvhAccountNo;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Login\SvhOrderIdLookupPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonFindClientOrderSvhAccountNo;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\Login\SvhOrderIdLookupPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonBack;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\Login\SvhOrderIdLookupPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonClose;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/UserInterface;component/login/svhorderidlookuppage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Login\SvhOrderIdLookupPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.TextBoxSvhAccountNo = ((System.Windows.Controls.TextBox)(target));
            
            #line 40 "..\..\..\Login\SvhOrderIdLookupPage.xaml"
            this.TextBoxSvhAccountNo.KeyUp += new System.Windows.Input.KeyEventHandler(this.TextBoxSvhAccountNo_KeyUp);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ButtonFindClientOrderSvhAccountNo = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\..\Login\SvhOrderIdLookupPage.xaml"
            this.ButtonFindClientOrderSvhAccountNo.Click += new System.Windows.RoutedEventHandler(this.ButtonSearchBySvhAccountNo_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ButtonBack = ((System.Windows.Controls.Button)(target));
            
            #line 51 "..\..\..\Login\SvhOrderIdLookupPage.xaml"
            this.ButtonBack.Click += new System.Windows.RoutedEventHandler(this.ButtonBack_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ButtonClose = ((System.Windows.Controls.Button)(target));
            
            #line 52 "..\..\..\Login\SvhOrderIdLookupPage.xaml"
            this.ButtonClose.Click += new System.Windows.RoutedEventHandler(this.ButtonClose_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

