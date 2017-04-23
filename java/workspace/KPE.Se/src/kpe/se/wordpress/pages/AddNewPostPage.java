package kpe.se.wordpress.pages;

import org.openqa.selenium.By;
import org.openqa.selenium.WebDriver;

import kpe.se.common.PageBase;
import kpe.se.common.TimeOuts;

public class AddNewPostPage extends PageBase {

	private By _locTitle = By.id("title");
	private By _locEditor = By.id("tinymce");
	private By _locPublish = By.id("publish");
	
	public AddNewPostPage(WebDriver driver) {
		super(driver);
	}
	
	public Boolean isLoaded() {
		return tryWaitForExists(_locTitle, _locPublish, By.id("content_tbl"));
	}
	
	public void setTitle(String text) {
		sendKeys(_locTitle, text, true);
	}
	
	public void setBody(String text) {
		sendKeysToMceWidget("content_ifr", _locEditor, text);
	}
	
	public void clickPublish() {
		mouseOverElement(_locPublish);
		performClick(_locPublish);
	}
	
} 
