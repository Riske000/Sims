﻿using Sims.CompositeComon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims.UI.Components.Toolbar.ViewModel
{
    public class ToolbarController : ViewModelBase
    {
        private RelayCommand homeCommand;
        


        private MainWindowViewModel mainWindowViewModel;
        public MainWindowViewModel MainWindowViewModel
        {
            get { return mainWindowViewModel; }
            set { mainWindowViewModel = value; }
        }

        
    }
}
