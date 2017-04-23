package kpe.se.wordpress.pages;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;

import kpe.se.common.*;

public class LoginPage extends PageBase {

	private By _locUsername = By.id("user_login");
	private By _locPassword = By.id("user_pass");
	private By _locSubmit = By.id("wp-submit");
	
	public LoginPage(WebDriver driver) {
		super(driver);
	}
	
	public void NavigateTo() {
		navigate("http://demosite.center/wordpress/wp-login.php");
	}
	
	public Boolean isLoaded() {
		return tryWaitForVisible(_locUsername, _locPassword, _locSubmit);
	}

	public AdminPage login(String username, String password) {
		sendKeys(_locUsername, username, true);
		sendKeys(_locPassword, password, true);
		performClick(_locSubmit);
		
		return new AdminPage(_driver);
	}
	

}
