using Sims.CompositeComon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims.UI.Components.Toolbar.ViewModel
{
    public class ToolbarViewModel : ViewModelBase
    {
        private RelayCommand homeCommand;
        private RelayCommand startNewExaminationCommand;
        private RelayCommand examinationCommand;
        private RelayCommand termsCommand;
        private RelayCommand medicineCommand;
        private RelayCommand patientsCommand;
        private RelayCommand vacationCommand;
        private RelayCommand lightThemeCommand;
        private RelayCommand darkThemeCommand;
        private RelayCommand enCommand;
        private RelayCommand srCommand;


        private MainWindowViewModel mainWindowViewModel;
        public MainWindowViewModel MainWindowViewModel
        {
            get { return mainWindowViewModel; }
            set { mainWindowViewModel = value; }
        }

        
    }
}
