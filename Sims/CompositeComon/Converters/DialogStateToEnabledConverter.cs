using Sims.CompositeComon.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Sims.CompositeComon.Converters
{
    public class DialogStateToEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return false;
            }

            DialogState dialogState;

            try
            {
                dialogState = (DialogState)value;
            }
            catch (Exception)
            {
                return false;
            }

            return dialogState == DialogState.Add || dialogState == DialogState.Edit;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
