package com.autoprac;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;

public class AddedToCartModal {

	private final String xPathBase = "//div[@id='layer_cart']";
	
	private final WebDriver driver;
	
	public AddedToCartModal(WebDriver driver) {
		this.driver = driver;
	}
	
	public boolean isLoaded(Boolean throwEx) {
		try {
			return 	getBaseElement(10) != null &&
					isContinueShoppingButtonVisible(true) && 
					isProceedToCheckoutButtonVisible(true) &&
					isCloseButtonVisible(true);
			
		} catch(Exception ex) {
			System.out.println("AddedToCartModal.isLoaded()");
			System.out.println(ex.toString());
			if(throwEx) {
				throw ex;
			}
		}
		return false;
	}
	
	public boolean isUnloaded() {
		try {
			new WebDriverWait(driver, 3).until(ExpectedConditions.invisibilityOfElementLocated(By.xpath(xPathBase)));
			return driver.findElement(By.xpath(xPathBase)).isDisplayed() == false;
		} catch(Exception ex) {
			// logging ???
		}
		return false;
	}

	private WebElement getBaseElement(long timeOutInSeconds) {
		new WebDriverWait(driver, timeOutInSeconds).until(ExpectedConditions.visibilityOfElementLocated(By.xpath(xPathBase)));
		WebElement retVal = driver.findElement(By.xpath(xPathBase));
		//System.out.println("AddedToCartModal.getBaseElement(): Element found: " + (retVal != null));
		return retVal;
	}
	
	private WebElement getProceedToCheckoutElement() {
		return driver.findElement(By.xpath(xPathBase + "//a[contains(@title, 'Proceed to checkout')]")); 
	}

	public boolean clickClose(boolean throwEx) {
		try {
			getCloseElement().click();
			return true;
		} catch(Exception ex) {
			if(throwEx) {
				throw ex;
			}
		}
		return false;
	}
	
	public boolean isCloseButtonVisible(boolean throwEx) {
		try {
			boolean isDisplayed = getCloseElement().isDisplayed();
			//System.out.println("isCloseButtonVisible: " + isDisplayed);
			return isDisplayed;
		} catch(Exception ex) {
			if(throwEx) {
				throw ex;
			}
		}
		return false;
	}
	
	private WebElement getCloseElement() {
		return driver.findElement(By.xpath(xPathBase + "//span[contains(@class, 'cross')]")); 
	}
	
	public boolean isProceedToCheckoutButtonVisible(boolean throwEx) {
		try {
			boolean isDisplayed = getProceedToCheckoutElement().isDisplayed();
			//System.out.println("isProceedToCheckoutButtonVisible: " + isDisplayed);
			return isDisplayed;
		} catch(Exception ex) {
			if(throwEx) {
				throw ex;
			}
		}
		return false;
	}
	
	private WebElement getContinueShoppingElement() {
		String xPath = xPathBase + "//span[contains(@class, 'continue')]/span";
		new WebDriverWait(driver, 3).until(ExpectedConditions.visibilityOfElementLocated(By.xpath(xPathBase)));
		return driver.findElement(By.xpath(xPath)); 
	}
	
	public boolean isContinueShoppingButtonVisible(boolean throwEx) {
		try {
			WebElement ele = getContinueShoppingElement();
			//System.out.println("isContinueShoppingButtonVisible: ele == null: " + (ele == null));
			boolean isDisplayed = ele.isDisplayed();
			//System.out.println("isContinueShoppingButtonVisible: isDisplayed: " + isDisplayed);
			return isDisplayed;
		} catch(Exception ex) {
			if(throwEx) {
				throw ex;
			}
		}
		return false;
	}
	
	
}
