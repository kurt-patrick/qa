package com.autoprac;

import java.util.List;
import java.util.ArrayList;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;
import org.testng.Assert;
import org.testng.annotations.Test;

import com.autoprac.InPageShoppingCart.CartItem;
import com.autoprac.common.helpers.ElementHelpers;

public class InPageShoppingCart {

	private final String xPathBase 				= "//div[contains(@class, 'shopping_cart')]";
	private final String xPathCartQuantity		= xPathBase + "//span[contains(@class, 'ajax_cart_quantity')]";
	private final String xPathCartEmpty			= xPathBase + "//span[contains(@class, 'ajax_cart_no_product')]";
	
	private final WebDriver driver;
	
	@FindBy(xpath = xPathBase)					private WebElement baseElement;
	@FindBy(xpath = xPathCartQuantity)			private WebElement cartQuantityElement;
	@FindBy(xpath = xPathCartEmpty)				private WebElement cartEmptyElement;

	public InPageShoppingCart(WebDriver driver) {
		this.driver = driver;
		PageFactory.initElements(driver, this);
	}
	
	public boolean isLoaded() {
		try {
			return 	this.baseElement.isDisplayed() && (this.cartQuantityElement.isDisplayed() || this.cartEmptyElement.isDisplayed());
		} catch(Exception ex) {
			return false;
		}
	}
	
	public int getTotalQuantity() {
		if(this.cartEmptyElement.isDisplayed()) {
			return 0;
		}
		return Integer.parseInt(this.cartQuantityElement.getText());
	}
	
	public InPageShoppingCart.DropDown mouseOver() {
		DropDown retVal = new DropDown(driver);
		ElementHelpers.mouseOverElement(driver, By.xpath(xPathBase + "/a"));
		/*
		if(!retVal.isLoaded()) {
			ElementHelpers.mouseOverElement(driver, By.xpath(xPathBase + "/a"));
		}
		*/
		return retVal;
	}

	public class DropDown {
		
		private final String xPathBase = "//div[contains(@class, 'cart_block_list')]";
		private final String xPathProductsBase = xPathBase + "/dl[contains(@class, 'products')]";
		private final String xPathCartPricesBase = xPathBase + "/div[contains(@class, 'cart-prices')]";
		private final String xPathCartButtonsBase = xPathBase + "/p[contains(@class, 'cart-buttons')]";
		
		private final WebDriver driver;
		
		public DropDown(WebDriver driver) {
			this.driver = driver;
		}
		
		public boolean isLoaded() {
			try {
				return 	ElementHelpers.getElement(driver, By.xpath(xPathProductsBase), true, true, 3).isDisplayed() && 
						ElementHelpers.getElement(driver, By.xpath(xPathCartPricesBase), true, true, 3).isDisplayed() &&
						ElementHelpers.getElement(driver, By.xpath(xPathCartButtonsBase), true, true, 3).isDisplayed();
			} catch(Exception ex) {
				// logging ???
			}
			return false;
		}
		
		public List<CartItem> getAllCartItems() {
			int index = 1;
			List<CartItem> retVal = new ArrayList<CartItem>();
			List<WebElement> baseElementList = driver.findElements(By.xpath(xPathProductsBase + "/dt"));
			for(WebElement baseElement : baseElementList) {
				retVal.add(new CartItem(driver, baseElement, xPathProductsBase + "/dt[" + index + "]"));
				index += 1;
			}
			return retVal;
		}
		
		public int getItemCount() {
			return driver.findElements(By.xpath(xPathProductsBase + "/dt")).size();
		}
		
		public double getShipping() {
			String xPath = xPathCartPricesBase + "/div/span[contains(@class, 'cart_block_shipping_cost')]";
			return ElementHelpers.getNumericValueFromElement(driver, By.xpath(xPath));
		}
		
		public double getTotal() {
			String xPath = xPathCartPricesBase + "/div/span[contains(@class, 'cart_block_total')]";
			return ElementHelpers.getNumericValueFromElement(driver, By.xpath(xPath));
		}

		public void clickCheckOut() {
			
		}
		
		public void clearCart() {

			int cartQuantity = getTotalQuantity();
			if (cartQuantity == 0) {
				// No need to do anything - Exit
				return;
			}

			Assert.assertTrue(cartQuantity > 0, "Home page cart contains no items");

			InPageShoppingCart.DropDown dropDown = mouseOver();
			Assert.assertNotNull(dropDown, "Mouse over the cart has not loaded the dropdown");

			List<CartItem> cartItems = dropDown.getAllCartItems();
			Assert.assertTrue(cartItems.size() > 0, "Failed to get the cart items");

			CartItem cartItem = null;
			int expectedItemCount = cartItems.size() - 1;
			for (int i = cartItems.size() - 1; i >= 0; i--) {

				cartItem = cartItems.get(i);
				Assert.assertNotNull(cartItem, "Cart item is null at index: " + i);

				// cart.mouseOver();
				Assert.assertTrue(cartItem.tryClickRemove(),
						"Clicking (x) to remove the product has failed at index: " + i);

				// the clicked item will take a few seconds to be removed
				Assert.assertEquals(dropDown.getItemCount(), expectedItemCount,
						"Cart item count has not decreased by 1 to: " + expectedItemCount);
				expectedItemCount -= 1;
				cartItem = null;
			}

			Assert.assertEquals(dropDown.getItemCount(), (int) 0, "The cart should not contains items");
			Assert.assertEquals(dropDown.getShipping(), (double) 0, "The carts shipping is not set to zero");
			Assert.assertEquals(dropDown.getTotal(), (double) 0, "The carts total is not set to zero");

		}
		
		
	}
	
	public class CartItem {
		
		private final String xPathBase;
		private final WebDriver driver;
		private final WebElement baseElement; 
		
		public CartItem(WebDriver driver, WebElement baseElement, String xPathBase) {
			this.driver = driver;
			this.xPathBase = xPathBase;
			this.baseElement = baseElement;
		}
		
		public boolean tryClickRemove() {
			try {
				WebElement element = getRemoveATag();
				element.click();
				new WebDriverWait(driver, 5).until(ExpectedConditions.stalenessOf(element));
				return true;
			} catch (Exception ex) {
				System.out.println(ex.toString());
			}
			return false;
		}
		
		private WebElement getRemoveATag() {
			String xPath = xPathBase + "/span/a[contains(@class, 'ajax_cart_block_remove_link')]";
			//System.out.println("getRemoveATag(): xPath: " + xPath);
			//System.out.println("getRemoveATag(): Waiting for (x) to be visible");
			new WebDriverWait(driver, 3).until(ExpectedConditions.visibilityOfElementLocated(By.xpath(xPath)));
			//System.out.println("getRemoveATag(): (x) is visible");
			return driver.findElement(By.xpath(xPath));
		}
		
		public boolean isLoaded() {
			try {
				return 	this.baseElement.isDisplayed() && 
						this.getRemoveATag().isDisplayed();
			} catch(Exception ex) {
				// logging ???
			}
			return false;
		}

		public boolean isUnLoaded() {
			try {
				new WebDriverWait(driver, 2).until(ExpectedConditions.stalenessOf(baseElement));
				return true;
			} catch(Exception ex) {
				// logging ???
			}
			return false;
		}
		
		public int getCartQuantity() {
			return 0;
//			return Integer.parseInt(this.cartQuantityElement.getText());
		}
		
	}
	
}
