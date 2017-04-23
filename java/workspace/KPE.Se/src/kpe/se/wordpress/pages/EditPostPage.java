package kpe.se.wordpress.pages;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;

import kpe.se.common.PageBase;

public class EditPostPage extends PageBase {

	private By _locTitle = By.xpath("//input[@id='title']");
	private By _locEditor = By.id("tinymce");
	private By _locPTag = By.tagName("p");
	private By _locUpdate = By.name("save");
	private By _locViewPost = By.linkText("View Post");
	
	public EditPostPage(WebDriver driver) {
		super(driver);
	}
	
	public String getTitle() {
		return getInputValue(_locTitle);
	}
	
	public String getBody() {
		return getTextFromMceWidget("content_ifr", _locEditor, _locPTag);
	}
	
	public Boolean isLoaded() {
		return tryWaitForExists(_locTitle, By.id("content_tbl"), _locUpdate);
	}
	
	public void ClickViewPost() {
		performClick(_locViewPost);
	}
	
}
