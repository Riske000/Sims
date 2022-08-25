using Sims.UI.Dialogs.ViewModel;
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
using System.Windows.Shapes;

namespace Sims.UI.Dialogs.View
{
    /// <summary>
    /// Interaction logic for MedicinesView.xaml
    /// </summary>
    public partial class MedicinesView : Window
    {
        public MedicinesView()
        {
            InitializeComponent();

            DataContext = new MedicinesController(this);
        }
    }
}
