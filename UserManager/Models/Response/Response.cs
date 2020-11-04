using System.Xml.Serialization;
using UserManager.DAL.Models;

namespace UserManager.Models.Response
{
    [XmlRoot("Response")]
    public class Response
    {
        [XmlElement("user")]
        public User User { get; set; }

        [XmlAttribute("Success")]
        public bool IsSuccess { get; set; }

        [XmlAttribute("ErrorId")]
        public int ErrorId { get; set; }

        [XmlElement("ErrorMsg")]
        public string ErrorMsg { get; set; }
    }
}