package jmeterjunit;

import java.awt.image.BufferedImage;
import java.awt.image.DataBuffer;

import javax.imageio.ImageIO;

import org.testng.annotations.Test;

import java.io.File;
import java.io.IOException;
import java.net.URL;
import java.nio.file.Files;
import java.nio.file.LinkOption;
import java.nio.file.Path;
import java.nio.file.Paths;
 
public class ImgDiffPercent
{
	@Test
	public void testODBC() {
		
		java.util.Date dateToday = new java.util.Date();
		java.text.SimpleDateFormat dateFormat = new java.text.SimpleDateFormat("dd-MMM-yyyy");
		String serviceStartDate = dateFormat.format(dateToday);
		
		
	    try {
	        Class.forName("sun.jdbc.odbc.JdbcOdbcDriver");
	        System.out.println("sun.jdbc.odbc.JdbcOdbcDriver found");
	      } catch (ClassNotFoundException cnfe) {
	        System.out.println("Error: sun.jdbc.odbc.JdbcOdbcDriver not found");
	      }		
	}
	
	//@Test
	public void testCompare03() {
		  String actual = "{\"coord\":{\"lon\":-0.13,\"lat\":51.51}";
		  String expected = "{\"coord\":{\"lon\":-0.13,\"lat\":51.51}";
		  
		  boolean result = actual.contains(expected);
		  if(result) {
			  int index = 0;
			  for(char ch : expected.toCharArray()) {
				  result = ch == actual.charAt(index);
				  if(!result) {
					  String message = "Dont match " + ch + " " + actual.charAt(index); 
					  break;
				  }
				  index += 1;
			  }
			  actual.toCharArray();
			  
		  }
		
	}
	
	//@Test
	  public void testCompare02()
	  {
		  
	  switch("a") {
	  case "b":
		  break;
	  case "c":
		  break;
	  default:
	  }
		  
	  try {
		  
		  
		  String actual = "actual";
		  String expected = "expected";
		  
		  boolean result = actual.contains(expected);
		  if(result) {
			  
		  }
		  
		String contents = new String(Files.readAllBytes(Paths.get("manifest.mf")));
	} catch (IOException e1) {
		// TODO Auto-generated catch block
		e1.printStackTrace();
	}
		  
    	File fileA = new File("d:/black.jpg");
    	File fileB = new File("d:/Lenna50.jpg");
    	
	    float percentage = 0;
	    try {
	        // take buffer data from both image files //
	        BufferedImage biA = ImageIO.read(fileA);
	        DataBuffer dbA = biA.getData().getDataBuffer();
	        int sizeA = dbA.getSize();
	        BufferedImage biB = ImageIO.read(fileB);
	        DataBuffer dbB = biB.getData().getDataBuffer();
	        int sizeB = dbB.getSize();
	        int count = 0;
	        // compare data-buffer objects //
	        if (sizeA == sizeB) {

	            for (int i = 0; i < sizeA; i++) {

		            //System.out.println("dbA.getElem(i): " + dbA.getElem(i));
	                if (dbA.getElem(i) == dbB.getElem(i)) {
	                    count = count + 1;
	                }

	            }
	            percentage = (float) (count * 100) / sizeA;
	        } else {
	            System.out.println("Both the images are not of same size");
	        }

	    } catch (Exception e) {
	        System.out.println("Failed to compare image files ...");
	    }
	    
	    percentage = percentage;
	    System.out.println("percentage: " + (percentage));
	    
    }
	
	
	//@Test()
  public void testCompare()
  {
    BufferedImage img1 = null;
    BufferedImage img2 = null;
    try {
    	//File url1 = new File("d:/Lenna50.jpg");
    	File url1 = new File("d:/black.jpg");
    	File url2 = new File("d:/white.jpg");
        
        /*
      URL url1 = new URL("http://rosettacode.org/mw/images/3/3c/Lenna50.jpg");
      URL url2 = new URL("http://rosettacode.org/mw/images/b/b6/Lenna100.jpg");
      */
      img1 = ImageIO.read(url1);
      img2 = ImageIO.read(url2);
    } catch (IOException e) {
      e.printStackTrace();
    }
    int width1 = img1.getWidth(null);
    int width2 = img2.getWidth(null);
    int height1 = img1.getHeight(null);
    int height2 = img2.getHeight(null);
    if ((width1 != width2) || (height1 != height2)) {
      System.err.println("Error: Images dimensions mismatch");
      System.exit(1);
    }
    long diff = 0;
    for (int y = 0; y < height1; y++) {
      for (int x = 0; x < width1; x++) {
        int rgb1 = img1.getRGB(x, y);
        int rgb2 = img2.getRGB(x, y);
        int r1 = (rgb1 >> 16) & 0xff;
        int g1 = (rgb1 >>  8) & 0xff;
        int b1 = (rgb1      ) & 0xff;
        int r2 = (rgb2 >> 16) & 0xff;
        int g2 = (rgb2 >>  8) & 0xff;
        int b2 = (rgb2      ) & 0xff;
        diff += Math.abs(r1 - r2);
        diff += Math.abs(g1 - g2);
        diff += Math.abs(b1 - b2);
      }
    }
    double n = width1 * height1 * 3;
    double p = diff / n / 255.0;
    System.out.println("diff percent: " + (p * 100.0));
  }
}