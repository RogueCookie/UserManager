using System.Xml.Serialization;
using UserManager.DAL.Enum;

namespace UserManager.DAL.Models
{
    public class User
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        [XmlAttribute("Name")]
        public string UserName { get; set; }

        /// <summary>
        /// Hold current user status
        /// </summary>
        [XmlElement("Status")]
        public UserStatus UserStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [XmlIgnore]
        public string Password { get; set; }

    }
}