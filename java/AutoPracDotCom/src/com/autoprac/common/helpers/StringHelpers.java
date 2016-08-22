package com.autoprac.common.helpers;

public final class StringHelpers {
	
	private StringHelpers() {}
	
	public static String removeCurrencySymbol(String value, boolean trimString) {
		String retVal = value.replace("$", "");
		if(trimString) {
			retVal = retVal.trim();
		}
		return retVal;
	}

	public static boolean isStringNullOrEmpty(String value) {
		return (value == null) || (value.length() == 0);
	}
	
	public static boolean isStringNullOrWhiteSpace(String value) {
		return (value == null) || (value.length() > 0 && value.trim().length() == 0);
	}
	
	public static boolean isStringNullOrEmptyOrWhiteSpace(String value) {
		return isStringNullOrEmpty(value) || isStringNullOrWhiteSpace(value);
	}

}
