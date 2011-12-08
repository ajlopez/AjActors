namespace AjActors.WebCrawler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class DownloadTarget
    {
        private Uri target;
        private int depth;
        private string content;

        public DownloadTarget(Uri target, int depth)
        {
            if (target == null)
                throw new ArgumentNullException("target");

            if (depth < 1)
                throw new ArgumentOutOfRangeException("depth");

            this.target = target;
            this.depth = depth;
        }

        public Uri Target { get { return this.target; } }

        public string TargetAddress
        {
            get { return Uri.EscapeUriString(this.target.ToString()); }
            set { this.target = new Uri(value, UriKind.RelativeOrAbsolute); }
        }

        public Uri Referrer { get; set; }

        public string ReferrerAddress
        {
            get
            {
                if (this.Referrer == null)
                {
                    return null;
                }

                return Uri.EscapeUriString(this.Referrer.ToString());
            }

            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.Referrer = new Uri(value, UriKind.RelativeOrAbsolute);
                }
            }
        }

        public int Depth
        {
            get { return this.depth; }
            set { this.depth = value; }
        }

        public string Content
        {
            get { return this.content; }
            set { this.content = value; }
        }
    }
}
