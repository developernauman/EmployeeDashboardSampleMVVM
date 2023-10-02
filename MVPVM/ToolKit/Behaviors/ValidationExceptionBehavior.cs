using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace MVPVM.ToolKit.Behaviors
{
    public class ValidationExceptionBehavior : Behavior<FrameworkElement>
    {
        private int validationExceptionCount;

        protected override void OnAttached()
        {
            this.AssociatedObject.AddHandler(Validation.ErrorEvent, new EventHandler<ValidationErrorEventArgs>(this.OnValidationError));
        }

        private void OnValidationError(object sender, ValidationErrorEventArgs e)
        {
            if (e.Error.Exception == null)
            {
                return;
            }

            if (e.Action == ValidationErrorEventAction.Added)
            {
                this.validationExceptionCount++;
            }
            else
            {
                this.validationExceptionCount--;
            }

            if (this.AssociatedObject.DataContext is IValidationExceptionHandler)
            {
                var viewModel = (IValidationExceptionHandler)this.AssociatedObject.DataContext;
                viewModel.ValidationExceptionsChanged(this.validationExceptionCount);
            }
        }
    }
}
