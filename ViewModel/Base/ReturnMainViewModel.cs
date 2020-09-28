using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayotsMvvm.ViewModel.Base
{
    public abstract class ReturnMainViewModel
    {
        public static MainViewModel GetMainViewModel { get; set; }
    }
}
