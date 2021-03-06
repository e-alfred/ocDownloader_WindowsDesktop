﻿#pragma checksum "..\..\..\Screens\NewOCConnection.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7A830EAA9571DDD1B2BE1CCDF7A2BDC4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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
using WpfAnimatedGif;


namespace ocDownloader.Screens {
    
    
    /// <summary>
    /// NewOCConnection
    /// </summary>
    public partial class NewOCConnection : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\Screens\NewOCConnection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border MainWindowBorder;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Screens\NewOCConnection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ConnectionName;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\Screens\NewOCConnection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ConnectionURL;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\Screens\NewOCConnection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ConnectionUsername;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\Screens\NewOCConnection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox ConnectionPassword;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\Screens\NewOCConnection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image NewConnectionLoader;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\Screens\NewOCConnection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveButton;
        
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
            System.Uri resourceLocater = new System.Uri("/ocDownloader;component/screens/newocconnection.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Screens\NewOCConnection.xaml"
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
            this.MainWindowBorder = ((System.Windows.Controls.Border)(target));
            return;
            case 2:
            
            #line 31 "..\..\..\Screens\NewOCConnection.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.NewConnectionClose_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ConnectionName = ((System.Windows.Controls.TextBox)(target));
            
            #line 36 "..\..\..\Screens\NewOCConnection.xaml"
            this.ConnectionName.GotFocus += new System.Windows.RoutedEventHandler(this.FormTextBoxes_GotFocus);
            
            #line default
            #line hidden
            
            #line 36 "..\..\..\Screens\NewOCConnection.xaml"
            this.ConnectionName.LostFocus += new System.Windows.RoutedEventHandler(this.FormTextBoxes_LostFocus);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ConnectionURL = ((System.Windows.Controls.TextBox)(target));
            
            #line 38 "..\..\..\Screens\NewOCConnection.xaml"
            this.ConnectionURL.GotFocus += new System.Windows.RoutedEventHandler(this.FormTextBoxes_GotFocus);
            
            #line default
            #line hidden
            
            #line 38 "..\..\..\Screens\NewOCConnection.xaml"
            this.ConnectionURL.LostFocus += new System.Windows.RoutedEventHandler(this.FormTextBoxes_LostFocus);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ConnectionUsername = ((System.Windows.Controls.TextBox)(target));
            
            #line 40 "..\..\..\Screens\NewOCConnection.xaml"
            this.ConnectionUsername.GotFocus += new System.Windows.RoutedEventHandler(this.FormTextBoxes_GotFocus);
            
            #line default
            #line hidden
            
            #line 40 "..\..\..\Screens\NewOCConnection.xaml"
            this.ConnectionUsername.LostFocus += new System.Windows.RoutedEventHandler(this.FormTextBoxes_LostFocus);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ConnectionPassword = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 42 "..\..\..\Screens\NewOCConnection.xaml"
            this.ConnectionPassword.GotFocus += new System.Windows.RoutedEventHandler(this.FormPasswdBox_GotFocus);
            
            #line default
            #line hidden
            
            #line 42 "..\..\..\Screens\NewOCConnection.xaml"
            this.ConnectionPassword.LostFocus += new System.Windows.RoutedEventHandler(this.FormPasswdBox_LostFocus);
            
            #line default
            #line hidden
            return;
            case 7:
            this.NewConnectionLoader = ((System.Windows.Controls.Image)(target));
            return;
            case 8:
            this.SaveButton = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\..\Screens\NewOCConnection.xaml"
            this.SaveButton.Click += new System.Windows.RoutedEventHandler(this.SaveButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

