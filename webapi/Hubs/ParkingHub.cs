using Microsoft.AspNetCore.SignalR;

namespace webapi.Hubs
{
    public class ParkingHub : Hub
    {
        /*private ParkingiController parkingiController;

        public ParkingHub(ParkingiController parkingi)
        {
            parkingiController = parkingi;
        }*/
        public async Task UpdateLayout(string value)
        {
            //await parkingiController.PutParkingSpot(Guid.Parse(value));
            Console.WriteLine("Sending a layout update");
            await Clients.All.SendAsync("LayoutUpdate", value);
        }
    }
}
