#define SHOWINBROWSER

namespace FlightControl.Manager
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Runtime.Serialization;

    using FlightControl.Core;
    using FlightControl.External;
    using FlightControl.Manager.Properties;
    using FlightControl.Model;

    class Program
    {
        static void Main(string[] args)
        {
            // Set in case of existing session
            const string Token = "";

            try
            {
                // Create proxy
                var proxy = new FlightControlProxy(Settings.Default.BaseUrl);

                // Create context basing on input data
                var existingSession = !string.IsNullOrEmpty(Token);
                var context = existingSession
                    ? new FlightContext(proxy, new SessionInfo { Token = Token })
                    : new FlightContext(proxy);
                var tower = new FlightControlTower(context);

                // Show the view in browser
                ShowInBrowser(context);

                // Run the process
                tower.Run();
            }
            catch (WebException)
            {
                // Token has expired or the external service is down
            }
            catch (SerializationException)
            {
                // Token has expired or incorrect dataformat
            }
            

#if DEBUG
            Console.ReadLine();
#endif
        }

        private static void ShowInBrowser(FlightContext context)
        {
#if SHOWINBROWSER
            Process.Start("chrome", Settings.Default.BaseUrl + "/view?token=" + context.Session.Token);
#endif
        }
    }
}
