package com.autoprac;

import org.openqa.selenium.NoSuchElementException;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.FindBy;
import org.openqa.selenium.support.PageFactory;
import org.openqa.selenium.support.ui.LoadableComponent;
import org.testng.Assert;

import com.autoprac.common.helpers.ElementHelpers;

public class SignInPage extends LoadableComponent<SignInPage> {
	
	private final WebDriver driver;
	private final LoadableComponent<?> parentPage;
	
	@FindBy(id = "email") 					private WebElement emailAddressInput;
	@FindBy(id = "passwd")					private WebElement passwordInput;
	@FindBy(id = "SubmitLogin")				private WebElement loginButton;
	@FindBy(className = "page-heading")		private WebElement pageHeadingHTag;

	public SignInPage(WebDriver driver) {
		this(driver, null);
	}
	
	public SignInPage(WebDriver driver, LoadableComponent<?> parentPage) {
		this.driver = driver;
		this.parentPage = parentPage;
		PageFactory.initElements(driver, this);
	}
	
	public MyAccount login(String emailAddress, String password) 
	{
		ElementHelpers.pressKeys(emailAddressInput, emailAddress, true);
		ElementHelpers.pressKeys(passwordInput, password, true);
		loginButton.click();
		return PageFactory.initElements(driver, MyAccount.class);
	}

	public SignInPage loginFail(String emailAddress, String password) 
	{
		ElementHelpers.pressKeys(emailAddressInput, emailAddress, true);
		ElementHelpers.pressKeys(passwordInput, password, true);
		loginButton.click();
		return this;
	}
	
	@Override
	protected void isLoaded() throws Error {
		try {
			Boolean flgSuccess =
				(pageHeadingHTag != null && pageHeadingHTag.isDisplayed()) && 
				"Authentication".equalsIgnoreCase(pageHeadingHTag.getText());
			Assert.assertTrue(flgSuccess, "Page is not loaded");
		} catch(NoSuchElementException ex) {
			throw new AssertionError();	
		}
	}

	@Override
	protected void load() {
		if(this.parentPage != null) {
			this.parentPage.get();
		}
		driver.get(Constants.SignInUrl);
	}

}
