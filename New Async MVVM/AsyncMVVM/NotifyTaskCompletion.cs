//using System;
//using System.ComponentModel;
//using System.Threading.Tasks;
//public sealed class NotifyTaskCompletion<TResult> 
//{
//    public NotifyTaskCompletion(Task<TResult> task)
//    {
//        Task = task;
//    }


//    public Task<TResult> Task { get; set; }
//    public TResult Result
//    {
//        get
//        {
//            return (Task.Status == TaskStatus.RanToCompletion) ?
//Task.Result : default(TResult);
//        }
//    }
//    public TaskStatus Status { get { return Task.Status; } }
//    public bool IsCompleted { get { return Task.IsCompleted; } }
//    public bool IsNotCompleted { get { return !Task.IsCompleted; } }
//    public bool IsSuccessfullyCompleted
//    {
//        get
//        {
//            return Task.Status ==
//TaskStatus.RanToCompletion;
//        }
//    }
//    public bool IsCanceled { get { return Task.IsCanceled; } }
//    public bool IsFaulted { get { return Task.IsFaulted; } }
//    public AggregateException Exception { get { return Task.Exception; } }
//    public Exception InnerException
//    {
//        get
//        {
//            return (Exception == null) ?
//null : Exception.InnerException;
//        }
//    }
//    public string ErrorMessage
//    {
        
//        get
//        {
//            var mostInner = AsyncMVVM.MyViewModel.MostInner(InnerException);
//            return (InnerException == null) ?
//null : InnerException.Message;
//        }
//    }
//    public event PropertyChangedEventHandler PropertyChanged;
//}