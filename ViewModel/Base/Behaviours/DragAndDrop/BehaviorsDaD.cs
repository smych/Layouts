using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LayotsMvvm.ViewModel.Base.Behaviours.DragAndDrop
{
    public static class BehaviorsDaD
    {
        #region DandBehaviour

        public static readonly DependencyProperty DandBehaviourProperty =
            DependencyProperty.RegisterAttached("DandBehaviour", typeof(ICommand), typeof(BehaviorsDaD),
                new FrameworkPropertyMetadata(null,
                    FrameworkPropertyMetadataOptions.None,
                    OnDandBehaviourChanged));
        public static ICommand GetDandBehaviour(DependencyObject d)
        {
            return (ICommand)d.GetValue(DandBehaviourProperty);
        }
        public static void SetDandBehaviour(DependencyObject d, ICommand value)
        {
            d.SetValue(DandBehaviourProperty, value);
        }
        private static void OnDandBehaviourChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Grid g = d as Grid; // Grid
            if (g != null)
            {
                g.Drop += (s, a) =>
                {
                    ICommand iCommand = GetDandBehaviour(d);
                    if (iCommand != null)
                    {
                        if (iCommand.CanExecute(a.Data))
                        {
                            iCommand.Execute(a.Data);
                        }
                    }
                };
            }
            else
            {
                throw new ApplicationException("Non grid");
            }
        }
        #endregion
    }
}
