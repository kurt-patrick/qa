package com.autoprac.tests;

import org.openqa.selenium.WebDriver;
import org.testng.annotations.Test;

import com.autoprac.MyAccount;
import com.autoprac.PageHeader;
import com.autoprac.SignInPage;
import com.autoprac.common.helpers.DriverHelper;

public class MyAccountTests {
	
	@Test
	public void ValidateMyAccountLoadsAfterSignIn() {

		WebDriver driver = DriverHelper.getDriver();
		
		PageHeader pageHeader = new PageHeader(driver);
		SignInPage signInPage = new SignInPage(driver, pageHeader);
		MyAccount myAccountPage = new MyAccount(driver, signInPage);
		myAccountPage.get();
		
		DriverHelper.dispose(driver);
		
	}

}
