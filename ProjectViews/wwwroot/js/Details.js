$(document).ready(function () {
  $(".color-option").click(function () {
    $(".color-option").removeClass("selected-color");
    $(this).addClass("selected-color");
  });
});
const color = "e60000";
const size =18;
var app = anglar.module("myDetail", []); // create module
    app.controller("myCtrl", function ($scope, $http) {
  //get data with color and size
      $scope.getData = function () {
        $http.get("https://localhost:7109/api/ShoeDetails/get-shoeDetails-by-Color-Size?colorName=" + color + "&sizeNumber=" + size)
            .then(function (response) {
              $scope.product = response.data;
            });
      }
    });
