﻿using Sims.Model;
using Sims.UI.Components.Login.View;
using Sims.UI.Dialogs.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sims
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            if (ApplicationContext.Instance.User == null)
            {
                LoginView view = new LoginView();
                view.ShowDialog();
            }
            

            InitializeComponent();
        }
    }
}
