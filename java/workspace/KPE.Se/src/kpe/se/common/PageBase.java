package kpe.se.common;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.interactions.Actions;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;

public abstract class PageBase {
	protected WebDriver _driver = null;
	
	public PageBase(WebDriver driver) {
		_driver = driver;
	}
	
	public abstract Boolean isLoaded();
	
	protected String getText(WebElement element) {
		return getText(element, true);
	}
	
	protected String getText(WebElement element, Boolean trim) {
		String retVal = (trim) ? element.getText().trim() : element.getText();
		return retVal;
	}
	
	protected String getText(By locator) {
		return getText(locator, true);
	}
	
	protected String getText(By locator, Boolean trim) {
		return getText(locator, trim, TimeOuts.DefaultTimeOut);
	}
	
	protected String getText(By locator, Boolean trim, long timeOut) {
		WebElement ele = waitForExists(timeOut, locator)[0];
		return getText(ele, trim);
	}
	
	protected String getInputValue(By locator) {
		return getInputValue(locator, true);
	}
	
	protected String getInputValue(By locator, Boolean trim) {
		return getInputValue(locator, true, TimeOuts.DefaultTimeOut);
	}
	
	protected String getInputValue(By locator, Boolean trim, long timeOut) {
		return getAttribute(locator, "value", trim, timeOut);
	}

	protected String getAttribute(By locator, String attribute, Boolean trim, long timeOut) {
		WebElement ele = waitForExists(timeOut, locator)[0];
		String retVal = ele.getAttribute(attribute);
		return (trim) ? retVal.trim() : retVal;
	}
	
	protected void sendKeysToMceWidget(String frameName, By locator, String text) {
		WebElement element = _driver.switchTo().frame(frameName).findElement(locator);
		sendKeys(element, text, false);
		_driver.switchTo().defaultContent();
	}
	
	protected String getTextFromMceWidget(String frameName, By mceLocator, By childTag) {
		WebElement mceElement = _driver.switchTo().frame(frameName).findElement(mceLocator);
		WebElement mceChildTag = mceElement.findElement(childTag);
		String retVal = getText(mceChildTag);
		_driver.switchTo().defaultContent();
		return retVal;
	}
	
	protected void sendKeys(By locator, String text, Boolean clearText) {
		WebElement element = findElement(locator);
		sendKeys(element, text, clearText);
	}
	
	protected void sendKeys(WebElement element, String text, Boolean clearText) {
		if(clearText) {
			element.clear();
		}
		element.sendKeys(text);
	}
	
	protected WebElement findElement(By locator) {
		return _driver.findElement(locator);
	}
	
	protected void performClick(By locator) {
		WebElement element = findElement(locator);
		performClick(element);
	}
	
	protected void performClick(WebElement element) {
		element.click();
	}
	
	protected void navigate(String url) {
		_driver.get(url);
		_driver.manage().window().maximize();
	}
	
	protected WebElement[] waitForVisible(By... locators) {
		return waitForVisible(TimeOuts.DefaultTimeOut, locators);
	}
	
	protected WebElement[] waitForVisible(long timeOutInSeconds, By... locators) {
		WebElement[] retVal = new WebElement[locators.length]; 
		WebDriverWait wait = new WebDriverWait(_driver, timeOutInSeconds);
		
		int index = 0;
		for(By locator : locators) {
			retVal[index] = wait.until(ExpectedConditions.visibilityOfElementLocated(locator));
			index += 1;
		}
		
		return retVal;
	}
	
	protected WebElement[] waitForExists(long timeOutInSeconds, By... locators) {
		WebElement[] retVal = new WebElement[locators.length]; 
		WebDriverWait wait = new WebDriverWait(_driver, timeOutInSeconds);
		
		int index = 0;
		try {
			for(By locator : locators) {
				retVal[index] = wait.until(ExpectedConditions.presenceOfElementLocated(locator));
				index += 1;
			}
		} catch (Exception e) {
			System.out.println("Failed to locate element: " + locators[index].toString());
			throw e;
		}
		
		return retVal;
	}
	
	protected Boolean tryWaitForVisible(By... locators) {
		return tryWaitForVisible(TimeOuts.DefaultTimeOut, locators);
	}
	
	protected Boolean tryWaitForVisible(long timeOutInSeconds, By... locators) {
		try {
			waitForVisible(timeOutInSeconds, locators);
			return true;
		} catch (Exception e) {
			return false;
		}
	}

	protected Boolean tryWaitForExists(By... locators) {
		return tryWaitForExists(TimeOuts.DefaultTimeOut, locators);
	}
	
	protected Boolean tryWaitForExists(long timeOutInSeconds, By... locators) {
		try {
			waitForExists(timeOutInSeconds, locators);
			return true;
		} catch (Exception e) {
			return false;
		}
	}
	
	protected Actions mouseOverElement(WebElement element) {
		Actions action = new Actions(_driver);
		action.moveToElement(element).perform();
		return action;
	}

	protected Actions mouseOverElement(By by) {
		WebElement element = findElement(by);
		return mouseOverElement(element);
	}	
	
	protected WebDriver getIFrameWebDriver(String frameName) {
		return _driver.switchTo().frame(frameName);
	}	
	
	
}
