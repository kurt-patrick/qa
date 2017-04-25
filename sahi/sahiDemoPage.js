'use strict';

/*
 * Starting page for all the different practise test pages available
 * http://sahitest.com/demo/
 */
function SahiDemoPage() {
	
	var m_this = this;
	
	this.clickMouseOver = function () {
		assertLink("Mouse over", "Mouse over");
		_click(_link("Mouse over"));
	}

	function clickLink($id) {
		_click(_link("$id));
	}
	
	function assertLink($id, $text) {
		_assertExists(_link($id));
		_assert(_isVisible(_link($id)));
		_assertEqual($text, _getText(_link($id)));
	}
	
}
