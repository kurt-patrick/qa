package com.autoprac;

import org.openqa.selenium.WebDriver;

public interface IConstructAndGet<T>
{
	public T constructAndGet(WebDriver driver);
}
