"use strict";

angular.module("pcnApp")
  .controller("adminController", [
    "$scope", "$q", "$mdDialog", "adminService", function($scope, $q, $mdDialog, adminService) {
      adminService.getComputersInfo().then(function(infos) {
        $scope.infos = infos;
      });

      $scope.showComputer = function(index, id, ip) {
        $scope.buttonIndex = index;
        $scope.currentIp = ip;
      };
    }
  ]);