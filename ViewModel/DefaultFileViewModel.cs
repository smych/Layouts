using LayotsMvvm.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LayotsMvvm.ViewModel
{
    public abstract class DefaultFileViewModel : ViewModelBase
    {
        public virtual string GetFileTitle { get; set; }
        public virtual string GetFilePath { get; set; }
    }
}
