mergeInto(LibraryManager.library, {

	
	NeftifySetThemeDefault: function (){
		document.body.removeAttribute("class");
	},
	
	NeftifySetThemeBranded: function (){
		document.body.setAttribute("class", "theme-branded");
	},
	
	NeftifySetThemeDark: function (){
		document.body.setAttribute("class", "theme-dark");
	},
	
	
	NeftifyToggleEmoji: function (){
        document.querySelector("#neftify-connect").showEmoji = !document.querySelector("#neftify-connect").showEmoji;
	},
	
	NeftifyToggleModal: function (){
        document.querySelector("#neftify-connect").noModal = !document.querySelector("#neftify-connect").noModal;
	},
    

	NeftifyDisconnectWithCustomButton: function () {
	
		DISCONNECT();
	},
	
	NeftifyGetConnectionDetails: function () {
		GetAccountData();
	},
	
	NeftifyGetBalance: function () {
		GetAccountBalance();
	},
	
	
	
	NeftifyUserIsConnected: function () {
		GetUserConnected();
	},
	
	NeftifyWindowAlert: function (str) {
		window.alert(UTF8ToString(str));
	},
	NeftifyConnectWithCustomButton: function () {
		 CONNECTNETFY();
        
	}

});