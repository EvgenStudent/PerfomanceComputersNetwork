"use strict";

angular.module("pcnApp")
  .service("adminService", function($q, $http) {
    var getComputersInfo = function() {
      var deferred = $q.defer();
      $http.get("http://localhost:21100/api/measure", {
        headers: { 'Content-Type': "application/json" }
      }).success(function(responce) {
        deferred.resolve(responce);
      }).error(function(error) {
        deferred.reject(error);
      });
      return deferred.promise;
    };

    var getComputerDetail = function(userid) {
      var deferred = $q.defer();
      $http.get("http://localhost:21100/api/measure/" + userid + "/compinfo", {
        headers: { 'Content-Type': "application/json" }
      }).success(function(responce) {
        deferred.resolve(responce);
      }).error(function(error) {
        deferred.reject(error);
      });
      return deferred.promise;
    };

    var getRamInfo = function(userid) {
      var deferred = $q.defer();
      $http.get("http://localhost:21100/api/measure/" + userid + "/ram", {
        headers: { 'Content-Type': "application/json" }
      }).success(function(responce) {
        deferred.resolve(responce);
      }).error(function(error) {
        deferred.reject(error);
      });
      return deferred.promise;
    };

    var getCpuInfo = function(userid) {
      var deferred = $q.defer();
      $http.get("http://localhost:21100/api/measure/" + userid + "/cpu", {
        headers: { 'Content-Type': "application/json" }
      }).success(function(responce) {
        deferred.resolve(responce);
      }).error(function(error) {
        deferred.reject(error);
      });
      return deferred.promise;
    };

    return {
      getComputersInfo: getComputersInfo,
      getComputerDetail: getComputerDetail,
      getRamInfo: getRamInfo,
      getCpuInfo: getCpuInfo
    };
  });