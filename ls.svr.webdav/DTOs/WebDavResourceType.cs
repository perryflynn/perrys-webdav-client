using System.Xml.Serialization;

namespace ls.svr.webdav.DTOs
{
    public class WebDavResourceType
    {
        [XmlElement("collection", Namespace = "DAV:")]
        public string? Collection { get; set; }
    }
}
