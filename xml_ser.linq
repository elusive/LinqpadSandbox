<Query Kind="Program" />

void Main()
{
	var el = new Element() {
		param = "elvis",
		set = "foo"
	};
	
	var xs = new System.Xml.Serialization.XmlSerializer(typeof(Element));
	var sw = new StringWriter();
	xs.Serialize(sw, el);
	sw.ToString().Dump();
}

// Define other methods and classes here
[System.Xml.Serialization.XmlType("element")]
public class Element
{
  [System.Xml.Serialization.XmlElement(IsNullable = true)]
  public string param { get; set; }
  [System.Xml.Serialization.XmlElement(IsNullable = true)]
  public string set { get; set; }
  
  [System.Xml.Serialization.XmlAttribute("param")]
  public string paramAttr { get; set; }
  [System.Xml.Serialization.XmlAttribute("set")]
  public string setAttr { get; set; }
}