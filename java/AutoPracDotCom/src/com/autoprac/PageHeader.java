package com.autoprac;

import java.util.List;
import java.util.ArrayList;

import org.openqa.selenium.By;
import org.openqa.selenium.NoSuchElementException;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;
import org.openqa.selenium.support.ui.LoadableComponent;
import org.testng.Assert;

public class PageHeader extends LoadableComponent<PageHeader> {

	private final WebDriver driver;
	
	@FindBy(id = "search_query_top")	private WebElement _searchInputTop;
	@FindBy(className = "login")		private WebElement _signInATag;

	public PageHeader(WebDriver driver) {
		this.driver = driver;
		PageFactory.initElements(driver, this);
	}
	
	private List<String> getAllATagHrefs()
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
	
	
	@Override
	protected void isLoaded() throws Error {
		try {
			Assert.assertTrue((_searchInputTop != null) && (_searchInputTop.isDisplayed()), "Page is not loaded");
		} catch(NoSuchElementException ex) {
			throw new AssertionError();	
		}
	}

	@Override
	protected void load() {
		driver.get(Constants.BaseUrl);
	}

	public SignInPage ClickSignIn() {
		this._signInATag.click();
		SignInPage rv = PageFactory.initElements(driver, SignInPage.class);
		return rv;
	}
	
}
