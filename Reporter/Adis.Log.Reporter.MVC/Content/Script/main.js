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
			$('.log_item.has_extra_info').click(function() { logging.main.ToggleExtraInfo(this); });

		}

		, SetPageNumberAndSubmit: function(pageNumber)
		{
			$('#pageNumber').val(pageNumber);
			document.forms[0].submit();
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

			var $categoryDropdown = $("#category");
			$categoryDropdown.unbind('change');
			$categoryDropdown.html("<option value='-1'>Loading, please wait...</option>");

			$.ajax({
				url: urls.Categories,
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

			var $applicationDropdown = $("#application");
			$applicationDropdown.html("<option value='-1'>Loading, please wait...</option>");

			$.ajax({
				url: urls.Applications.replace("__category__", category),
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

	}

	// set up global object instance
	if (!window.logging) { window.logging = new function() { } }
	window.logging.main = new Main();
	var local = logging.main;


})();

