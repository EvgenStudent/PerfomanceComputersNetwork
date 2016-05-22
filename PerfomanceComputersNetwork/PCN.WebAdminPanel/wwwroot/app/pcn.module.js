"use strict";

var app = angular.module("pcnApp", [
  "ngAnimate",
  "ngAria",
  "ngCookies",
  "ngResource",
  "ngSanitize",
  "ngMaterial"
]);

app.constant("settings", {
  apiBaseUri: "http://pcnserver20160522042844.azurewebsites.net/api",
  timerInterval: 10000
});