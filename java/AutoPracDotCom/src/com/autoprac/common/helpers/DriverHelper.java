package com.autoprac.common.helpers;

import org.openqa.selenium.WebDriver;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.ie.InternetExplorerDriver;
import org.testng.annotations.Optional;

public final class DriverHelper {

	private DriverHelper() {}
	
	public static WebDriver getDriver()
	{
		WebDriver driver = new FirefoxDriver();
		driver.manage().window().maximize();
		return driver;	
	}
	
	public static void dispose(WebDriver driver)
	{
		if(driver != null)
		{
			driver.close();
			driver.quit();
		}
	}
	
	public static WebDriver createDriver(@Optional("FF") String browserType) {
		if(browserType.equalsIgnoreCase("chrome")) {
			System.setProperty("webdriver.chrome.driver", "{file path to chrome}");
			return new ChromeDriver();
		} else if(browserType.equalsIgnoreCase("FF")) {
			return new FirefoxDriver();
		} else {
			return new InternetExplorerDriver();
		}
	}
	
}
