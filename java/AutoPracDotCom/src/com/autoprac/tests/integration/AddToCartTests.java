package com.autoprac.tests.integration;

import org.openqa.selenium.WebDriver;
import org.testng.Assert;
import org.testng.annotations.AfterTest;
import org.testng.annotations.BeforeTest;
import org.testng.annotations.Test;

import com.autoprac.AddedToCartModal;
import com.autoprac.HomePage;
import com.autoprac.HomePage.HomePageTab;
import com.autoprac.HomePage.TabProduct;
import com.autoprac.InPageShoppingCart;
import com.autoprac.ProductMouseOver;
import com.autoprac.ProductQuickView;
import com.autoprac.common.helpers.DriverHelper;

public class AddToCartTests {
	
	private WebDriver _driver;

	public AddToCartTests() {}
	
	@BeforeTest
	public void beforeTest() {
		_driver = DriverHelper.getDriver();
	}

	@Test
	public void validateHomePageLoads() {
		getHomePage();
	}
	
	@Test
	public void validateAddToCartFromQuickView() {
		
		HomePage homePage = getHomePage();
		InPageShoppingCart inPageCart = homePage.getShoppingCart(); 
		if(inPageCart.getTotalQuantity() > 0) {
			inPageCart.mouseOver().clearCart();
		}
		
		homePage.tryClickTab(HomePageTab.Popular);
		TabProduct product = homePage.getFirstProduct(HomePageTab.Popular, true);
		ProductMouseOver mouseOver = product.mouseOverProduct(true);
		mouseOver.isLoaded(true);
		
		// Get product (name and unit price)
		String expectedProductName = product.GetProductName();
		double expectedProductPrice = product.GetProductPrice();

		// click quick view and wait for it to load
		ProductQuickView quickView = mouseOver.clickQuickView(true);
		quickView.isLoaded(true);
		
		// Validate (Name and Price) match between screens
		Assert.assertEquals(quickView.GetProductName(), expectedProductName, "Product name on homepage and quickview dont match");
		Assert.assertEquals(quickView.GetProductPrice(), expectedProductPrice, "Product price on homepage and quickview dont match");

		// click add to cart
		AddedToCartModal addedToCartModal = quickView.clickAddToCart();
		addedToCartModal.isLoaded(true);
		
		// Validate Product (Name and Price) match between screens
		//Assert.assertEquals(addedToCartModal.GetProductName(), expectedProductName, "Product name on homepage and quickview dont match");
		//Assert.assertEquals(addedToCartModal.GetProductPrice(), expectedProductPrice, "Product price on homepage and quickview dont match");
		
	}
	
	@AfterTest
	public void afterTest() {
		DriverHelper.dispose(_driver);
	}

	private HomePage getHomePage() {
		return new HomePage(_driver).get();
	}
	

}
