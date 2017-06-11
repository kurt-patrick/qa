function tearDown(){
	_debug("tearDown()");
	_navigateTo("https://ibs.bankwest.com.au/BWLogin/rib.aspx", true);
	assertPageIsLoaded();
}

function assertPageIsLoaded() {
	assertExistsAndVisible(txtPAN());
	assertExistsAndVisible(txtPWD());
	assertExistsAndVisible(btnLogin());
	assertIsEmpty(txtPAN());
	assertIsEmpty(txtPWD());
}

function txtPAN() {
	return _textbox("AuthUC_txtUserID");
}

function setPAN($value) {
	var $element = txtPAN();
	_setValue($element, $value);	
}

function txtPWD() {
	return _password("AuthUC_txtData");
}

function setPWD($value) {
	var $element = txtPWD();
	_setValue($element, $value);	
}

function btnLogin() {
	return _link("AuthUC_btnLogin");
}

function clickLogin() {
	var $element = btnLogin();
	_click($element);
}

function assertExistsAndVisible($element) {
	_assertExists($element);
	_assertTrue(_isVisible($element));
}

function assertIsEmpty($element) {
	_assertEqual("", _getValue($element));	
}

function testNoDetailsProvided() {
	_debug("testNoDetailsProvided()");
	panAndSecureCodeTestLogic(
		"", "", "Attention", 
		"You forgot to enter your PAN and secure code. Please try again.");
}

function testNoSecureCode() {
	_debug("testNoSecureCode()");
	panAndSecureCodeTestLogic(
		"a", "", "Attention", 
		"You forgot to enter your secure code. Please try again.");
}

function testNoPAN() {
	_debug("testNoPAN()");
	panAndSecureCodeTestLogic(
		"", "a", "Attention", 
		"You forgot to enter your PAN. Please try again.");
}

function testInvalidLogin() {
	_debug("testInvalidLogin()");
	panAndSecureCodeTestLogic(
		"a", "a", "Attention", 
		"Your PAN and secure code don't match. Please check both and try again. In case you've forgotten your password, you can quickly reset your secure code online.");
}

function panAndSecureCodeTestLogic($pan, $pwd, $heading, $message) {
	setPAN($pan);
	setPWD($pwd);
	clickLogin();
	this.assertExistsAndVisible(_heading2($heading));
	this.assertExistsAndVisible(_span($message));
}

function testPanMaxLength() {
	var $element = txtPAN();
	_assertEqual(8, $element.maxLength);
	setPAN("123456789");
	_assertEqual("12345678", _getText($element));
}

function testSecureCodeMaxLength() {
	var $element = txtPWD();
	_assertEqual(16, $element.maxLength);
	setPWD("12345678901234567");
	_assertEqual("1234567890123456", _getText($element));
}

//tearDown();
_runUnitTests();


