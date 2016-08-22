package com.autoprac.common.helpers;

import java.io.File;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.GregorianCalendar;

import org.openqa.selenium.OutputType;
import org.openqa.selenium.TakesScreenshot;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.remote.Augmenter;

import com.sun.jna.platform.FileUtils;

public final class ReportingHelper {

	public static boolean takeScreenShot(WebDriver driver) throws IOException {
		
        // RemoteWebDriver does not implement the TakesScreenshot class
        // if the driver does have the Capabilities to take a screenshot
        // then Augmenter will add the TakesScreenshot methods to the instance
        WebDriver augmentedDriver = new Augmenter().augment(driver);
        File screenshotFile = ((TakesScreenshot)augmentedDriver).
                            getScreenshotAs(OutputType.FILE);		
        String filePath = new SimpleDateFormat("yyyy-mm-dd-HH-MM-ss").format(new GregorianCalendar().getTime()) + ".png";
        File imageFile = new File(filePath);
        org.apache.commons.io.FileUtils.moveFile(screenshotFile, imageFile);
        return true;
	}
	
}
