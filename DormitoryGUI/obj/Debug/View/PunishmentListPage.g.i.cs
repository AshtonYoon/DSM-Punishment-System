﻿#pragma checksum "..\..\..\View\PunishmentListPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "626A6E7B0AA977208DC5CF95FD64ADF9"
//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace DormitoryGUI.View {
    
    
    /// <summary>
    /// PunishmentListPage
    /// </summary>
    public partial class PunishmentListPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 380 "..\..\..\View\PunishmentListPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DormitoryGUI.View.SideMenuButton BackButton;
        
        #line default
        #line hidden
        
        
        #line 440 "..\..\..\View\PunishmentListPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView BadList;
        
        #line default
        #line hidden
        
        
        #line 461 "..\..\..\View\PunishmentListPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView GoodList;
        
        #line default
        #line hidden
        
        
        #line 525 "..\..\..\View\PunishmentListPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton GoodPoint;
        
        #line default
        #line hidden
        
        
        #line 529 "..\..\..\View\PunishmentListPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton BadPoint;
        
        #line default
        #line hidden
        
        
        #line 536 "..\..\..\View\PunishmentListPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PunishmentName;
        
        #line default
        #line hidden
        
        
        #line 549 "..\..\..\View\PunishmentListPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DormitoryGUI.View.CustomSlider MinimumPoint;
        
        #line default
        #line hidden
        
        
        #line 552 "..\..\..\View\PunishmentListPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal DormitoryGUI.View.CustomSlider MaximumPoint;
        
        #line default
        #line hidden
        
        
        #line 553 "..\..\..\View\PunishmentListPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddPushimentListButton;
        
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
            System.Uri resourceLocater = new System.Uri("/DormitoryGUI;component/view/punishmentlistpage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\PunishmentListPage.xaml"
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
            this.BackButton = ((DormitoryGUI.View.SideMenuButton)(target));
            return;
            case 2:
            this.BadList = ((System.Windows.Controls.ListView)(target));
            
            #line 445 "..\..\..\View\PunishmentListPage.xaml"
            this.BadList.SizeChanged += new System.Windows.SizeChangedEventHandler(this.SearchList_SizeChanged);
            
            #line default
            #line hidden
            
            #line 446 "..\..\..\View\PunishmentListPage.xaml"
            this.BadList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.SearchList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.GoodList = ((System.Windows.Controls.ListView)(target));
            
            #line 466 "..\..\..\View\PunishmentListPage.xaml"
            this.GoodList.SizeChanged += new System.Windows.SizeChangedEventHandler(this.SearchList_SizeChanged);
            
            #line default
            #line hidden
            
            #line 467 "..\..\..\View\PunishmentListPage.xaml"
            this.GoodList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.SearchList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 490 "..\..\..\View\PunishmentListPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EditPunishmentListButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 504 "..\..\..\View\PunishmentListPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DelPunishmentListButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.GoodPoint = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 7:
            this.BadPoint = ((System.Windows.Controls.RadioButton)(target));
            return;
            case 8:
            this.PunishmentName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.MinimumPoint = ((DormitoryGUI.View.CustomSlider)(target));
            return;
            case 10:
            this.MaximumPoint = ((DormitoryGUI.View.CustomSlider)(target));
            return;
            case 11:
            this.AddPushimentListButton = ((System.Windows.Controls.Button)(target));
            
            #line 555 "..\..\..\View\PunishmentListPage.xaml"
            this.AddPushimentListButton.Click += new System.Windows.RoutedEventHandler(this.AddPushimentListButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

