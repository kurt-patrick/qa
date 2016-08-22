package com.autoprac;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;

import com.autoprac.common.helpers.ElementHelpers;

public class ProductMouseOver {

	private final String xPathBase;
	private final WebDriver driver;
	
	public ProductMouseOver(WebDriver driver, String xPathBase) {
		this.driver = driver;
		this.xPathBase = xPathBase;
	}
	
	public boolean isLoaded(Boolean throwEx) {
		try {
			// NOTE: Add to Cart button is only enabled/exists if stock exists 
			return 	(isAddToCartATagVisible() || isAddToCartSpanTagVisible()) && 
					(isMoreATagVisible() && isQuickViewSpanVisible());
			
		} catch(Exception ex) {
			if(throwEx) {
				throw ex;
			}
		}
		return false;
	}
	
	public boolean isUnloaded() {
		try {
			new WebDriverWait(driver, 1).until(ExpectedConditions.invisibilityOfElementLocated(By.xpath(xPathBase)));
			return 	driver.findElements(By.xpath(xPathBase)).isEmpty();
		} catch(Exception ex) {
			// logging ???
		}
		return false;
	}
	
	public boolean isAddToCartATagVisible() {
		try {
			return getAddToCartATag().isDisplayed();
		} catch(Exception ex) {
			System.out.println("ProductMouseOver.isAddToCartATagVisible(): Failed to load element: " + ex.toString());
		}
		return false;
	}

	public boolean isAddToCartSpanTagVisible() {
		try {
			return getAddToCartSpanTag().isDisplayed();
		} catch(Exception ex) {
			System.out.println("ProductMouseOver.isAddToCartSpanTagVisible(): Failed to load element: " + ex.toString());
		}
		return false;
	}
	
	public boolean isAddToCartEnabled() {
		try {
			return getAddToCartATag().isDisplayed();
		} catch(Exception ex) {
			//System.out.println("Failed to load Add to cart element: " + ex.toString());
		}
		return false;
	}
	
	private WebElement getAddToCartATag() {
		// Add to cart element can be in 2 potential states
		// 1. Enabled  - users are able to add to cart 					- in this case an ATag exists
		// 2. Disabled - users cannot add to cart - item has sold out   - in this case a SpanTag exists
		
		String xPath = this.xPathBase + "//a[contains(@class, 'ajax_add_to_cart_button')]";
		return ElementHelpers.getElement(driver, By.xpath(xPath), false, false); 
	}

	private WebElement getAddToCartSpanTag() {
		// Add to cart element can be in 2 potential states
		// 1. Enabled  - users are able to add to cart 					- in this case an ATag exists
		// 2. Disabled - users cannot add to cart - item has sold out   - in this case a SpanTag exists
		String xPath = this.xPathBase + "//span[contains(@class, 'ajax_add_to_cart_button')]/span";
		return ElementHelpers.getElement(driver, By.xpath(xPath), false, false);
	}
	
	private WebElement getMoreATag() {
		return driver.findElement(By.xpath(this.xPathBase + "//a/span[text()='More']/..")); 
	}
	
	public boolean isMoreATagVisible() {
		try {
			return getMoreATag().isDisplayed(); 
		} catch(Exception ex) {
			System.out.println("Failed to load getMoreATag()");
			System.out.println(ex.toString());
		}
		return false;
	}
	
	public ProductMorePage clickMore(boolean mouseOver) {
		getMoreATag().click();
		return new ProductMorePage(driver);
	}

	public AddedToCartModal clickAddToCart() {
		getAddToCartATag().click();
		return new AddedToCartModal(driver);
	}
	
	public ProductQuickView clickQuickView(boolean throwEx) {
		try {
			getQuickViewSpan().click();
			return new ProductQuickView(driver);
		} catch(Exception ex) {
			if(throwEx) {
				throw ex;
			}
		}
		return null;
	}

	private WebElement getQuickViewSpan() {
		return this.driver.findElement(By.xpath(xPathBase + "//a[contains(@class, 'quick-view')]/span"));
	}
	
	public boolean isQuickViewSpanVisible() {
		try {
			return getQuickViewSpan().isDisplayed(); 
		} catch(Exception ex) {
			System.out.println("Failed to load getQuickViewSpan()");
			System.out.println(ex.toString());
		}
		return false;
	}
	
	
}
