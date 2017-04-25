'use strict';

/*
 * Mouse over practise page 
 * http://sahitest.com/demo/mouseover.htm
 */
function MouseOverPage() {
	
	var m_this = this;
	var m_byIdWriteOnHover = "Write on hover";
	var m_byIdBlankOnHover = "Blank on hover";
	
	this.assertButton_WriteOnHover = function () {
		assertButton(m_byIdWriteOnHover, "Write on hover");
	}

	this.assertButton_BlankOnHover = function () {
		assertButton(m_byIdBlankOnHover, "Blank on hover");
	}
	
	this.assertTextBox_TB01 = function ($text) {
		assertTextBox("t1", $text);
	}

	this.mouseOverButton = function ($id) {
		_mouseOver(_button($id));
		return m_this;
	}
	
	this.mouseOverWriteOnHover = function () {
		m_this.mouseOverButton(m_byIdWriteOnHover);
		return m_this;
	}

	this.mouseOverBlankOnHover = function () {
		m_this.mouseOverButton(m_byIdBlankOnHover);
		return m_this;
	}
	
	function assertButton($id, $text) {
		_assertExists(_button($id));
		_assert(_isVisible(_button($id)));
		_assertEqual($text, _getValue(_button($id)));
	}
	
	function assertTextBox($id, $text) {
		_assertExists(_textbox($id));
		_assert(_isVisible(_textbox($id)));
		_assertEqual($text, _getValue(_textbox($id)));
	}
	
};
