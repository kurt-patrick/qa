package com.autoprac.common.helpers;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.interactions.Actions;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;

public final class ElementHelpers {
	
	public static void pressKeys(WebElement element, String value, boolean clearText) {
		if(clearText) {
			element.clear();
		}
		element.sendKeys(value);
	}

	public static Actions mouseOverElement(WebDriver driver, WebElement element) {
		Actions action = new Actions(driver);
		try {
			action.moveToElement(element).perform();
			return action;
		} catch(Exception ex) {
			 throw ex;
		}
	}

	public static Actions mouseOverElement(WebDriver driver, By by) {
		try {
			return mouseOverElement(driver, getElement(driver, by));
		} catch(Exception ex) {
			 throw ex;
		}
	}
	
	public static String getIFrameName(WebDriver driver, By by) {
		return getElementAttribute(driver, by, "name");
	}

	public static WebDriver getIFrameWebDriver(WebDriver driver, String frameName) {
		return driver.switchTo().frame(frameName);
	}
	
	public static WebDriver getIFrameWebDriver(WebDriver driver, By by) {
		String frameName = getIFrameName(driver, by);
		return getIFrameWebDriver(driver, frameName);
	}

	public static String getElementText(WebDriver driver, By by) {
		return getElement(driver, by, true, true, 0).getText().trim();
	}
	
	public static String getElementAttribute(WebDriver driver, By by, String attributeName) {
		return getElement(driver, by, true, true, 0).getAttribute(attributeName);
	}

	public static WebElement getElement(WebDriver driver, By by) {
		return getElement(driver, by, true, true, 0);
	}

	public static WebElement getElement(WebDriver driver, By by, int timeOutInSeconds) {
		return getElement(driver, by, true, true, timeOutInSeconds);
	}
	
	public static boolean existsAndVisible(WebDriver driver, By by, int timeOutInSeconds, boolean throwEx) {
		try {
			new WebDriverWait(driver, timeOutInSeconds).until(ExpectedConditions.visibilityOfElementLocated(by));
			return true;
		} catch (Exception ex) {
			System.out.println(ex.toString());
			if(throwEx) {
				throw ex;
			}
		}
		return false;
	}
	
	public static WebElement getElement(WebDriver driver, By by, boolean logEx, boolean throwEx) {
		return getElement(driver, by, logEx, throwEx, 0);
	}
	
	public static WebElement getElement(WebDriver driver, By by, boolean logEx, boolean throwEx, int timeOutInSeconds) {
		try {
			if(timeOutInSeconds > 0) {
				existsAndVisible(driver, by, timeOutInSeconds, true);
			}
			return driver.findElement(by);
		} catch (Exception ex) {
			if(logEx) {
				System.out.println(ex.toString());
			}
			if(throwEx) {
				throw ex;
			}
		}
		return null;
	}
	
	public static double getNumericValueFromElement(WebDriver driver, By by) {
		double retVal = 0;
		String theText = null;
		try {
			theText = getElementText(driver, by);
			theText = StringHelpers.removeCurrencySymbol(theText, false);
			// skip if the string is empty
			if(theText.length() > 0) {
				retVal = Double.parseDouble(theText);
			}
		} catch (NumberFormatException ex) {
			System.out.println("theText: " + theText);
			throw ex;
		}
		return retVal;
	}
	
}
