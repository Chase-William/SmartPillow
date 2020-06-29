using Android.App;
using Android.Content;
using AndroidX.Work;
using SmartPillow.Backgrounding;
using Xamarin.Forms.Platform.Android;
using static SmartPillow.Droid.MainActivity;

// https://developer.android.com/training/run-background-service/create-service
namespace SmartPillow.Droid.Locals.Backgrounding
{   
    public class DatabaseWorker : Worker
    {
        public DatabaseWorker(Context context, WorkerParameters workerParameters) : base(context, workerParameters) { }
        
        public override Result DoWork()
        {
            if (InputData.KeyValueMap.ContainsKey("action"))
            {
                var alarmWrapper = (AndroidAlarmWrapper)InputData.KeyValueMap["action"];
                var test = alarmWrapper.Alarm;
            }            
            return Result.InvokeSuccess();
        }
    }
}