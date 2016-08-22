package com.autoprac.tests;

import java.util.List;

import org.openqa.selenium.WebDriver;
import org.testng.Assert;
import org.testng.annotations.AfterTest;
import org.testng.annotations.BeforeTest;
import org.testng.annotations.Test;

import com.autoprac.*;
import com.autoprac.HomePage.*;
import com.autoprac.InPageShoppingCart.CartItem;
import com.autoprac.common.helpers.DriverHelper;

public class HomePageTests {

	private final String DependsOn_validateAddToCart = "validateAddToCart";
	private final String DependsOn_validateHomePageLoads = "validateHomePageLoads";
	private final String DependsOn_validateTabsWork = "validateTabsWork";
	private final String DependsOn_validateProductMouseOver = "validateProductMouseOver";

	private WebDriver _driver;

	@BeforeTest
	public void beforeTest() {
		_driver = DriverHelper.getDriver();
	}

	@Test
	public void validateHomePageLoads() {
		getHomePage();
	}

	/*
	 * @Test (dependsOnMethods = DependsOn_validateHomePageLoads) public void
	 * validateCartMouseOver() {
	 * 
	 * InPageShoppingCart cart = new InPageShoppingCart(_driver);
	 * Assert.assertTrue(cart.isLoaded(),
	 * "Home page shopping cart does not exist"); InPageShoppingCart.DropDown
	 * dropDown = cart.mouseOver();
	 * 
	 * try { Thread.sleep(5000); } catch (InterruptedException e) { // TODO
	 * Auto-generated catch block e.printStackTrace(); }
	 * 
	 * Assert.assertNotNull(dropDown, "cart.mouseOver() has returned null");
	 * Assert.assertTrue(dropDown.isLoaded(),
	 * "Mousing over the cart has not loaded the dropdown");
	 * 
	 * }
	 */

	@Test(dependsOnMethods = DependsOn_validateHomePageLoads)
	public void validateTabsWork() {

		HomePage homePage = getHomePage();
		Assert.assertTrue(homePage.tryClickTab(HomePageTab.BestSellers), "Clicking tab 'Best sellers failed'");

		homePage.tryClickTab(HomePageTab.Popular);
		Assert.assertTrue(homePage.tryClickTab(HomePageTab.Popular), "Clicking tab 'Popular'");

	}

	@Test(dependsOnMethods = DependsOn_validateHomePageLoads)
	public void preTestStepClearCart() {
		clearCart(true);
	}

	@Test(dependsOnMethods = DependsOn_validateTabsWork)
	public void validateProductHasPriceAndName() {

		HomePage homePage = getHomePage();
		HomePage.TabProduct product = homePage.getTabProductByIndex(HomePageTab.Popular, 1);

		Assert.assertNotNull(product, "Failed to get the first product in the popular tab");
		Assert.assertTrue(product.GetProductPrice() > 0, "Product price is <= 0");
		Assert.assertTrue(product.GetProductName().trim().length() > 0, "Product name is missing");

	}

	@Test(dependsOnMethods = DependsOn_validateTabsWork)
	public void validateProductMouseOver() {
		HomePage homePage = getHomePage();
		HomePage.TabProduct product = homePage.getTabProductByIndex(HomePageTab.Popular, 1);
		ProductMouseOver productMouseOver = product.mouseOverProduct(false);
		Assert.assertNotNull(productMouseOver, "mouseOverProduct has failed");
		Assert.assertTrue(productMouseOver.isLoaded(false), "mouseOver dialog has failed to load");
	}

	@Test(dependsOnMethods = DependsOn_validateProductMouseOver)
	public void validateProductMouseOverAndClickQuickView() {

		HomePage homePage = getHomePage();
		HomePage.TabProduct product = homePage.getTabProductByIndex(HomePageTab.Popular, 1);

		ProductMouseOver productMouseOver = product.mouseOverProduct(false);
		Assert.assertNotNull(productMouseOver, "mouseOverProduct has failed");

		ProductQuickView productQuickView = productMouseOver.clickQuickView(false);
		Assert.assertNotNull(productQuickView, "clicking quick view  has failed");

		Assert.assertTrue(productQuickView.isLoaded(false), "quick view modal is not loaded");
		Assert.assertTrue(productQuickView.clickClose(), "Clicking (X) close on quick view has failed");
		Assert.assertNotNull(homePage.get(), "Homepage is not loaded");

	}

