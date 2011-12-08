namespace AjActors.WebCrawler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Globalization;

    public class Resolver
    {
        private List<Uri> downloadedAddresses = new List<Uri>();

        public Actor<Downloader> Downloader { get; set; }

        public void Process(DownloadTarget target)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            Console.WriteLine("[Resolver] processing " + target.Target.ToString());

            if (target.Depth > 5)
            {
                Console.WriteLine(
                   string.Format(CultureInfo.InvariantCulture, "URL rejected {0} by max depth", target.TargetAddress));
                return;
            }

            if ((target.Target.Scheme != Uri.UriSchemeHttp) &&
                (target.Target.Scheme != Uri.UriSchemeHttps))
            {
                Console.WriteLine(
                    string.Format(CultureInfo.InvariantCulture, "URL rejected {0}: unsupported protocol", target.TargetAddress));
                return;
            }

            if (target.Referrer != null &&
                target.Target.Host != target.Referrer.Host)
            {
                Console.WriteLine(
                    string.Format(CultureInfo.InvariantCulture, "URL rejected {0}: different host", target.TargetAddress));
                return;
            }

            if (this.downloadedAddresses.Contains(target.Target))
            {
                Console.WriteLine(
                    string.Format(CultureInfo.InvariantCulture, "URL rejected {0}: already downloaded", target.TargetAddress));
            }
            else
            {
                this.downloadedAddresses.Add(target.Target);

                this.Downloader.Send(downloader => downloader.Download(target));

                Console.WriteLine(
                    string.Format(CultureInfo.InvariantCulture, "URL accepted: {0}", target.TargetAddress));
            }
        }

        public void Process(string partialUri)
        {
            Uri url;

            Console.WriteLine("[Resolver] Dispatching " + partialUri);

            if (!Uri.TryCreate(partialUri, UriKind.Absolute, out url))
                throw new ArgumentException("Invalid Message Format");

            DownloadTarget target = new DownloadTarget(url, 1);

            this.Process(target);
        }
    }
}
