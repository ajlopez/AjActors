using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AjActors.WebCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string arg in args)
                MakeActor().Send(resolver => resolver.Process(arg));

            System.Console.ReadLine();
        }

        static Actor<Resolver> MakeActor()
        {
            Resolver resolver = new Resolver();
            Harvester harvester = new Harvester();
            Downloader downloader = new Downloader();

            Actor<Resolver> aresolver = new Actor<Resolver>(resolver);
            Actor<Harvester> aharvester = new Actor<Harvester>(harvester);
            Actor<Downloader> adownloader = new Actor<Downloader>(downloader);

            resolver.Downloader = adownloader;
            harvester.Resolver = aresolver;
            downloader.Harvester = aharvester;

            return aresolver;
        }
    }
}
