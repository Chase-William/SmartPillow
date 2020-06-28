//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Support.V4.App;
//using SmartPillow.BackgroundMsg;
//using System;
//using System.Threading;
//using System.Threading.Tasks;
//using Xamarin.Forms;

//namespace SmartPillow.Droid.Service
//{	
//	[Service]
//	public class LongRunningTaskService : IntentService
//	{
//		CancellationTokenSource _cts;

//		public override IBinder OnBind(Intent intent)
//		{
//			return null;
//		}

//		public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
//		{
//			_cts = new CancellationTokenSource();

//			Task.Run(() => {
//				try
//				{
//					//INVOKE THE SHARED CODE

//					var counter = new TickManager();
//					counter.RunCounter(_cts.Token).Wait();
//				}
//				catch (Android.Accounts.OperationCanceledException)
//				{
//				}
//				finally
//				{
//					if (_cts.IsCancellationRequested)
//					{
//						var message = new CancelledMessage();
//						Device.BeginInvokeOnMainThread(
//							() => MessagingCenter.Send(message, "CancelledMessage")
//						);
//					}
//				}

//			}, _cts.Token);

//			return StartCommandResult.Sticky;
//		}

//		public override void OnDestroy()
//		{
//			if (_cts != null)
//			{
//				_cts.Token.ThrowIfCancellationRequested();

//				_cts.Cancel();
//			}
//			base.OnDestroy();
//		}

//		protected override void OnHandleIntent(Intent intent)
//		{
//			throw new System.NotImplementedException();
//		}
//	}

//	public class CancelledMessage { }
//}