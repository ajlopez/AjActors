namespace AjActors.WebCrawler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Net;
    using System.Globalization;

    public class Downloader
    {
        public Actor<Harvester> Harvester { get; set; }

        public void Download(DownloadTarget target)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            Console.WriteLine("[Downloader] Processing " + target.Target.ToString());

            try
            {
                WebClient client = new WebClient();
                target.Content = client.DownloadString(target.Target);

                Console.WriteLine(
                    string.Format(CultureInfo.InvariantCulture, "URL {0} downloaded", target.TargetAddress));
            }
            catch (System.Net.WebException)
            {
                Console.WriteLine(
                   string.Format(
                   CultureInfo.InvariantCulture,
                   "URL could not be downloaded",
                   target.TargetAddress));

                return;
            }

            this.Harvester.Send(harvester => harvester.HarvestLinks(target));

            Console.WriteLine("[Downloader] Processed " + target.Target.ToString());
        }
    }
}
