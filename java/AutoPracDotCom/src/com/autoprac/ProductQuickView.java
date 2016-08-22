package com.autoprac;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;

import com.autoprac.common.helpers.ElementHelpers;

public class ProductQuickView {

	private final String xPathBase = "//div[contains(@class, 'fancybox-opened')]//iframe";
	private final String xPathCenterDiv = "//div[contains(@class, 'pb-center-column')]";
	private final String xPathRightDiv = "//div[contains(@class, 'pb-right-column')]";
	private final WebDriver driver;
	private WebDriver iFrameDriver;
	
	public ProductQuickView(WebDriver driver) {
		this.driver = driver;
	}
	
	private String productName;
	public String GetProductName() {
		if(productName == null) {
			String xPath = xPathCenterDiv + "/h1";
			productName = ElementHelpers.getElementText(getIFrameDriver(), By.xpath(xPath));
		}
		return (productName == null) ? "" : productName;
	}

	private double productPrice;
	public double GetProductPrice() {
		if(productPrice <= 0) {
			String xPath = xPathRightDiv + "//span[@id='our_price_display']";
			productPrice = ElementHelpers.getNumericValueFromElement(getIFrameDriver(), By.xpath(xPath));
		}
		return productPrice;
	}
	
	public AddedToCartModal clickAddToCart() {
		getAddToCartButton().click();
		return new AddedToCartModal(driver);
	}
	
	private WebElement getAddToCartButton() {
		return ElementHelpers.getElement(getIFrameDriver(), By.xpath(xPathBase + "//div[contains(@class, 'box-cart-bottom')]//button"));
	}
	
	private WebDriver getIFrameDriver() {
		if(this.iFrameDriver == null) {
			this.iFrameDriver = ElementHelpers.getIFrameWebDriver(driver, By.xpath(xPathBase));
		}
		return this.iFrameDriver;
	}
	
	public boolean isLoaded(boolean throwEx) {
		try {
			ElementHelpers.existsAndVisible(driver, By.xpath(xPathBase), 10, throwEx);
			return true;
		} catch(Exception ex) {
			if(throwEx) {
				throw ex;
			}
		}
		return false;
	}
	
	public boolean isUnloaded() {
		try {
			new WebDriverWait(driver, 2).until(ExpectedConditions.invisibilityOfElementLocated(By.xpath(xPathBase)));
			return 	driver.findElements(By.xpath(xPathBase)).isEmpty();
		} catch(Exception ex) {
			// logging ???
		}
		return false;
	}
	
	public boolean clickClose() {
		boolean retVal = false;
		try {
			// 1. get the element
			WebElement ele = driver.findElement(By.className("fancybox-close")); 
			// 2. click close
			ele.click();
			// 3. validate modal is no longer displayed
			new WebDriverWait(driver, 5).until(ExpectedConditions.stalenessOf(ele));
			// success
			retVal = true;
		} catch(Exception ex) {
			 throw ex;
		}
		return retVal;
	}
	
	
}
