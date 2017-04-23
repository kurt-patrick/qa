package kpe.se.wordpress.pages;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;

import kpe.se.common.PageBase;

public class AdminDashboardPage extends PageBase {

	private By _locDashboard = By.xpath("//*[@id='adminmenu']//a[starts-with(text(),'Dashboard')]");
	private By _locPosts = getParentLocator("Posts");
	private By _locSettings = getParentLocator("Settings");
	
	public AdminDashboardPage(WebDriver driver) {
		super(driver);
	}
	
	public Boolean isLoaded() {
		return tryWaitForVisible(_locPosts, _locDashboard, _locSettings);
	}
	
	public AddNewPostPage mouseOverPostsClickAddNew() {
		mouseOverMenuItemAndClickChildATag("Posts", "Add New");
		return new AddNewPostPage(_driver);
	}
	
	public void clickMenuItem(String menuText) {
		By locator = getParentLocator(menuText);
		mouseOverElement(locator);
		performClick(locator);
	}
	
	public void mouseOverMenuItemAndClickChildATag(String menuText, String childText) {
		mouseOverParent(menuText);
		clickChildATag(childText);
	}

	private void mouseOverParent(String text) {
		By locator = getParentLocator(text);
		mouseOverElement(locator);
	}
	
	private void clickChildATag(String text) {
		By locator = getChildATagLocator(text);
		WebElement element = waitForVisible(locator)[0];
		performClick(element);
	}

	private static By getParentLocator(String text) {
		String xPath = String.format("//*[@id='adminmenu']//a[starts-with(text(),'%s')]", text);
		return By.xpath(xPath);
	}
	
	private static By getChildATagLocator(String text) {
		String xPath = String.format("//*[@id='adminmenu']//div[@class='wp-submenu sub-open']//a[contains(text(),'%s')][1]", text);
		return By.xpath(xPath);
	}

}
