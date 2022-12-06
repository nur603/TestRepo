using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Redemption.Domain
{
	[XmlRoot(ElementName = "result-messages-list")]
	public class Resultmessageslist
	{

		[XmlElement(ElementName = "message-code")]
		public double Messagecode { get; set; }

		[XmlElement(ElementName = "element-number")]
		public int Elementnumber { get; set; }

		[XmlElement(ElementName = "error-message")]
		public string Errormessage { get; set; }
	}

	[XmlRoot(ElementName = "PayOrderResponse")]
	public class PayOrderResponse
	{

		[XmlElement(ElementName = "result-code")]
		public int Resultcode { get; set; }

		[XmlElement(ElementName = "result-messages-list")]
		public Resultmessageslist Resultmessageslist { get; set; }

		[XmlElement(ElementName = "our-id")]
		public double Ourid { get; set; }

		[XmlElement(ElementName = "external-id")]
		public int Externalid { get; set; }
	}

	[XmlRoot(ElementName = "MakePayOrderResponse")]
	public class MakePayOrderResponse
	{

		[XmlElement(ElementName = "PayOrderResponse")]
		public PayOrderResponse PayOrderResponse { get; set; }

		[XmlAttribute(AttributeName = "xsi")]
		public string Xsi { get; set; }

		[XmlAttribute(AttributeName = "xsd")]
		public string Xsd { get; set; }

		[XmlText]
		public string Text { get; set; }
	}
}
