package com.autoprac;

import java.util.ArrayList;
import java.util.List;

import org.openqa.selenium.By;
import org.openqa.selenium.NoSuchElementException;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;
import org.openqa.selenium.support.ui.LoadableComponent;
import org.testng.Assert;

import com.autoprac.HomePage.HomePageTab;
import com.autoprac.common.helpers.ElementHelpers;

public class HomePage extends LoadableComponent<HomePage> {

	public enum HomePageTab
	{
		Popular,
		BestSellers
	}
	
	private final String XPath_Tab = "//a[contains(@class,'%s')]"; 
	private final String XPath_TabContent = "//ul[@id='%s' and contains(@class,'active')]"; 
	private final String XPath_TabProductCount = "//ul[@id='%s' and contains(@class,'active')]/li"; 
	
	private final WebDriver driver;
	@FindBy(className = "rte") private WebElement divRte;
	
	public HomePage(WebDriver driver) {
		this.driver = driver;
		PageFactory.initElements(driver, this);
	}

	public static HomePage constructAndGet(WebDriver driver) {
		HomePage obj = new HomePage(driver);
		return obj.get();
	}
	
	public List<String> getAllATagHrefs()
	{
		List<String> retVal = new ArrayList<String>();
		
		// find the slider_row row
		WebElement bodyDiv = driver.findElement(By.className("columns-container"));
		
		// find all the child aTag elements within the div
		List<WebElement> aTagList = bodyDiv.findElements(By.tagName("a"));

		for(WebElement a : aTagList) {
			String href = a.getAttribute("href");
			if(href != null && href.length() > 0 && !href.contains("controller=cart&add=")) {	
				if(!retVal.contains(href)) {
					retVal.add(href);
				}
			}
		}
		
		return retVal;
	}
	
	private String getClassNameForHomePageTab(HomePageTab tab) {
		return (tab == HomePageTab.Popular) ? "homefeatured" : "blockbestsellers";
	}
	
	public boolean tryClickTab(HomePageTab tab)
	{
		String xPath = null;
		String className = getClassNameForHomePageTab(tab);
		
		// 1. find the specific tab (ATag) we are looking for
		xPath = String.format(XPath_Tab, className);
		WebElement ele = driver.findElement(By.xpath(xPath));
		if(ele == null) {
			return false;
		}
		
		// 2. Click the ATag
		ele.click();
		
		// 3. Validate the parent li has the class 'active'
		String classValue = ElementHelpers.getElementAttribute(driver, By.xpath(xPath + "/.."), "class");
		if(!"active".equalsIgnoreCase(classValue)) {
			return false;
		}
		
		// 4. Validate the tab content specific div is shown and has the class active
		xPath = String.format(XPath_TabContent, className);
		classValue = ElementHelpers.getElementAttribute(driver, By.xpath(xPath), "class"); 
		if(!classValue.contains("active")) {
			return false;
		}

		// 5. Validate at least 1 item/product is shown
		return driver.findElements(By.xpath(xPath + "/li")).size() > 0;
		
	}
	
	public TabProduct getTabProductByIndex(HomePageTab tab, int index) {
		return new TabProduct(driver, tab, index);
	}

	// Returns the number of products that exist within the tab (Popular or Best Sellers)
	public int getTabProductCount(HomePageTab tab) {
		String className = getClassNameForHomePageTab(tab);
		String xPath = String.format(XPath_TabProductCount, className);
		return driver.findElements(By.xpath(xPath)).size();
	}
	
	
	public TabProduct getFirstProduct(HomePageTab tab, boolean mustBeAbleToAddToCart) {

		// NOTE: tryClickTab() must be called before this method
		int productCount = getTabProductCount(tab);
		Assert.assertTrue(productCount > 0, "No products exist within tab " + tab.toString());

		// NOTE: this needs to be iterative as the product may not be in stock - and therefore cannot be added to cart
		HomePage.TabProduct firstProduct = null;
		for (int i = 1; i <= productCount; i++) {

			firstProduct = getTabProductByIndex(tab, i);

			ProductMouseOver productMouseOver = firstProduct.mouseOverProduct(false);
			Assert.assertNotNull(productMouseOver, "mouseOverProduct has failed. Index: " + i);
			Assert.assertTrue(productMouseOver.isLoaded(false), "mouseOver dialog has failed to load. Index: " + i);

			if ( (!mustBeAbleToAddToCart) || (mustBeAbleToAddToCart && productMouseOver.isAddToCartEnabled()) ) {
				return firstProduct;
			}
			
		}

		Assert.assertNotNull(null, "No items exist that can be added to cart");
		
		return null;

	}
	
	
	@Override
	protected void isLoaded() throws Error {
		try {
			Assert.assertTrue(divRte != null && divRte.isDisplayed(), "Page is not loaded");
		} catch(NoSuchElementException ex) {
			throw new AssertionError();	
		}
	}

	@Override
	protected void load() {
		driver.get(Constants.BaseUrl);
	}
	
	public InPageShoppingCart getShoppingCart() {
		return new InPageShoppingCart(driver);
	}
	
	public class TabProduct {

		private String xPathBase;
		private WebDriver driver;
		private TabProduct(WebDriver driver) {
			this.driver = driver;
		}
		public TabProduct(WebDriver driver, HomePageTab tab, int index) {
			this(driver);
			
			if(index < 1) {
				throw new IndexOutOfBoundsException("index is 1 and must be > 0");
			}
			
			String className = getClassNameForHomePageTab(tab);
			this.xPathBase = String.format(XPath_TabContent + "/li[%d]", className, index) ;
			
		}
		
		private String productName;
		public String GetProductName() {
			if(productName == null) {
				String xPath = xPathBase + "//a[contains(@class, 'product-name')]";
				productName = ElementHelpers.getElementText(driver, By.xpath(xPath));
			}
			return (productName == null) ? "" : productName;
		}

		private double productPrice;
		public double GetProductPrice() {
			if(productPrice <= 0) {
				String xPath = xPathBase + "//div[contains(@class, 'right-block')]//span[contains(@class, 'product-price')]";
				productPrice = ElementHelpers.getNumericValueFromElement(driver, By.xpath(xPath));
			}
			return productPrice;
		}
		
		public ProductMouseOver mouseOverProduct(boolean throwEx) {
			try {
				ElementHelpers.mouseOverElement(driver, By.xpath(xPathBase));
				return new ProductMouseOver(driver, xPathBase);
			} catch(Exception ex) {
				if(throwEx) {
					throw ex;
				}
			}
			return null;
		}
		
	}
}
