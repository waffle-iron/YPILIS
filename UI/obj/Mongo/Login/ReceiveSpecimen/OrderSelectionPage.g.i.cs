﻿#pragma checksum "..\..\..\..\Login\ReceiveSpecimen\OrderSelectionPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "2A9077B72899A6DBE9548DB69E36718F"
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
using YellowstonePathology.UI.CustomControls;


namespace YellowstonePathology.UI.Login.ReceiveSpecimen {
    
    
    /// <summary>
    /// OrderSelectionPage
    /// </summary>
    public partial class OrderSelectionPage : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 66 "..\..\..\..\Login\ReceiveSpecimen\OrderSelectionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ListViewPanelSets;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\..\Login\ReceiveSpecimen\OrderSelectionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox ListBoxFacilities;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\..\Login\ReceiveSpecimen\OrderSelectionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonBack;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\..\..\Login\ReceiveSpecimen\OrderSelectionPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonOrderTest;
        
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
            System.Uri resourceLocater = new System.Uri("/UserInterface;component/login/receivespecimen/orderselectionpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Login\ReceiveSpecimen\OrderSelectionPage.xaml"
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
            this.ListViewPanelSets = ((System.Windows.Controls.ListView)(target));
            return;
            case 2:
            this.ListBoxFacilities = ((System.Windows.Controls.ListBox)(target));
            
            #line 91 "..\..\..\..\Login\ReceiveSpecimen\OrderSelectionPage.xaml"
            this.ListBoxFacilities.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ListBoxFacilities_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ButtonBack = ((System.Windows.Controls.Button)(target));
            
            #line 100 "..\..\..\..\Login\ReceiveSpecimen\OrderSelectionPage.xaml"
            this.ButtonBack.Click += new System.Windows.RoutedEventHandler(this.ButtonBack_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ButtonOrderTest = ((System.Windows.Controls.Button)(target));
            
            #line 101 "..\..\..\..\Login\ReceiveSpecimen\OrderSelectionPage.xaml"
            this.ButtonOrderTest.Click += new System.Windows.RoutedEventHandler(this.ButtonOrderTest_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

