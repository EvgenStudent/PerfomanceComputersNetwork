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
  apiBaseUri: "http://localhost:21100/api",
  timerInterval: 10000
});