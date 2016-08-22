package com.autoprac;

import org.openqa.selenium.NoSuchElementException;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;
import org.openqa.selenium.support.ui.LoadableComponent;
import org.testng.Assert;

public class MyAccount extends LoadableComponent<MyAccount> {

	private final WebDriver driver;
	private final LoadableComponent<SignInPage> parentPage;
	
	@FindBy(className = "page-heading") 	private WebElement _pageHeadHTag;
	@FindBy(className = "logout") 			private WebElement _logOutATag;
	
	public MyAccount(WebDriver driver) {
		this(driver, null);
	}

	public MyAccount(WebDriver driver, LoadableComponent<SignInPage> parentPage) {
		this.driver = driver;
		this.parentPage = parentPage;
		PageFactory.initElements(driver, this);
	}
	
	@Override
	protected void isLoaded() throws Error {
		try {
			if(this.parentPage == null) {
				throw new NoSuchElementException("The parent page object is required");
			}
			Boolean flgSuccess =
				(_pageHeadHTag != null && "MY ACCOUNT".equalsIgnoreCase(_pageHeadHTag.getText())) && 
				(_logOutATag != null && "Sign out".equalsIgnoreCase(_logOutATag.getText()));
			Assert.assertTrue(flgSuccess, "Page is not loaded");
		} catch(NoSuchElementException ex) {
			throw new AssertionError();
		}
	}

	@Override
	protected void load() {
		if(this.parentPage != null) {
			try {
				this.parentPage.get().login(Constants.SignIn_Username, Constants.SignIn_Password);
			} catch(Exception ex) {
				// do nothing
			}
		}
	}

}