	@Test(dependsOnMethods = DependsOn_validateProductMouseOver)
	public void validateAddToCart() {

		// NOTE: this needs to be iterative as the product may not be in stock
		// and therefore cannot be added to cart

		HomePage homePage = getHomePage();

		int productCount = homePage.getTabProductCount(HomePageTab.Popular);
		Assert.assertTrue(productCount > 0, "No products exist within tab (Popular)");

		boolean flgProductHasStock = false;
		for (int i = 1; i <= productCount; i++) {

			HomePage.TabProduct product = homePage.getTabProductByIndex(HomePageTab.Popular, i);

			ProductMouseOver productMouseOver = product.mouseOverProduct(false);
			Assert.assertNotNull(productMouseOver, "mouseOverProduct has failed. Index: " + i);
			Assert.assertTrue(productMouseOver.isLoaded(false), "mouseOver dialog has failed to load. Index: " + i);

			flgProductHasStock = productMouseOver.isAddToCartEnabled();
			if (!flgProductHasStock) {
				// skip this product
				System.out.println("Skipping product at index " + i + " as add to cart button is disabled");
				continue;
			}

			AddedToCartModal modal = productMouseOver.clickAddToCart();
			Assert.assertTrue(modal.isLoaded(false), "Added to cart modal is not loaded");
			Assert.assertTrue(modal.clickClose(false), "Clicking (X) on added to cart modal has failed");
			Assert.assertTrue(modal.isUnloaded(), "Added to cart modal has not been unloaded");

		}

		Assert.assertTrue(flgProductHasStock, "No items exist that can be added to cart");

	}

	@Test(dependsOnMethods = DependsOn_validateAddToCart)
	public void validateClearCart() {
		clearCart(false);
	}

	@Test(dependsOnMethods = DependsOn_validateHomePageLoads)
	private void clearCart(boolean preTestClearCart) {

		// System.out.println("preTestClearCart: " + preTestClearCart);

		int cartQuantity = 0;
		InPageShoppingCart cart = new InPageShoppingCart(_driver);
		Assert.assertTrue(cart.isLoaded(), "Home page shopping cart does not exist");

		cartQuantity = cart.getTotalQuantity();
		// System.out.println("cartQuantity: " + cartQuantity);
		if (preTestClearCart && cartQuantity == 0) {
			// No need to do anything - Exit
			return;
		}

		Assert.assertTrue(cartQuantity > 0, "Home page cart contains no items");

		InPageShoppingCart.DropDown dropDown = cart.mouseOver();
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

	@Test(dependsOnMethods = DependsOn_validateProductMouseOver)
	public void validateProductMouseOverAndClickMore() {

		HomePage homePage = getHomePage();
		HomePage.TabProduct product = homePage.getTabProductByIndex(HomePageTab.Popular, 1);

		// Assert.assertTrue(product.mouseOverProductAndClickQuickView(),
		// "Mouseover and clicking quick view has failed");
		// Assert.assertTrue(product.clickCloseOnClickQuickView(), "Clicking (X)
		// close on quick view has failed");

	}

	// @Test(dependsOnMethods = DependsOn_validateHomePageLoads)
	public void validateSliderRow() {
		HomePage homePage = getHomePage();

		List<String> hrefs = homePage.getAllATagHrefs();
		Assert.assertTrue(hrefs.size() > 0);

		System.out.println("A tags found: " + hrefs.size());
		for (String aTag : hrefs) {

			String expectedHref = aTag;

			// click on the a tag
			System.out.println("Naviagating to external url: " + expectedHref);
			_driver.get(expectedHref);

			// validate the href page has loaded
			System.out.println("asserting urls");
			String actualHref = _driver.getCurrentUrl();
			Assert.assertTrue(
					actualHref.equalsIgnoreCase(expectedHref)
							|| actualHref.replace("https", "http").equalsIgnoreCase(expectedHref),
					"Failed to load the site: " + expectedHref);

			// start at the home page again
			System.out.println("homePage.get();");
			homePage.get();

		}

	}

	@AfterTest
	public void afterTest() {
		DriverHelper.dispose(_driver);
	}

	private HomePage getHomePage() {
		return new HomePage(_driver).get();
	}

}
