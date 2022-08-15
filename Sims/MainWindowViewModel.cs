using Sims.CompositeComon;
using Sims.UI.Components.Toolbar.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sims
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ToolbarViewModel toolbarViewModel;
        public ToolbarViewModel ToolbarViewModel
        {
            get { return toolbarViewModel; }
            set { toolbarViewModel = value; }
        }
    }
}
