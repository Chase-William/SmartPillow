using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SmartPillow.BackgroundMsg
{
	public class StartLongRunningTaskMsg { }

    public class StopLongRunningTaskMsg { }

    public class TickedMsg
    {
        public string Msg { get; set; }
    }

    public class TickManager
    {
		public async Task RunCounter(CancellationToken token)
		{
			await Task.Run(async () => {

				for (long i = 0; i < long.MaxValue; i++)
				{
					token.ThrowIfCancellationRequested();

					await Task.Delay(250);
					var message = new TickedMsg
					{
						Msg = i.ToString()
					};

					Device.BeginInvokeOnMainThread(() => {
						MessagingCenter.Send(message, "TickedMessage");
					});
				}
			}, token);
		}		
	}
}