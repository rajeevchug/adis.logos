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

		, FilterBarOnReady: function()
		{
			$('#category').change(function() { local.LoadApplicationFilterDropdown(); });
			$('#logServer').change(function() { local.LoadCategoryFilterDropdown(); });
			$('.log_item.has_extra_info').click(function() { logging.main.ToggleExtraInfo(this); });
			//$('.filter_header').click(function() { logging.main.ToggleFilterSlide(); });
		}

		, SetPageNumberAndSubmit: function(pageNumber)
		{
			$('#pageNumber').val(pageNumber);
			document.forms[0].submit();
		}

		, ToggleFilterSlide: function()
		{
			$('#filterForm').slideToggle('slow', function()
			{
				var i = 0;
				$(".main_section").css('top', $(".top_section").height());
			});
		}

		, ToggleExtraInfo: function(logItem)
		{
			var $extraInfo = $(".extra_info", logItem);
			if ($extraInfo.css('display') == 'none')
			{
				$extraInfo.slideDown('slow');
			} else
			{
				$extraInfo.slideUp('slow');
			}
		}

		, LoadCategoryFilterDropdown: function()
		{
			var currentCategory = $('#category').val();
			
			var logServer = $('#logServer').val();
			
			var $categoryDropdown = $("#category");
			$categoryDropdown.unbind('change');
			$categoryDropdown.html("<option value='-1'>Loading, please wait...</option>");

			$.ajax({
				url: urls.Categories.replace('__server__', logServer),
				type: "GET",
				dataType: "json",
				async: true,
				success: function(jsonReply)
				{

					var options = "";
					for (var i = 0; i < jsonReply.length; i++)
					{
						var application = jsonReply[i];
						options += "<option value='" + application + "'>" + application + "</option>";
					}
					$categoryDropdown.html(options);
					$categoryDropdown.val(currentCategory);
				},
				complete: function()
				{
					$categoryDropdown.change(function() { local.LoadApplicationFilterDropdown(); });
				}
			});
		}

		, LoadApplicationFilterDropdown: function()
		{
			var category = $("#category :selected").text();
			var logServer = $('#logServer').val();

			var $applicationDropdown = $("#application");
			$applicationDropdown.html("<option value='-1'>Loading, please wait...</option>");

			$.ajax({
				url: urls.Applications.replace("__server__", logServer).replace("__category__", category),
				type: "GET",
				dataType: "json",
				async: true,
				success: function(jsonReply)
				{

					var options = "";
					for (var i = 0; i < jsonReply.length; i++)
					{
						var application = jsonReply[i];
						options += "<option value='" + application + "'>" + application + "</option>";
					}
					$applicationDropdown.html(options);
				}
			});

		}

		, ResetFilterCookies: function()
		{
			EraseCookie("LogServer");
			EraseCookie("Category");
			EraseCookie("Application");
			EraseCookie("Severity");
			EraseCookie("Machine");
			EraseCookie("Instance");
			EraseCookie("User");
			EraseCookie("StartTime");
			EraseCookie("EndTime");

		}

	}

	function CreateCookie(name, value, days)
	{
		if (!days) { days = 365; }
		var date = new Date();
		date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
		var expires = "; expires=" + date.toGMTString();
		document.cookie = name + "=" + value + expires + "; path=/";
	}

	function EraseCookie(name)
	{
		CreateCookie(name, "", -1);
	}

	function ReadCookie(name)
	{
		var nameEQ = name + "=";
		var carr = document.cookie.split(';');
		for (var i = 0; i < carr.length; i++)
		{
			var c = carr[i];
			while (c.charAt(0) == ' ') c = c.substring(1, c.length);
			if (c.indexOf(nameEQ) == 0)
			{
				var cookie = c.substring(nameEQ.length, c.length);
				log.debug(c);
				return cookie;
			}
		}
		return null;
	}


	// set up global object instance
	if (!window.logging) { window.logging = new function() { } }
	window.logging.main = new Main();
	var local = logging.main;


})();

