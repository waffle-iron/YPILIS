﻿#pragma checksum "..\..\..\..\Login\ReceiveSpecimen\ContainerMatchingPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4D86E641848718F421A78F9D1511E026"
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
using YellowstonePathology.UI.Converter;
using YellowstonePathology.UI.ValidationRules;


namespace YellowstonePathology.UI.Login.ReceiveSpecimen {
    
    
    /// <summary>
    /// ContainerMatchingPage
    /// </summary>
    public partial class ContainerMatchingPage : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 118 "..\..\..\..\Login\ReceiveSpecimen\ContainerMatchingPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxOrderedAs;
        
        #line default
        #line hidden
        
        
        #line 121 "..\..\..\..\Login\ReceiveSpecimen\ContainerMatchingPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxAccessionAs;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\..\..\Login\ReceiveSpecimen\ContainerMatchingPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxCollectionDate;
        
        #line default
        #line hidden
        
        
        #line 167 "..\..\..\..\Login\ReceiveSpecimen\ContainerMatchingPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonNext;
        
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
            System.Uri resourceLocater = new System.Uri("/UserInterface;component/login/receivespecimen/containermatchingpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Login\ReceiveSpecimen\ContainerMatchingPage.xaml"
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
            
            #line 92 "..\..\..\..\Login\ReceiveSpecimen\ContainerMatchingPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonSpecimenNumberMatchStatus_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TextBoxOrderedAs = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.TextBoxAccessionAs = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.TextBoxCollectionDate = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            
            #line 132 "..\..\..\..\Login\ReceiveSpecimen\ContainerMatchingPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonSpecimenDescriptionMatchStatus_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 140 "..\..\..\..\Login\ReceiveSpecimen\ContainerMatchingPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonClientDetailFresh_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 143 "..\..\..\..\Login\ReceiveSpecimen\ContainerMatchingPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonClientDetailFormalin_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 146 "..\..\..\..\Login\ReceiveSpecimen\ContainerMatchingPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonClientDetailBPlus_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 149 "..\..\..\..\Login\ReceiveSpecimen\ContainerMatchingPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonClientDetailCytolyt_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 152 "..\..\..\..\Login\ReceiveSpecimen\ContainerMatchingPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonClientDetail95IPA_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 155 "..\..\..\..\Login\ReceiveSpecimen\ContainerMatchingPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonClientDetailNotApplicable_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.ButtonNext = ((System.Windows.Controls.Button)(target));
            
            #line 167 "..\..\..\..\Login\ReceiveSpecimen\ContainerMatchingPage.xaml"
            this.ButtonNext.Click += new System.Windows.RoutedEventHandler(this.ButtonNext_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

