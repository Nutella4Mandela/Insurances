using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
public class InsuranceDefiner
{
    [XmlAttribute(AttributeName = "Name")]
    public string InsName { get; set; }
    
    [XmlAttribute(AttributeName = "Partnered")]
    public int Partnered { get; set; }
    
    [XmlAttribute(AttributeName = "Referral")]
    public int Referral { get; set; }
    
    [XmlAttribute(AttributeName = "Plans")]
    public int Plans { get; set; }
    
    [XmlAttribute(AttributeName = "Requirements")]
    public string Requirements { get; set; }
}

