﻿#pragma checksum "..\..\LabEventsControlTab.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1872F0AD4151D2654197DE2BB5ED8365"
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


namespace YellowstonePathology.UI {
    
    
    /// <summary>
    /// LabEventsControlTab
    /// </summary>
    public partial class LabEventsControlTab : System.Windows.Controls.TabItem, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\LabEventsControlTab.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ListViewLabEventLog;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\LabEventsControlTab.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ListViewLabEvents;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\LabEventsControlTab.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonViewEvent;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\LabEventsControlTab.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonLogEvent;
        
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
            System.Uri resourceLocater = new System.Uri("/UserInterface;component/labeventscontroltab.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\LabEventsControlTab.xaml"
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
            this.ListViewLabEventLog = ((System.Windows.Controls.ListView)(target));
            return;
            case 2:
            this.ListViewLabEvents = ((System.Windows.Controls.ListView)(target));
            return;
            case 3:
            this.ButtonViewEvent = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\LabEventsControlTab.xaml"
            this.ButtonViewEvent.Click += new System.Windows.RoutedEventHandler(this.ButtonViewEvent_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ButtonLogEvent = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\LabEventsControlTab.xaml"
            this.ButtonLogEvent.Click += new System.Windows.RoutedEventHandler(this.ButtonLogEvent_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

