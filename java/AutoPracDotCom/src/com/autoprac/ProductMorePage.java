package com.autoprac;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;

public class ProductMorePage {

	private final WebDriver driver;
	
	public ProductMorePage(WebDriver driver) {
		this.driver = driver;
	}
	
	public boolean isLoaded() {
		try {
//			return 	isAddToCartATagVisible() && 
//					isMoreATagVisible() &&
//					isQuickViewVisible();
			
		} catch(Exception ex) {
			// logging ???
		}
		return false;
	}
	
	public boolean isUnloaded() {
		try {
//			new WebDriverWait(driver, 1).until(ExpectedConditions.invisibilityOfElementLocated(By.xpath(xPathBase)));
//			return 	driver.findElements(By.xpath(xPathBase)).isEmpty();
		} catch(Exception ex) {
			// logging ???
		}
		return false;
	}
	
}
