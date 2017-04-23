package jmeterjunit;

import org.junit.Assert;
import org.junit.Test;

public class MyTestClass {

	public MyTestClass() {
		
	}
	
	public MyTestClass(String args) {
		System.out.println("args: " + args);
	}
	
	@Test
	public void testWithFireFox() {
		int a = 1 + 2;
		for(int i=0; i<100000; i++)
			Assert.assertEquals(a, 3);
	}

	@Test
	public void testWithChrome() {
		int a = 1 + 2;
		Assert.assertEquals(a, 3);
	}
	
	private void performTest() {
		
	}
	
	
	
}
