package kpe.se.wordpress.tests;

//import java.io.ByteArrayInputStream;
//import java.io.IOException;
//import java.io.InputStream;
//import java.io.UnsupportedEncodingException;
//import java.util.Arrays;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

/*
import javax.xml.parsers.DocumentBuilder;
import javax.xml.parsers.DocumentBuilderFactory;
import javax.xml.parsers.ParserConfigurationException;
import javax.xml.xpath.XPath;
import javax.xml.xpath.XPathConstants;
import javax.xml.xpath.XPathExpressionException;
import javax.xml.xpath.XPathFactory;
*/

import org.testng.Assert;
import org.testng.annotations.Test;

/*
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.Node;
import org.w3c.dom.NodeList;
import org.xml.sax.SAXException;

import bsh.Console;
*/

public class PractiseTests {

	@Test
	public void jmeterTests() //throws ParserConfigurationException, SAXException, IOException//, XPathExpressionException
	{
		String numbers = " 33 77 132 210 180 ";
		String[] numbersArr = numbers.trim().split(" ");
		
		if("challenge_account_145".startsWith("challenge_account")) {
			
		}
		
		
		/*log.info("number:");
		for(int i=0; i<numbersArr.length; i++) {
			//log.info(numbersArr[i].toString());
		}
		*/

		ArrayList<Integer> test = new ArrayList<Integer>();
		ArrayList<Integer> listInt = new ArrayList<Integer>();
		for(String key : numbersArr) {
			listInt.add(Integer.parseInt(key));
		}
		
		Collections.sort(listInt);
		
		int a = listInt.get(0);
		
		return;
		
		/*
		DocumentBuilderFactory documentBuildFactory = DocumentBuilderFactory.newInstance();
		DocumentBuilder doccumentBuilder = documentBuildFactory.newDocumentBuilder();
		Document document = 
		 doccumentBuilder.parse(new ByteArrayInputStream("<name>Oscar</name>".getBytes()));
		
		String xml = "<table><tr><td><button><i class='glyphicon glyphicon-remove'></i></button></td></tr><tr><td><button><i class='glyphicon glyphicon-remove'></i></button></td></tr><tr><td><button><i class='glyphicon glyphicon-ok'></i></button></td></tr></table>";
		
        Document xmlDoc = getXmlDocumentFromString(xml);		
        XPath xPath =  XPathFactory.newInstance().newXPath();
        String expression = "//i[contains(@class, 'glyphicon-ok')]/preceding::i";
        
        // Query the table for a match on 'glyphicon-ok' from there return all sibling nodes
        // By doing this we can determine the index of the node we found
        NodeList nodeList =
        		(NodeList)xPath.compile(expression).evaluate(xmlDoc, XPathConstants.NODESET);
        
        int nodeIndex = nodeList.getLength() + 1; 
        

        // Lookup the correct input and extract its 'name' attribute
        String inputName = null;
        xml = "<table><tr><td><input name='5EOg4n77PF' type='text' value=''></td></tr><tr><td><input type='text' value='' name='KPfldLisoD'></td></tr><tr><td><input value='' name='fU7AnwWfCl' type='text'></td></tr></table>";
        xml = xml.replace("</td></tr>", "</input></td></tr>");
        xmlDoc = getXmlDocumentFromString(xml);		
        expression = String.format("//tr[%s]//input", nodeIndex);
        Element element = (Element) xPath.compile(expression).evaluate(xmlDoc, XPathConstants.NODE);
        inputName = element.getAttribute("name");
        
        System.out.println("inputName:");
        System.out.println(inputName);
        

        //read a string value
        //String email = xPath.compile(expression).evaluate(xmlDocument);
	
        //read an xml node using xpath
        //Node node = (Node) xPath.compile(expression).evaluate(xmlDocument, XPathConstants.NODE);
	
        //read a nodelist using xpath
        //NodeList nodeList = (NodeList) xPath.compile(expression).evaluate(xmlDocument, XPathConstants.NODESET);        

        
        
        //int index = _driver.findElements(By.xpath("//i[contains(@class, 'glyphicon-ok')]/preceding::i")).size() + 1;
		
        
        
        //doc.get
        
		Assert.assertTrue(true, "True test");
		*/
	}
	
	/*
	public static Document getXmlDocumentFromString(String xml) throws ParserConfigurationException, SAXException, IOException
	{
        DocumentBuilderFactory dbf = DocumentBuilderFactory.newInstance();
        DocumentBuilder docBuilder = dbf.newDocumentBuilder();
        InputStream is = new ByteArrayInputStream(xml.getBytes()); //("UTF-8"));
        return docBuilder.parse(is);		
	}
	*/
	
}
