using Android.App;
using Android.Content;
using AndroidX.Work;
using SmartPillow.Droid.Locals.SmartPillowAlarm;
using Xamarin.Forms;

// https://developer.android.com/training/run-background-service/create-service
namespace SmartPillow.Droid.Locals.Backgrounding
{   
    public class DatabaseWorker : Worker
    {
        public DatabaseWorker(Context context, WorkerParameters workerParameters) : base(context, workerParameters) { }

        public override Result DoWork()
        {
            Android.Util.Log.Debug("CalculatorWorker", "reeeeeeeeeeeeeeeeeeeee");          
            return Result.InvokeSuccess();
        }
    }
}