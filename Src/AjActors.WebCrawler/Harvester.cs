namespace AjActors.WebCrawler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Globalization;

    public class Harvester
    {
        public Actor<Resolver> Resolver { get; set; }

        public void HarvestLinks(DownloadTarget target)
        {
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            Console.WriteLine("[Harvester] Processing " + target.Target.ToString());

            IEnumerable<string> links = HarvestUrls(target.Content);
            foreach (string link in links)
            {
                Uri uri = new Uri(link, UriKind.RelativeOrAbsolute);
                if (!uri.IsAbsoluteUri)
                    uri = new Uri(target.Target, uri);

                DownloadTarget newTarget = new DownloadTarget(uri, target.Depth + 1);
                newTarget.Referrer = target.Target;
                this.Resolver.Send(resolver => resolver.Process(newTarget));

                Console.WriteLine(
                    string.Format(CultureInfo.InvariantCulture, "Url To Harvest {0} {1}", newTarget.Depth, newTarget.TargetAddress));
            }

            Console.WriteLine("[Harvester] Processed " + target.Target.ToString());
        }

        private static List<string> HarvestUrls(string content)
        {
            string regexp = @"href=\s*""([^&""]*)""";

            MatchCollection matches = Regex.Matches(content, regexp);
            List<string> links = new List<string>();

            foreach (Match m in matches)
            {
                if (!links.Contains(m.Groups[1].Value))
                {
                    links.Add(m.Groups[1].Value);
                }
            }

            return links;
        }
    }
}
