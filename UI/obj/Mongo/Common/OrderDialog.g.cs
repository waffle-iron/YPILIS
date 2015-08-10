﻿#pragma checksum "..\..\..\Common\OrderDialog.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "29F4C033A97DD51524F18448E652786C"
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


namespace YellowstonePathology.UI.Common {
    
    
    /// <summary>
    /// OrderDialog
    /// </summary>
    public partial class OrderDialog : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 193 "..\..\..\Common\OrderDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridTreeView;
        
        #line default
        #line hidden
        
        
        #line 282 "..\..\..\Common\OrderDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonOrder;
        
        #line default
        #line hidden
        
        
        #line 289 "..\..\..\Common\OrderDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonDelete;
        
        #line default
        #line hidden
        
        
        #line 290 "..\..\..\Common\OrderDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonClose;
        
        #line default
        #line hidden
        
        
        #line 298 "..\..\..\Common\OrderDialog.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TreeView TreeViewOrders;
        
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
            System.Uri resourceLocater = new System.Uri("/UserInterface;component/common/orderdialog.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Common\OrderDialog.xaml"
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
            case 7:
            this.gridTreeView = ((System.Windows.Controls.Grid)(target));
            return;
            case 8:
            this.ButtonOrder = ((System.Windows.Controls.Button)(target));
            
            #line 282 "..\..\..\Common\OrderDialog.xaml"
            this.ButtonOrder.Click += new System.Windows.RoutedEventHandler(this.ButtonOrder_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ButtonDelete = ((System.Windows.Controls.Button)(target));
            
            #line 289 "..\..\..\Common\OrderDialog.xaml"
            this.ButtonDelete.Click += new System.Windows.RoutedEventHandler(this.ButtonDelete_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.ButtonClose = ((System.Windows.Controls.Button)(target));
            
            #line 290 "..\..\..\Common\OrderDialog.xaml"
            this.ButtonClose.Click += new System.Windows.RoutedEventHandler(this.ButtonClose_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.TreeViewOrders = ((System.Windows.Controls.TreeView)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 35 "..\..\..\Common\OrderDialog.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.CheckBoxPanelTest_Checked);
            
            #line default
            #line hidden
            
            #line 35 "..\..\..\Common\OrderDialog.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.CheckBoxPanelTest_Unchecked);
            
            #line default
            #line hidden
            break;
            case 2:
            
            #line 43 "..\..\..\Common\OrderDialog.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.CheckBoxPanelTest_Checked);
            
            #line default
            #line hidden
            
            #line 43 "..\..\..\Common\OrderDialog.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.CheckBoxPanelTest_Unchecked);
            
            #line default
            #line hidden
            break;
            case 3:
            
            #line 52 "..\..\..\Common\OrderDialog.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.CheckBoxStain_Checked);
            
            #line default
            #line hidden
            
            #line 52 "..\..\..\Common\OrderDialog.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.CheckBoxStain_Unchecked);
            
            #line default
            #line hidden
            break;
            case 4:
            
            #line 61 "..\..\..\Common\OrderDialog.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.CheckBoxStain_Checked);
            
            #line default
            #line hidden
            
            #line 61 "..\..\..\Common\OrderDialog.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.CheckBoxStain_Unchecked);
            
            #line default
            #line hidden
            break;
            case 5:
            
            #line 70 "..\..\..\Common\OrderDialog.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.CheckBoxStain_Checked);
            
            #line default
            #line hidden
            
            #line 70 "..\..\..\Common\OrderDialog.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.CheckBoxStain_Unchecked);
            
            #line default
            #line hidden
            break;
            case 6:
            
            #line 90 "..\..\..\Common\OrderDialog.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Checked += new System.Windows.RoutedEventHandler(this.CheckBoxStainColor_Checked);
            
            #line default
            #line hidden
            
            #line 90 "..\..\..\Common\OrderDialog.xaml"
            ((System.Windows.Controls.CheckBox)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.CheckBoxStainColor_Unchecked);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

