/*
 * Created by SharpDevelop.
 * User: en21165
 * Date: 02/18/2013
 * Time: 16:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Input;

namespace MVVM.ViewModel
{
	/// <summary>
	/// Description of DelegateCommand.
	/// </summary>
public class DelegateCommand : ICommand
{
    private readonly Predicate<object> _canExecute;
    private readonly Action<object> _execute;
 
    public event EventHandler CanExecuteChanged;
 
    public DelegateCommand(Action<object> execute) 
                   : this(execute, null)
    {
    }
 
    public DelegateCommand(Action<object> execute, 
                   Predicate<object> canExecute)
    {
        _execute = execute;
        _canExecute = canExecute;
    }
 
    public bool CanExecute(object parameter)
    {
        if (_canExecute == null)
        {
            return true;
        }
 
        return _canExecute(parameter);
    }
 
    public void Execute(object parameter)
    {
        _execute(parameter);
    }
 
    public void RaiseCanExecuteChanged()
    {
        if( CanExecuteChanged != null )
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}
 
}
