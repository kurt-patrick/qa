package kpe.se.wordpress.pages;

import org.openqa.selenium.WebDriver;

import kpe.se.common.PageBase;

public class AdminPage extends PageBase {

	private AdminDashboardPage _dashboard = null;
	
	public AdminPage(WebDriver driver) {
		super(driver);
		_dashboard = new AdminDashboardPage(driver);
	}
	
	public Boolean isLoaded() {
		return _dashboard.isLoaded() && _driver.getCurrentUrl().contains("wp-admin");
	}

	public AdminDashboardPage Dashboard() {
		return _dashboard;
	}
	
}
