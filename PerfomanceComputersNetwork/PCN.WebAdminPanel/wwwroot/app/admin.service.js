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

    return {
      getComputersInfo: getComputersInfo
    };
  });