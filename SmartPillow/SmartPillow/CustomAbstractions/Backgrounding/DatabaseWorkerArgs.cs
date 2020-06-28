using SmartPillowLib.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
// https://devblogs.microsoft.com/xamarin/getting-started-workmanager/
namespace SmartPillow.Backgrounding
{
	/// <summary>
	///		Event args for the messaging center.
	/// </summary>
	public class DatabaseWorkerArgs : EventArgs
	{
		/// <summary>
		///		Constructor that initializes all its fields based off parameters.<br/>
		///		@param - action, action to be executed<br/>
		///		@param - alarm, target item of this event<br/>
		///		@param - collection, discriminator for the database to know which collection this alarm belongs to
		/// </summary>
		public DatabaseWorkerArgs(Action<Alarm, string> action, Alarm alarm, string collection)
		{
			Action = action;
			Alarm = alarm;
			Collection = collection;
		}

		public Action<Alarm, string> Action { get; set; }
		public Alarm Alarm { get; set; }
		public string Collection { get; set; }
	}

	//public class StartLongRunningTaskMsg { }

 //   public class StopLongRunningTaskMsg { }

 //   public class TickedMsg
 //   {
 //       public string Msg { get; set; }
 //   }

 //   public class TickManager
 //   {
	//	public async Task RunCounter(CancellationToken token)
	//	{
	//		await Task.Run(async () => {

	//			for (long i = 0; i < long.MaxValue; i++)
	//			{
	//				token.ThrowIfCancellationRequested();

	//				await Task.Delay(250);
	//				var message = new TickedMsg
	//				{
	//					Msg = i.ToString()
	//				};

	//				Device.BeginInvokeOnMainThread(() => {
	//					MessagingCenter.Send(message, "TickedMessage");
	//				});
	//			}
	//		}, token);
	//	}		
	//}
}