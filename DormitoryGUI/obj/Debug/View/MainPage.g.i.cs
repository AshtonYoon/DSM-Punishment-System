﻿#pragma checksum "..\..\..\View\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "AFEBE0E332059D3D1B5A6788C2CFD54E"
//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

using DormitoryGUI;
using DormitoryGUI.View;
using DormitoryGUI.ViewModel;
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


namespace DormitoryGUI {
    
    
    /// <summary>
    /// MainPage
    /// </summary>
    public partial class MainPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 380 "..\..\..\View\MainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid PointProcedure;
        
        #line default
        #line hidden
        
        
        #line 386 "..\..\..\View\MainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ApplyPointButton;
        
        #line default
        #line hidden
        
        
        #line 448 "..\..\..\View\MainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel ScoreCategory;
        
        #line default
        #line hidden
        
        
        #line 471 "..\..\..\View\MainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CurrentDate;
        
        #line default
        #line hidden
        
        
        #line 478 "..\..\..\View\MainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TeachereName;
        
        #line default
        #line hidden
        
        
        #line 492 "..\..\..\View\MainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DormitoryGUI.View.SideMenuButton UploadExcel;
        
        #line default
        #line hidden
        
        
        #line 508 "..\..\..\View\MainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchCommand;
        
        #line default
        #line hidden
        
        
        #line 532 "..\..\..\View\MainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SearchButton;
        
        #line default
        #line hidden
        
        
        #line 545 "..\..\..\View\MainPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView SearchList;
        
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
            System.Uri resourceLocater = new System.Uri("/DormitoryGUI;component/view/mainpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\MainPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.PointProcedure = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.ApplyPointButton = ((System.Windows.Controls.Button)(target));
            
            #line 395 "..\..\..\View\MainPage.xaml"
            this.ApplyPointButton.Click += new System.Windows.RoutedEventHandler(this.ApplyPointButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ScoreCategory = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 4:
            this.CurrentDate = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.TeachereName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.UploadExcel = ((DormitoryGUI.View.SideMenuButton)(target));
            return;
            case 7:
            this.SearchCommand = ((System.Windows.Controls.TextBox)(target));
            
            #line 517 "..\..\..\View\MainPage.xaml"
            this.SearchCommand.KeyUp += new System.Windows.Input.KeyEventHandler(this.SearchCommand_KeyUp);
            
            #line default
            #line hidden
            return;
            case 8:
            this.SearchButton = ((System.Windows.Controls.Button)(target));
            
            #line 533 "..\..\..\View\MainPage.xaml"
            this.SearchButton.Click += new System.Windows.RoutedEventHandler(this.SearchButton_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.SearchList = ((System.Windows.Controls.ListView)(target));
            
            #line 550 "..\..\..\View\MainPage.xaml"
            this.SearchList.SizeChanged += new System.Windows.SizeChangedEventHandler(this.SearchList_SizeChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

