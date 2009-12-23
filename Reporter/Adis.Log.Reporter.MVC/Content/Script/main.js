/// <reference path="jquery-1.3.2.js" />

(function()
{

	function Main()
	{
		this.Name = "Main";
		this.Debug = false;
		this.Version = 1.00;
	}

	Main.prototype = {
		/*----------------*/
		/* Base functions */
		/*----------------*/

		Init: function(version)
		{
			if (version != this.Version)
			{
				alert(stringFormat("{0} script file is the wrong verion, it is {1}, it should be {2}", this.Name, this.Version, version));
			}
		}

		, EnableDebug: function()
		{
			this.Debug = true;
		}


		, SetPageNumberAndSubmit: function(pageNumber)
		{
			document.forms[0].elements['pageNumber'].value = pageNumber;
			document.forms[0].submit();
		}

		, ToggleExtraInfo: function(button)
		{
			var $extraInfo = $(button).parents("table").find(".extra_info");
			if ($extraInfo.css('display') == 'none')
			{
				$extraInfo.slideDown('slow');
				$(button).text("Hide Extra Info");
			} else
			{
				$extraInfo.slideUp('slow');
				$(button).parents("table").find(".show_extra_info_button").text("Show Extra Info");
			}
		}

	}

	// set up global object instance
	if (!window.logging) { window.logging = new function() { } }
	window.logging.main = new Main();

	var local = logging.main;


})();

