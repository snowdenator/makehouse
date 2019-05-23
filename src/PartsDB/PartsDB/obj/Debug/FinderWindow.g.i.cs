﻿#pragma checksum "..\..\FinderWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2C4BA725A0A569C2F197876A8FFDA5DD2754ED7076FF5D034C783D7292336471"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PartsDB;
using PartsDB.Types;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
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


namespace PartsDB {
    
    
    /// <summary>
    /// FinderWindow
    /// </summary>
    public partial class FinderWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\FinderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock SessionLabel;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\FinderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button LogoutButton;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\FinderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SettingsButton;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\FinderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RefreshInfoButton;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\FinderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddSupplierButton;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\FinderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddPartButton;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\FinderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl InfoTabControl;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\FinderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem PartsTab;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\FinderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid PartsDataGrid;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\FinderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem SuppliersTab;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\FinderWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid SuppliersDataGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/PartsDB;component/finderwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\FinderWindow.xaml"
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
            
            #line 9 "..\..\FinderWindow.xaml"
            ((PartsDB.FinderWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.SessionLabel = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.LogoutButton = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\FinderWindow.xaml"
            this.LogoutButton.Click += new System.Windows.RoutedEventHandler(this.LogoutButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.SettingsButton = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\FinderWindow.xaml"
            this.SettingsButton.Click += new System.Windows.RoutedEventHandler(this.SettingsButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.RefreshInfoButton = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\FinderWindow.xaml"
            this.RefreshInfoButton.Click += new System.Windows.RoutedEventHandler(this.RefreshInfoButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.AddSupplierButton = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\FinderWindow.xaml"
            this.AddSupplierButton.Click += new System.Windows.RoutedEventHandler(this.AddSupplierButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.AddPartButton = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\FinderWindow.xaml"
            this.AddPartButton.Click += new System.Windows.RoutedEventHandler(this.AddPartButton_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.InfoTabControl = ((System.Windows.Controls.TabControl)(target));
            return;
            case 9:
            this.PartsTab = ((System.Windows.Controls.TabItem)(target));
            return;
            case 10:
            this.PartsDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 29 "..\..\FinderWindow.xaml"
            this.PartsDataGrid.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.PartsDataGrid_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 29 "..\..\FinderWindow.xaml"
            this.PartsDataGrid.AutoGeneratingColumn += new System.EventHandler<System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs>(this.PartsDataGrid_AutoGeneratingColumn);
            
            #line default
            #line hidden
            return;
            case 11:
            this.SuppliersTab = ((System.Windows.Controls.TabItem)(target));
            return;
            case 12:
            this.SuppliersDataGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 32 "..\..\FinderWindow.xaml"
            this.SuppliersDataGrid.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.SuppliersDataGrid_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

