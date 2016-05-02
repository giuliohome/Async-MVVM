using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MVVM.ViewModel;
using System.Windows.Input;

namespace AsyncMVVM
{
    public class MyViewModel : ViewModelBase
    {
        TaskScheduler GuiScheduler;
        public MyViewModel()
        {
            urlByteCountResult = -1;
            canRun = true;
        }

        //private string statusMsg;

        //public string StatusMsg
        //{
        //    get { return statusMsg; }
        //    set { statusMsg = value; OnPropertyChanged(() => StatusMsg); }
        //}


        private int urlByteCountResult;
        public int UrlByteCountResult // much more standard
        {
            get { return urlByteCountResult; }
            set { urlByteCountResult = value; OnPropertyChanged(() => UrlByteCountResult); }
        }
        private bool urlByteCountIsNotCompleted;
        public bool UrlByteCountIsNotCompleted
        {
            get { return urlByteCountIsNotCompleted; }
            set { urlByteCountIsNotCompleted = value; OnPropertyChanged(() => UrlByteCountIsNotCompleted); }
        }
        private bool urlByteCountIsFaulted;
        public bool UrlByteCountIsFaulted
        {
            get { return urlByteCountIsFaulted; }
            set { urlByteCountIsFaulted = value; OnPropertyChanged(() => UrlByteCountIsFaulted); }
        }
        private string urlByteCountErrorMessage;
        public string UrlByteCountErrorMessage
        {
            get { return urlByteCountErrorMessage; }
            set { urlByteCountErrorMessage = value; OnPropertyChanged(() => UrlByteCountErrorMessage); }
        }

        private DelegateCommand testCommand;
        public ICommand TestCommand
        {
            get
            {
                if (testCommand == null)
                {
                    testCommand = new DelegateCommand((parameter) => TestLogic(), (parameter) => CanTest());
                }
                return testCommand;
            }
        }
        bool canRun;
        public bool CanRun
        {
            get { return canRun; }
            set { canRun = value; OnPropertyChanged(() => CanRun); }
        }
        private bool CanTest()
        {
            return canRun;
        }

        private async void TestLogic()
        {
            CanRun = false;
            UrlByteCountIsNotCompleted = true;
            ((DelegateCommand)TestCommand).RaiseCanExecuteChanged();

            try
            {
                UrlByteCountResult = await MyStaticService.CountBytesInUrlAsync("http://www.example.com");
                UrlByteCountIsFaulted = false;
            }
            catch (Exception exc)
            {
                UrlByteCountIsFaulted = true;
                UrlByteCountErrorMessage = MostInner(exc).Message;
            }
            finally
            {
                CanRun = true;
                ((DelegateCommand)TestCommand).RaiseCanExecuteChanged();
                UrlByteCountIsNotCompleted = false;
            }
            
        }

        internal static Exception MostInner(Exception exc)
        {
            if (exc.InnerException != null)
            {
                return MostInner(exc.InnerException);
            }
            return exc;
        }
        //private int count = 0;
        //private async Task<int> FoundASolution()
        //{
        //    UrlByteCount = new NotifyTaskCompletion<int>(MyStaticService.ImmediateSet(-1));
        //    if (++count % 2 == 1)
        //    {
        //        Artificial delay to show responsiveness.
        //       await Task.Delay(TimeSpan.FromSeconds(3)).ConfigureAwait(false);
        //        throw new Exception("An exception! Please retry");
        //    }

        //    UrlByteCount = new NotifyTaskCompletion<int>(
        //         MyStaticService.CountBytesInUrlAsync("http://www.example.com"));

        //    return await MyStaticService.CountBytesInUrlAsync("http://www.example.com");
        //}
        //private async Task ExceptionWrapper()
        //{
        //    var start = DateTime.Now;
        //    int lenght = -1;
        //    string exc_msg = "";
        //    try
        //    {
        //        lenght = await FoundASolution();
        //    }
        //    catch (Exception exc)
        //    {
        //        exc_msg = exc.Message;
        //    }
        //    var end = DateTime.Now;
        //    int ms = (int)Math.Round((end - start).TotalMilliseconds, 0);
        //    if (lenght >= 0)
        //    {
        //        StatusMsg = "found lenght: " + lenght + " in " + ms + " ms";
        //    }
        //}

    }
}
