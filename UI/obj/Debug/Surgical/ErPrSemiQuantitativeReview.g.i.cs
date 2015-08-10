﻿#pragma checksum "..\..\..\Surgical\ErPrSemiQuantitativeReview.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B7F386D273963AD5E10EF33B74A769A6"
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


namespace YellowstonePathology.UI.Surgical {
    
    
    /// <summary>
    /// ErPrSemiQuantitativeReview
    /// </summary>
    public partial class ErPrSemiQuantitativeReview : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 6 "..\..\..\Surgical\ErPrSemiQuantitativeReview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal YellowstonePathology.UI.Surgical.ErPrSemiQuantitativeReview ErPrSemiQuantitativeReviewControl;
        
        #line default
        #line hidden
        
        
        #line 197 "..\..\..\Surgical\ErPrSemiQuantitativeReview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock TextBlockSignature;
        
        #line default
        #line hidden
        
        
        #line 215 "..\..\..\Surgical\ErPrSemiQuantitativeReview.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ItemsControl ItemsControlAmendment;
        
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
            System.Uri resourceLocater = new System.Uri("/UserInterface;component/surgical/erprsemiquantitativereview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Surgical\ErPrSemiQuantitativeReview.xaml"
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
            this.ErPrSemiQuantitativeReviewControl = ((YellowstonePathology.UI.Surgical.ErPrSemiQuantitativeReview)(target));
            return;
            case 6:
            
            #line 80 "..\..\..\Surgical\ErPrSemiQuantitativeReview.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonAddAmendment_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.TextBlockSignature = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            
            #line 200 "..\..\..\Surgical\ErPrSemiQuantitativeReview.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonSetErPrInterpretation_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 201 "..\..\..\Surgical\ErPrSemiQuantitativeReview.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonViewDocument_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 208 "..\..\..\Surgical\ErPrSemiQuantitativeReview.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonPanelSetSignature_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.ItemsControlAmendment = ((System.Windows.Controls.ItemsControl)(target));
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
            case 2:
            
            #line 38 "..\..\..\Surgical\ErPrSemiQuantitativeReview.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBoxAmendmentUsers_SelectionChanged);
            
            #line default
            #line hidden
            break;
            case 3:
            
            #line 49 "..\..\..\Surgical\ErPrSemiQuantitativeReview.xaml"
            ((System.Windows.Controls.TextBox)(target)).RequestBringIntoView += new System.Windows.RequestBringIntoViewEventHandler(this.Any_RequestBringIntoView);
            
            #line default
            #line hidden
            break;
            case 4:
            
            #line 54 "..\..\..\Surgical\ErPrSemiQuantitativeReview.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonDeleteAmendment_Click);
            
            #line default
            #line hidden
            break;
            case 5:
            
            #line 61 "..\..\..\Surgical\ErPrSemiQuantitativeReview.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ButtonSignAmendment_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

