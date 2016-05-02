"use strict";

angular.module("pcnApp")
  .controller("adminController", [
    "$scope", "$q", "$mdDialog", "adminService", function($scope, $q, $mdDialog, adminService) {
      adminService.getComputersInfo().then(function(infos) {
        $scope.infos = infos;
      });

      google.charts.load("current", { packages: ["corechart", "line"] });
      var cpuChartInit = function() {
        google.charts.setOnLoadCallback(function() {
          var data = new google.visualization.DataTable();
          data.addColumn("datetime", "DateTime");
          data.addColumn("number", "CPU");

          var rows = [];
          $scope.cpuArray.forEach(function(el) {
            rows.push([new Date(Date.parse(el.DateTime)), el.Value]);
          });
          data.addRows(rows);

          var options = {
            hAxis: {
              title: "Время"
            },
            vAxis: {
              title: "Загруженность (%)"
            }
          };

          var chart = new google.visualization.LineChart(document.getElementById("cpu_chart_div"));

          chart.draw(data, options);
        });
      };
      var ramChartInit = function() {
        google.charts.setOnLoadCallback(function() {
          var data = new google.visualization.DataTable();
          data.addColumn("datetime", "DateTime");
          data.addColumn("number", "RAM");

          var rows = [];
          $scope.ramArray.forEach(function (el) {
            rows.push([new Date(Date.parse(el.DateTime)), el.Usage / 1024 / 1024]);
          });
          data.addRows(rows);

          var options = {
            hAxis: {
              title: "Время"
            },
            vAxis: {
              title: "Загруженность (МБ)"
            }
          };

          var chart = new google.visualization.LineChart(document.getElementById("ram_chart_div"));

          chart.draw(data, options);
        });
      };

      $scope.showComputer = function(index, id, ip) {
        $scope.buttonIndex = index;
        $scope.currentIp = ip;

        $q.all({
          compDetails: adminService.getComputerDetail(id),
          ramArray: adminService.getRamInfo(id),
          cpuArray: adminService.getCpuInfo(id)
        }).then(function(data) {
          $scope.compDetails = data.compDetails;
          $scope.ramArray = data.ramArray;
          $scope.cpuArray = data.cpuArray;

          cpuChartInit();
          ramChartInit();
        }, function(error) {
          console.log(error);
        });
      };
    }
  ]);